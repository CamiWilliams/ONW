using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSequence : MonoBehaviour
{
    public GameObject Stages;

    public GameObject Ball1;
    public GameObject Ball2;
    public GameObject Ball3;

    public GameObject EasyParticle;
    public GameObject MediumParticle;
    public GameObject HardParticle;
    public GameObject BallParticle;

    private bool[] boatsHidden = new bool[3];

    void Start()
    {
        //Ball1.SetActive(false);
        //Ball2.SetActive(false);
        //Ball3.SetActive(false);
        //BallParticle.SetActive(false);
        EasyParticle.SetActive(false);
        MediumParticle.SetActive(false);
        HardParticle.SetActive(false);
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

    public void ShowBalls()
    {
        Ball1.SetActive(true);
        Ball2.SetActive(true);
        Ball3.SetActive(true);

        BallScript.resetPosition(Ball1);
        BallScript.resetPosition(Ball2);
        BallScript.resetPosition(Ball3);
    }

    public void StageComplete()
    {
        Debug.Log("Finished Stage 2!");
        EasyParticle.SetActive(true);
        MediumParticle.SetActive(true);
        HardParticle.SetActive(true);
        //BallParticle.SetActive(true);
        StartCoroutine(HideParticles());
    }

    private IEnumerator HideParticles()
    {
        float sec = 3f;
        yield return new WaitForSeconds(sec);
        EasyParticle.SetActive(false);
        MediumParticle.SetActive(false);
        HardParticle.SetActive(false);
        //BallParticle.SetActive(false);
    }
}
