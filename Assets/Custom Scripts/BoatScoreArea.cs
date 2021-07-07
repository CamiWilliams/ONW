using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScoreArea : MonoBehaviour
{
    public GameObject currentBoat;
    public GameObject effectObject;

    public int boatNum;

    public GameObject Stage2;

    private void Start()
    {
        effectObject.SetActive(false);
        currentBoat.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boat:" + currentBoat.name + "Collider" + other.gameObject.name);
        string name = other.gameObject.name;
        if (name == "Ball1" || name == "Ball2" || name == "Ball3")
        {
            Stage2.GetComponent<ThrowingSequence>().setHiddenBoat(boatNum);
            Debug.Log("Ball in" + name);
            effectObject.SetActive(true);
            BallScript.resetPosition(other.gameObject);
            StartCoroutine(HideBoat());
        }
    }

    private IEnumerator HideBoat()
    {
        float sec = 2f;
        yield return new WaitForSeconds(sec);
        currentBoat.SetActive(false);
    }
}
