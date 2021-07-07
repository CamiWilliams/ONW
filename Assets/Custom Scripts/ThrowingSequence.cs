using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSequence : MonoBehaviour
{
    public GameObject Stages;

    private GameObject WoodBoatHard;
    private GameObject WoodBoatMedium;
    private GameObject WoodBoatEasy;

    private GameObject EasyParticle;
    private GameObject MediumParticle;
    private GameObject HardParticle;
    private GameObject BallParticle;

    private bool[] boatsHidden = new bool[3];

    void Start()
    {
        WoodBoatHard = GameObject.Find("WoodBoatHard");
        WoodBoatMedium = GameObject.Find("WoodBoatMedium");
        WoodBoatEasy = GameObject.Find("WoodBoatEasy");
        WoodBoatHard.SetActive(false);
        WoodBoatMedium.SetActive(false);
        WoodBoatEasy.SetActive(false);

        EasyParticle = GameObject.Find("EasyParticle");
        MediumParticle = GameObject.Find("MediumParticle");
        HardParticle = GameObject.Find("HardParticle");
        BallParticle = GameObject.Find("BallParticle");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setHiddenBoat(int index)
    {
        boatsHidden[index] = true;

        if (boatsHidden[0] && boatsHidden[1] && boatsHidden[2])
        {
            StageComplete();
            Stages.GetComponent<StageManager>().ActivateStage(2);
        }
    }

    public void ShowBoats()
    {
        WoodBoatHard.SetActive(true);
        WoodBoatMedium.SetActive(true);
        WoodBoatEasy.SetActive(true);
    }

    public void StageComplete()
    {
        Debug.Log("Finished Stage 2!");
        //EasyParticle.SetActive(true);
        //MediumParticle.SetActive(true);
        //HardParticle.SetActive(true);
        BallParticle.SetActive(true);
        StartCoroutine(HideParticles());
    }

    private IEnumerator HideParticles()
    {
        float sec = 3f;
        yield return new WaitForSeconds(sec);
        //EasyParticle.SetActive(false);
        //MediumParticle.SetActive(false);
        //HardParticle.SetActive(false);
        BallParticle.SetActive(false);
    }
}
