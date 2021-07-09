using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleSequence : MonoBehaviour
{
    public GameObject FinaleParticle;
    public GameObject WinnerParticle1;
    public GameObject WinnerParticle2;
    public GameObject WinnerMenu;
    public GameObject TimerInfo;

    void Start()
    {
        FinaleParticle.SetActive(false);
        WinnerParticle1.SetActive(false);
        WinnerParticle2.SetActive(false);
        WinnerMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFinale()
    {
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
