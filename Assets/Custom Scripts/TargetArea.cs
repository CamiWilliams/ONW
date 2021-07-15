using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetArea : MonoBehaviour
{
    public int StageNum;
    public GameObject Stages;
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
        switch (StageNum)
        {
            case 0:
                Stage1.GetComponent<MemoryPlatforms>().ShowStage1Animation();
                break;
            case 1:
                Stage2.GetComponent<ThrowingSequence>().ShowBalls();
                break;
            case 2:
                StageManager.isStage3Done = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 3:
                Stage4.GetComponent<RockWall>().ShowRocks();
                break;
            case 4:
                if( other.name != "PlatformRock")
                {
                    Finale.GetComponent<FinaleSequence>().PlayFinale();
                }
                break;
            case 5: //Play game
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 6: //Instructions
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
