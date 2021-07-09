using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class StumpInteractable : XRBaseInteractable
{
    public GameObject Stage1;
    public GameObject SignText;
    public string StumpName;

    private int[] memorySequence;
    private int globalIndex;
    private bool isAnimating;

    // Start is called before the first frame update
    void Start()
    {
        memorySequence = Stage1.GetComponent<MemoryPlatforms>().getMemorySequence();
        isAnimating = Stage1.GetComponent<MemoryPlatforms>().getIsAnimating();
        globalIndex = Stage1.GetComponent<MemoryPlatforms>().getGlobalIndex();

        SignText.GetComponent<TextMeshProUGUI>().text = "_ _ _ _";
    }

    // Update is called once per frame
    void Update()
    {
        memorySequence = Stage1.GetComponent<MemoryPlatforms>().getMemorySequence();
        isAnimating = Stage1.GetComponent<MemoryPlatforms>().getIsAnimating();
        globalIndex = Stage1.GetComponent<MemoryPlatforms>().getGlobalIndex();

        if (globalIndex == -1)
        {
            SignText.GetComponent<TextMeshProUGUI>().text = "_ _ _ _";
        }
    }

    public void Teleported()
    {
        if (!isAnimating)
        {
            globalIndex++;
            Stage1.GetComponent<MemoryPlatforms>().setGlobalIndex(globalIndex);

            switch (memorySequence[globalIndex])
            {
                case 0:
                    if (StumpName == "Purple")
                    {
                        correctTeleport();
                    }
                    else
                    {
                        incorrectTeleport();

                    }
                    break;
                case 1:
                    if (StumpName == "Tan")
                    {
                        correctTeleport();
                    }
                    else
                    {
                        incorrectTeleport();
                    }
                    break;
                case 2:
                    if (StumpName == "Orange")
                    {
                        correctTeleport();
                    }
                    else
                    {
                        incorrectTeleport();
                    }
                    break;
                case 3:
                    if (StumpName == "Pink")
                    {
                        correctTeleport();
                    }
                    else
                    {
                        incorrectTeleport();
                    }
                    break;
                case 4:
                    if (StumpName == "Red")
                    {
                        correctTeleport();
                    }
                    else
                    {
                        incorrectTeleport();
                    }
                    break;
                default:
                    Debug.Log("Default whaaaat");
                    break;
            }
        }
    }

    private void correctTeleport()
    {
        string stringBuilder = "";
        int index = 0;

        while (index < memorySequence.Length)
        {
            if (index > globalIndex)
            {
                stringBuilder += "_ ";
                if (index == 3)
                {
                    stringBuilder += " ";
                }
            }
            else
            {
                stringBuilder += "0 ";
            }
            index++;
        }

        SignText.GetComponent<TextMeshProUGUI>().text = stringBuilder;

        if (globalIndex == memorySequence.Length - 1)
        {
            // Completed round 1
            Stage1.GetComponent<MemoryPlatforms>().StageComplete();
        }
    }

    private void incorrectTeleport()
    {
        SignText.GetComponent<TextMeshProUGUI>().text = ":(";
        globalIndex = -1;
        Stage1.GetComponent<MemoryPlatforms>().setGlobalIndex(globalIndex);
    }
}
