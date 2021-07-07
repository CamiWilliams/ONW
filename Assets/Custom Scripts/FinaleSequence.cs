using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleSequence : MonoBehaviour
{
    private GameObject FinaleParticle;
    private GameObject WinnerParticle1;
    private GameObject WinnerParticle2;
    private GameObject WinnerMenu;
    private GameObject TimerInfo;

    void Start()
    {
        FinaleParticle = GameObject.Find("FinaleParticle");
        FinaleParticle.SetActive(false);
        WinnerParticle1 = GameObject.Find("WinnerParticle1");
        WinnerParticle1.SetActive(false);
        WinnerParticle2 = GameObject.Find("WinnerParticle2");
        WinnerParticle2.SetActive(false);
        WinnerMenu = GameObject.Find("WinnerMenu");
        WinnerMenu.SetActive(false);

        TimerInfo = GameObject.Find("TimerInfo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFinale()
    {
        Debug.Log("Finished All Stages!");
        TimerInfo.SetActive(false);

        FinaleParticle.SetActive(true);
        WinnerParticle1.SetActive(true);
        WinnerParticle2.SetActive(true);
        WinnerMenu.SetActive(true);

        StartCoroutine(HideParticles());
    }

    private IEnumerator HideParticles()
    {
        float sec = 3f;
        yield return new WaitForSeconds(sec);
        FinaleParticle.SetActive(false);
    }
}
