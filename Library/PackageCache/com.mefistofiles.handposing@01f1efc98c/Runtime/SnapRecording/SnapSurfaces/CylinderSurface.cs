﻿using UnityEngine;

namespace HandPosing.SnapSurfaces
{
    [System.Serializable]
    public class CylinderSurfaceData : SnapSurfaceData
    {
        public override System.Type SurfaceType => typeof(CylinderSurface);

        public override object Clone()
        {
            CylinderSurfaceData clone = new CylinderSurfaceData();
            clone.startPoint = this.startPoint;
            clone.endPoint = this.endPoint;
            clone.angle = this.angle;
            return clone;
        }

        public override SnapSurfaceData Mirror()
        {
            CylinderSurfaceData mirror = Clone() as CylinderSurfaceData;
            return mirror;
        }

        public Vector3 startPoint = new Vector3(0f, 0.1f, 0f);
        public Vector3 endPoint = new Vector3(0f, -0.1f, 0f);

        [Range(0f, 360f)]
        public float angle = 120f;
    }

    /// <summary>
    /// This type of surface defines a cylinder in which the grip pose is valid around an object.
    /// An angle can be used to constrain the cylinder and not use a full circle.
    /// The radious is automatically specified as the distance from the axis of the cylinder to the original grip position.
    /// </summary>
    [System.Serializable]
    public class CylinderSurface : SnapSurface
    {
        [SerializeField]
        private CylinderSurfaceData _data = new CylinderSurfaceData();

        public override SnapSurfaceData Data
        {
            get => _data;
            set => _data = value as CylinderSurfaceData; 
        }

        /// <summary>
        /// Direction from the axis of the cylinder to the original grip position.
        /// </summary>
        public Vector3 StartAngleDir
        {
            get
            {
                if (this.GripPoint == null)
                {
                    return Vector3.forward;
                }
                return Vector3.ProjectOnPlane(GripPoint.transform.position - StartPoint, Direction).normalized;
            }
        }

        /// <summary>
        /// Direction from the axis of the cylinder to the maximum angle alowance.
        /// </summary>
        public Vector3 EndAngleDir
        {
            get
            {
                return Quaternion.AngleAxis(Angle, Direction) * StartAngleDir;
            }
        }

        /// <summary>
        /// Base cap of the cylinder, in world coordinates.
        /// </summary>
        public Vector3 StartPoint
        {
            get
            {
                if (this.relativeTo != null)
                {
                    return this.relativeTo.TransformPoint(_data.startPoint);
                }
                else
                {
                    return _data.startPoint;
                }
            }
            set
            {
                if (this.relativeTo != null)
                {
                    _data.startPoint = this.relativeTo.InverseTransformPoint(value);
                }
                else
                {
                    _data.startPoint = value;
                }
            }
        }

        /// <summary>
        /// End cap of the cylinder, in world coordinates.
        /// </summary>
        public Vector3 EndPoint
        {
            get
            {
                if (this.relativeTo != null)
                {
                    return this.relativeTo.TransformPoint(_data.endPoint);
                }
                else
                {
                    return _data.endPoint;
                }
            }
            set
            {
                if (this.relativeTo != null)
                {
                    _data.endPoint = this.relativeTo.InverseTransformPoint(value);
                }
                else
                {
                    _data.endPoint = value;
                }
            }
        }

        /// <summary>
        /// The maximum angle for the surface of the cylinder, starting from the original grip position.
        /// To invert the direction of the angle, swap the caps order.
        /// </summary>
        public float Angle
        {
            get
            {
                return _data.angle;
            }
            set
            {
                _data.angle = Mathf.Repeat(value, 360f);
            }
        }

        /// <summary>
        /// The generated radious of the cylinder.
        /// Represents the distance from the axis of the cylinder to the original grip position.
        /// </summary>
        public float Radious
        {
            get
            {
                if (this.GripPoint == null)
                {
                    return 0f;
                }
                Vector3 start = StartPoint;
                Vector3 projectedPoint = start + Vector3.Project(this.GripPoint.position - start, Direction);
                return Vector3.Distance(projectedPoint, this.GripPoint.position);
            }
        }

        /// <summary>
        /// The direction of the central axis of the cylinder.
        /// </summary>
        public Vector3 Direction
        {
            get
            {
                Vector3 dir = (EndPoint - StartPoint);
                if (dir.sqrMagnitude == 0f)
                {
                    return this.relativeTo ? this.relativeTo.up : Vector3.up;
                }
                return dir.normalized;
            }
        }

        private float Height
        {
            get
            {
                return (EndPoint - StartPoint).magnitude;
            }
        }

        /// <summary>
        /// The rotation of the central axis of the cylinder.
        /// </summary>
        private Quaternion Rotation
        {
            get
            {
                if (_data.startPoint == _data.endPoint)
                {
                    return Quaternion.LookRotation(Vector3.forward);
                }
                return Quaternion.LookRotation(StartAngleDir, Direction);
            }
        }

        public override HandPose InvertedPose(HandPose pose)
        {
            HandPose invertedPose = pose;
            Quaternion globalRot = relativeTo.rotation * invertedPose.relativeGrip.rotation;

            Quaternion invertedRot = Quaternion.AngleAxis(180f, StartAngleDir) * globalRot;
            invertedPose.relativeGrip.rotation = Quaternion.Inverse(relativeTo.rotation) * invertedRot;

            return invertedPose;
        }

        public override Pose MirrorPose(Pose pose)
        {
            Vector3 normal = Quaternion.Inverse(this.relativeTo.rotation) * StartAngleDir;
            Vector3 tangent = Quaternion.Inverse(this.relativeTo.rotation) * Direction;

            return pose.MirrorPose(normal, tangent);
        }

        private Vector3 PointAltitude(Vector3 point)
        {
            Vector3 start = StartPoint;
            Vector3 projectedPoint = start + Vector3.Project(point - start, Direction);
            return projectedPoint;
        }

        public override Vector3 NearestPointInSurface(Vector3 targetPosition)
        {
            Vector3 start = StartPoint;
            Vector3 dir = Direction;
            Vector3 projectedVector = Vector3.Project(targetPosition - start, dir);

            if (projectedVector.magnitude > Height)
            {
                projectedVector = projectedVector.normalized * Height;
            }
            if (Vector3.Dot(projectedVector, dir) < 0f)
            {
                projectedVector = Vector3.zero;
            }

            Vector3 projectedPoint = StartPoint + projectedVector;
            Vector3 targetDirection = Vector3.ProjectOnPlane((targetPosition - projectedPoint), dir).normalized;
            //clamp of the surface
            float desiredAngle = Mathf.Repeat(Vector3.SignedAngle(StartAngleDir, targetDirection, dir), 360f);
            if (desiredAngle > Angle)
            {
                if (Mathf.Abs(desiredAngle - Angle) >= Mathf.Abs(360f - desiredAngle))
                {
                    targetDirection = StartAngleDir;
                }
                else
                {
                    targetDirection = EndAngleDir;
                }
            }
            Vector3 surfacePoint = projectedPoint + targetDirection * Radious;
            return surfacePoint;
        }

        public override Pose MinimalRotationPoseAtSurface(Pose userPose, Pose snapPose)
        {
            Vector3 desiredPos = userPose.position;
            Quaternion desiredRot = userPose.rotation;
            Quaternion baseRot = snapPose.rotation;
            Quaternion rotDif = (desiredRot) * Quaternion.Inverse(baseRot);
            Vector3 desiredDirection = (rotDif * Rotation) * Vector3.forward;
            Vector3 projectedDirection = Vector3.ProjectOnPlane(desiredDirection, Direction).normalized;
            Vector3 altitudePoint = PointAltitude(desiredPos);
            Vector3 surfacePoint = NearestPointInSurface(altitudePoint + projectedDirection * Radious);
            Quaternion surfaceRotation = CalculateRotationOffset(surfacePoint, desiredRot) * baseRot;
            return new Pose(surfacePoint, surfaceRotation);
        }

        public override Pose MinimalTranslationPoseAtSurface(Pose userPose, Pose snapPose)
        {
            Vector3 desiredPos = userPose.position;
            Quaternion baseRot = snapPose.rotation;

            Vector3 surfacePoint = NearestPointInSurface(desiredPos);
            Quaternion surfaceRotation = CalculateRotationOffset(surfacePoint, userPose.rotation) * baseRot;

            return new Pose(surfacePoint, surfaceRotation);
        }

        protected Quaternion CalculateRotationOffset(Vector3 surfacePoint, Quaternion desiredRotation)
        {
            Vector3 recordedDirection = Vector3.ProjectOnPlane(this.GripPoint.position - StartPoint, Direction);
            Vector3 desiredDirection = Vector3.ProjectOnPlane(surfacePoint - StartPoint, Direction);
            return Quaternion.FromToRotation(recordedDirection, desiredDirection);
        }

    }
}