using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Gesture library for frequently used social/fun gestures
/// <br></br><b>Add to scene within a single GameObject and initialize manager in editor to use</b>
/// </summary>
public class SocialGestures : GestureLibrary
{
    // The HandManager
    public HandManager manager;

    // List of activated gestures
    [Header("Active Gestures")]
    public bool thumbsUp = true;
    public bool thumbsDown = true;
    public bool paper = true;
    public bool rock = true;
    public bool okay = true;
    public bool peace = true;

    // Accessible list of all gesture names for this library
    private List<string> gestureList = new List<string>();

    // Initializes gestureList
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

    /// <summary>
    /// Gets the list of gesture names so the library can have gestures turned on/off through SetGestureActive()
    /// </summary>
    /// <returns> String list of all gesture names in this library </returns>
    public List<string> GetGestureList()
    {
        return gestureList;
    }

    /// <summary>
    /// Sets a given gesture to active or inactive
    /// </summary>
    /// <param name="var"> Gesture variable name </param>
    /// <param name="isActive"> True if gesture should be active, false otherwise </param>
    public void SetGestureActive(string var, bool isActive)
    {
        GetType().GetField(var).SetValue(this, isActive);
    }



    /// <summary>
    /// Handles gesture detection per update
    /// </summary>
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

        // Okay
        if (okay &&
            handShape[0] <= (int)Finger.FingerShape.Curved &&
            handShape[1] <= (int)Finger.FingerShape.Curved &&
            handShape[2] <= (int)Finger.FingerShape.Curved &&
            handShape[3] <= (int)Finger.FingerShape.Curved &&
            handShape[4] <= (int)Finger.FingerShape.Curved &&
            orient[3] == HandManager.Orientation.Knuckles_Up &&
            manager.PrimaryFingerPinch(Finger.FingerType.Index) == 1
            )
        {
            manager.SetCurrentGesture("Okay");
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
}
