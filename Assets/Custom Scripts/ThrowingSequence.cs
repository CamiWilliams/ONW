using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSequence : MonoBehaviour
{
    [Header("Manager References")]
    public MainGameManager gameManager;
    public MainSoundManager soundManager;

    [Header("Ball Game Objects")]
    public GameObject Ball1;
    public GameObject Ball2;
    public GameObject Ball3;

    [Header("Particle Game Objects")]
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
            gameManager.GetComponent<MainGameManager>().ActivateStage(2);
        }
    }

    public void ShowBalls()
    {
        Ball1.SetActive(true);
        Ball2.SetActive(true);
        Ball3.SetActive(true);

        resetBallPositions();
    }

    public void StageComplete()
    {
        soundManager.PlayDing();

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

    /**
     * Function to reset the position of all the ball objects
     * back into the crate.
     */
    public void resetBallPositions()
    {
        Ball1.transform.position = new Vector3(-7.41f, 1.16f, 2.473f);
        Ball2.transform.position = new Vector3(-7.46f, 1.16f, 2.681f);
        Ball3.transform.position = new Vector3(-7.1f, 1.16f, 2.644f);
    }
}
