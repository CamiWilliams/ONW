using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public string WallName;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Curr wall: " + WallName);
        string name = other.gameObject.name;
        if (name == "Ball1" || name == "Ball2" || name == "Ball3")
        {
            Debug.Log("Ball out of bounds" + name);
            BallScript.resetPosition(other.gameObject);
        }
    }
}
