using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/**
 * This class names gestures based on the characteristics from the HandManager.
 * Edited from https://github.com/jojods1125/VRGestureDetection
 * 
 * From the scene it takes in the HandManager game object, and allows you to 
 * toggle which gestures are active.
 */
public class SocialGestures : MonoBehaviour
{
    /** The HandManager game object. */
    public HandManager manager;

    /** The list of gestures to activate. */
    [Header("Active Gestures")]
    public bool thumbsUp = true;
    public bool thumbsDown = true;
    public bool paper = true;
    public bool rock = true;
    public bool peace = true;

    // Accessible list of all gesture names for this library
    private List<string> gestureList = new List<string>();

    void Start()
    {
        foreach (FieldInfo f in GetType().GetFields())
        {
            if (f.FieldType == typeof(bool))
            {
                gestureList.Add(f.Name);
            }
        }
    }

    void LateUpdate()
    {
        int[] handShape = manager.PrimaryHandShape();
        HandManager.Orientation[] orient = manager.PrimaryOrientation();

        // Thumb's Up
        if (thumbsUp &&
            handShape[0] <= (int)Finger.FingerShape.Curved &&
            handShape[1] >= (int)Finger.FingerShape.Folded &&
            handShape[2] >= (int)Finger.FingerShape.Folded &&
            handShape[3] >= (int)Finger.FingerShape.Folded &&
            handShape[4] >= (int)Finger.FingerShape.Folded &&
            orient[4] == HandManager.Orientation.Thumb_Up &&
            manager.PrimaryFingerPinch(Finger.FingerType.Index) == 0
            )
        {
            manager.SetCurrentGesture("Thumb's Up");
            return;
        }

        // Thumb's Down
        if (thumbsDown &&
            handShape[0] <= (int)Finger.FingerShape.Curved &&
            handShape[1] >= (int)Finger.FingerShape.Bent &&
            handShape[2] >= (int)Finger.FingerShape.Bent &&
            handShape[3] >= (int)Finger.FingerShape.Bent &&
            handShape[4] >= (int)Finger.FingerShape.Bent &&
            orient[4] == HandManager.Orientation.Thumb_Down &&
            manager.PrimaryFingerPinch(Finger.FingerType.Index) == 0
            )
        {
            manager.SetCurrentGesture("Thumb's Down");
            return;
        }

        // Paper
        if (paper &&
            handShape[0] <= (int)Finger.FingerShape.Curved &&
            handShape[1] <= (int)Finger.FingerShape.Curved &&
            handShape[2] <= (int)Finger.FingerShape.Curved &&
            handShape[3] <= (int)Finger.FingerShape.Curved &&
            handShape[4] <= (int)Finger.FingerShape.Curved &&
            manager.PrimaryFingerTouch(Finger.FingerType.Index, Finger.FingerType.Middle) &&
            manager.PrimaryFingerTouch(Finger.FingerType.Middle, Finger.FingerType.Ring) &&
            manager.PrimaryFingerTouch(Finger.FingerType.Ring, Finger.FingerType.Pinky) &&
            orient[2] == HandManager.Orientation.Palm_Down &&
            orient[3] == HandManager.Orientation.Knuckles_Mid
            )
        {
            manager.SetCurrentGesture("Paper");
            return;
        }

        // Rock
        if (rock &&
            handShape[0] >= (int)Finger.FingerShape.Curved &&
            handShape[1] >= (int)Finger.FingerShape.Folded &&
            handShape[2] >= (int)Finger.FingerShape.Folded &&
            handShape[3] >= (int)Finger.FingerShape.Folded &&
            handShape[4] >= (int)Finger.FingerShape.Folded &&
            orient[1] == HandManager.Orientation.Palm_In &&
            orient[3] != HandManager.Orientation.Knuckles_In &&
            orient[4] == HandManager.Orientation.Thumb_Up
            )
        {
            manager.SetCurrentGesture("Rock");
            return;
        }

        // Peace
        if (peace &&
            handShape[0] >= (int)Finger.FingerShape.Curved &&
            handShape[1] == (int)Finger.FingerShape.Extended &&
            handShape[2] == (int)Finger.FingerShape.Extended &&
            handShape[3] >= (int)Finger.FingerShape.Folded &&
            handShape[4] >= (int)Finger.FingerShape.Folded &&
            !manager.PrimaryFingerTouch(Finger.FingerType.Index, Finger.FingerType.Middle) &&
            (orient[0] == HandManager.Orientation.Palm_Back || orient[0] == HandManager.Orientation.Palm_Front) &&
            orient[3] == HandManager.Orientation.Knuckles_Up
            )
        {
            manager.SetCurrentGesture("Peace");
            return;
        }

        manager.SetCurrentGesture(null);
    }

    /**
     * Sets a given gesture as active or inactive.
     * @param var the gesture
     * @param isActive a boolean if it is active or not
     */
    public void SetGestureActive(string var, bool isActive)
    {
        GetType().GetField(var).SetValue(this, isActive);
    }

    public List<string> GetGestureList()
    {
        return gestureList;
    }

}
