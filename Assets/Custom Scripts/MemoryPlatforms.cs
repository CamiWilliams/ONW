using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MemoryPlatforms : MonoBehaviour
{
    public GameObject Stages;

    private GameObject PurplePlatform;
    private GameObject TanPlatform;
    private GameObject OrangePlatform;
    private GameObject PinkPlatform;
    private GameObject RedPlatform;

    private GameObject PurplePlatformHighLighted;
    private GameObject TanPlatformHighLighted;
    private GameObject OrangePlatformHighLighted;
    private GameObject PinkPlatformHighLighted;
    private GameObject RedPlatformHighLighted;

    private GameObject SignParticle;
    private GameObject PurpleParticle;
    private GameObject TanParticle;
    private GameObject OrangeParticle;
    private GameObject PinkParticle;
    private GameObject RedParticle;

    private int sequenceLength = 4;
    private int seqIndex = -1;
    private bool isAnimating;

    private int[] memorySequence;

    private int globalIndex = -1;

    void Start()
    {
        PurplePlatform = GameObject.Find("PurpleTreeStump");
        TanPlatform = GameObject.Find("TanTreeStump");
        OrangePlatform = GameObject.Find("OrangeTreeStump");
        PinkPlatform = GameObject.Find("PinkTreeStump");
        RedPlatform = GameObject.Find("RedTreeStump");

        PurplePlatformHighLighted = GameObject.Find("PurpleTreeStumpHighlighted");
        PurplePlatformHighLighted.SetActive(false);
        TanPlatformHighLighted = GameObject.Find("TanTreeStumpHighlighted");
        TanPlatformHighLighted.SetActive(false);
        OrangePlatformHighLighted = GameObject.Find("OrangeTreeStumpHighlighted");
        OrangePlatformHighLighted.SetActive(false);
        PinkPlatformHighLighted = GameObject.Find("PinkTreeStumpHighlighted");
        PinkPlatformHighLighted.SetActive(false);
        RedPlatformHighLighted = GameObject.Find("RedTreeStumpHighlighted");
        RedPlatformHighLighted.SetActive(false);

        SignParticle = GameObject.Find("SignParticle");
        SignParticle.SetActive(false);
        PurpleParticle = GameObject.Find("PurpleParticle");
        PurpleParticle.SetActive(false);
        TanParticle = GameObject.Find("TanParticle");
        TanParticle.SetActive(false);
        OrangeParticle = GameObject.Find("OrangeParticle");
        OrangeParticle.SetActive(false);
        PinkParticle = GameObject.Find("PinkParticle");
        PinkParticle.SetActive(false);
        RedParticle = GameObject.Find("RedParticle");
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
                Debug.Log("PurpleTreeStump");
                PurplePlatform.SetActive(false);
                PurplePlatformHighLighted.SetActive(true);
                break;
            case 1:
                Debug.Log("TanTreeStump");
                TanPlatform.SetActive(false);
                TanPlatformHighLighted.SetActive(true);
                break;
            case 2:
                Debug.Log("OrangeTreeStump");
                OrangePlatform.SetActive(false);
                OrangePlatformHighLighted.SetActive(true);
                break;
            case 3:
                Debug.Log("PinkTreeStump");
                PinkPlatform.SetActive(false);
                PinkPlatformHighLighted.SetActive(true);
                break;
            case 4:
                Debug.Log("RedTreeStump");
                RedPlatform.SetActive(false);
                RedPlatformHighLighted.SetActive(true);
                break;
            default:
                Debug.Log("Default Platform");
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
        Debug.Log("Finished Stage 1!");
        SignParticle.SetActive(true);
        PurpleParticle.SetActive(true);
        TanParticle.SetActive(true);
        OrangeParticle.SetActive(true);
        PinkParticle.SetActive(true);
        RedParticle.SetActive(true);
        StartCoroutine(HideParticles());

        Stages.GetComponent<StageManager>().ActivateStage(1);
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
