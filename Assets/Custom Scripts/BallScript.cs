using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private static Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector3(-8f, 0.9f, -1.8f);
        //originalPos = new Vector3(-0.73f, 1.24f, 0.75f);
    }

    public static void resetPosition(GameObject currBall)
    {
        currBall.transform.position = originalPos;
    }
}
