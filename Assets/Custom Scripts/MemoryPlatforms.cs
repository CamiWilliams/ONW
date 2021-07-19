using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MemoryPlatforms : MonoBehaviour
{
    [Header("Manager References")]
    public MainGameManager gameManager;
    public MainSoundManager soundManager;

    [Header("Main Tree Stumps")]
    public GameObject PurplePlatform;
    public GameObject TanPlatform;
    public GameObject OrangePlatform;
    public GameObject PinkPlatform;
    public GameObject RedPlatform;

    [Header("Highlighted Tree Stumps")]
    public GameObject PurplePlatformHighLighted;
    public GameObject TanPlatformHighLighted;
    public GameObject OrangePlatformHighLighted;
    public GameObject PinkPlatformHighLighted;
    public GameObject RedPlatformHighLighted;

    [Header("Winner Particles")]
    public GameObject SignParticle;
    public GameObject PurpleParticle;
    public GameObject TanParticle;
    public GameObject OrangeParticle;
    public GameObject PinkParticle;
    public GameObject RedParticle;

    private int sequenceLength = 4;
    private int seqIndex = -1;
    private bool isAnimating;

    private int[] memorySequence;

    private int globalIndex = -1;

    void Start()
    {
        PurplePlatformHighLighted.SetActive(false);
        TanPlatformHighLighted.SetActive(false);
        OrangePlatformHighLighted.SetActive(false);
        PinkPlatformHighLighted.SetActive(false);
        RedPlatformHighLighted.SetActive(false);

        SignParticle.SetActive(false);
        PurpleParticle.SetActive(false);
        TanParticle.SetActive(false);
        OrangeParticle.SetActive(false);
        PinkParticle.SetActive(false);
        RedParticle.SetActive(false);

        memorySequence = new int[sequenceLength];

        SetUpMemorySequence();
    }

    void Update()
    {

    }

    private void SetUpMemorySequence ()
    {
        System.Random randNum = new System.Random();
        int Min = 0;
        int Max = 5;
        int number = 0;
        int last_number = -1;
        int index = 0;

        do
        {
            number = randNum.Next(Min, Max);
            if (number == last_number) continue;
            memorySequence[index] = number;
            index++;
            last_number = number;
        } while (index < sequenceLength);
    }

    public void ShowStage1Animation()
    {
        InvokeRepeating("AnimatePlatforms", 1.0f, 2.0f);
    }

    private void AnimatePlatforms()
    {
        //TODO use this variable
        isAnimating = true;
        seqIndex++;
        unhighlightAll();

        if (seqIndex == sequenceLength)
        {
            CancelInvoke("AnimatePlatforms");
            isAnimating = false;
            seqIndex = -1;
            return;
        }

        switch (memorySequence[seqIndex])
        {
            case 0:
                PurplePlatform.SetActive(false);
                PurplePlatformHighLighted.SetActive(true);
                break;
            case 1:
                TanPlatform.SetActive(false);
                TanPlatformHighLighted.SetActive(true);
                break;
            case 2:
                OrangePlatform.SetActive(false);
                OrangePlatformHighLighted.SetActive(true);
                break;
            case 3:
                PinkPlatform.SetActive(false);
                PinkPlatformHighLighted.SetActive(true);
                break;
            case 4:
                RedPlatform.SetActive(false);
                RedPlatformHighLighted.SetActive(true);
                break;
            default:
                PurplePlatform.SetActive(false);
                PurplePlatformHighLighted.SetActive(true);
                break;
        }
    }

    private void unhighlightAll()
    {
        PurplePlatform.SetActive(true);
        TanPlatform.SetActive(true);
        OrangePlatform.SetActive(true);
        PinkPlatform.SetActive(true);
        RedPlatform.SetActive(true);

        PurplePlatformHighLighted.SetActive(false);
        TanPlatformHighLighted.SetActive(false);
        OrangePlatformHighLighted.SetActive(false);
        PinkPlatformHighLighted.SetActive(false);
        RedPlatformHighLighted.SetActive(false);
    }

    public int[] getMemorySequence()
    {
        return memorySequence;
    }

    public bool getIsAnimating()
    {
        return isAnimating;
    }

    public int getGlobalIndex()
    {
        return globalIndex;
    }

    public void setGlobalIndex(int num)
    {
        globalIndex = num;
    }

    public void StageComplete()
    {
        soundManager.PlayDing();

        SignParticle.SetActive(true);
        PurpleParticle.SetActive(true);
        TanParticle.SetActive(true);
        OrangeParticle.SetActive(true);
        PinkParticle.SetActive(true);
        RedParticle.SetActive(true);
        StartCoroutine(HideParticles());

        gameManager.GetComponent<MainGameManager>().ActivateStage(1);
    }

    private IEnumerator HideParticles()
    {
        float sec = 3f;
        yield return new WaitForSeconds(sec);
        SignParticle.SetActive(false);
        PurpleParticle.SetActive(false);
        TanParticle.SetActive(false);
        OrangeParticle.SetActive(false);
        PinkParticle.SetActive(false);
        RedParticle.SetActive(false);
    }
}
