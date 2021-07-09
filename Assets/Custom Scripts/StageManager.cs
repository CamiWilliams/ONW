using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject Stage4;
    public GameObject Finale;

    // Start is called before the first frame update
    void Start()
    {
        Stage1.SetActive(false);
        Stage2.SetActive(false);
        Stage3.SetActive(false);
        Stage4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateStage(int stageNum)
    {
        Debug.Log("Activate Stage: " + stageNum);
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
