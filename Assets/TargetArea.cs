using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    public int StageNum;
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject Stage4;
    public GameObject Finale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Stage Enter! " + StageNum);
        switch (StageNum)
        {
            case 0:
                Stage1.GetComponent<MemoryPlatforms>().ShowStage1Animation();
                break;
            case 1:
                Stage2.GetComponent<ThrowingSequence>().ShowBoats();
                break;
            case 2:
                Stage3.GetComponent<SimonSays>().StartSimonSays();
                break;
            case 3:
                Stage4.GetComponent<RockWall>().ShowRocks();
                break;
            case 4:
                Finale.GetComponent<FinaleSequence>().PlayFinale();
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("ON target exit");
    }
}
