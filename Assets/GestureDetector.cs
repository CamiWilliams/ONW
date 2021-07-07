using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerData;
    public UnityEvent onRecognized;
}

public class GestureDetector : MonoBehaviour
{
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    private List<OVRBone> fingerBones;

    private bool debugMode;

    // Start is called before the first frame update
    void Start()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
    }

    // Update is called once per frame
    void Update()
    {
        // returns true if the “X” button was released this frame.
        if (debugMode && OVRInput.Get(OVRInput.RawButton.X))
        {
            Save();
        }
    }

    void Save()
    {
        Gesture g = new Gesture();

        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();

        foreach (var bone in fingerBones)
        {
            //finger position relative to root
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
            Debug.Log("Bone " + bone + ": " + data);
        }

        g.fingerData = data;
        gestures.Add(g);
    }
}
