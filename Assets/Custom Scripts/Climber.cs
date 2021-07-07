using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    private CharacterController character;
    public static XRRayInteractor climbingHand;
    private ActionBasedContinuousMoveProvider continuousMovement;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ActionBasedContinuousMoveProvider>();
    }

    // Update is called once per frame (fixedupdate for smoother movement
    void FixedUpdate()
    {
        if (climbingHand)
        {
            continuousMovement.enabled = false;
            string leftOrRight = climbingHand.name;

            if (leftOrRight == "LeftHand Controller")
            {
                Climb(XRNode.LeftHand);
            }
            else
            {
                Climb(XRNode.RightHand);

            }
        }
        else
        {
            continuousMovement.enabled = true;
        }
    }

    // Climbing computations
    void Climb(XRNode hand)
    {
        InputDevices.GetDeviceAtXRNode(hand).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}
