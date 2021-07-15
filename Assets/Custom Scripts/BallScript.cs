using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class to reset the ball object back into the crate
 */
public class BallScript : MonoBehaviour
{
    private static Vector3 originalPos;

    void Start()
    {
        originalPos = new Vector3(-7.6f, 1.0f, 2.7f);
        //originalPos = new Vector3(-0.73f, 1.24f, 0.75f);
    }

    public static void resetPosition(GameObject currBall)
    {
        currBall.transform.position = originalPos;
    }
}
