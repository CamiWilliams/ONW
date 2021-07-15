using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StageManager : MonoBehaviour
{
    public XRRig rig;

    [Header("Stage Game Objects")]
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject Stage4;
    public GameObject Finale;

    public static bool isStage3Done;
    private bool debug = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!isStage3Done)
        {
            Stage1.SetActive(false);
            Stage2.SetActive(false);
            Stage3.SetActive(false);
            Stage4.SetActive(false);
        }
        else
        {
            Stage1.SetActive(true);
            Stage2.SetActive(true);
            Stage3.SetActive(true);
            Stage4.SetActive(true);
            rig.transform.position = new Vector3(9.9f, 0.87f, 0.8f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateStage(int stageNum)
    {
        Debug.Log("Activate Stage: " + stageNum);

        if (debug)
        {
            Stage1.SetActive(true);
            Stage2.SetActive(true);
            Stage3.SetActive(true);
            return;

        }

        switch (stageNum)
        {
            case 0:
                Stage1.SetActive(true);
                break;
            case 1:
                Stage2.SetActive(true);
                break;
            case 2:
                Stage3.SetActive(true);
                break;
            case 3:
                Stage4.SetActive(true);
                break;
            default:
                Debug.Log("Default stage");
                break;
        }
    }
}
