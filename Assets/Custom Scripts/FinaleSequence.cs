using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class to handle the logic for the ending finale sequence.
 */
public class FinaleSequence : MonoBehaviour
{
    [Header("Finale Game Objects")]
    public GameObject FinaleParticle;
    public GameObject WinnerParticle1;
    public GameObject WinnerParticle2;
    public GameObject WinnerMenu;
    public GameObject TimerInfo;
    public GameObject NextLevelObjects;

    void Start()
    {
        FinaleParticle.SetActive(false);
        WinnerParticle1.SetActive(false);
        WinnerParticle2.SetActive(false);
        WinnerMenu.SetActive(false);
        NextLevelObjects.SetActive(false);
    }

    /**
     * Function to deactivate the timer game objects
     * and activate the "winner" insignia and particles
     */
    public void PlayFinale()
    {
        TimerInfo.SetActive(false);

        FinaleParticle.SetActive(true);
        WinnerParticle1.SetActive(true);
        WinnerParticle2.SetActive(true);
        WinnerMenu.SetActive(true);
        NextLevelObjects.SetActive(true);

        StartCoroutine(HideParticles());
    }

    /**
     * Function to stop the finale particles.
     */
    private IEnumerator HideParticles()
    {
        float sec = 3f;
        yield return new WaitForSeconds(sec);
        FinaleParticle.SetActive(false);
    }
}
