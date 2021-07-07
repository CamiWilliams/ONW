using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    public GameObject Stages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSimonSays()
    {
        Debug.Log("Simon Says starts");
        Stages.GetComponent<StageManager>().ActivateStage(3);
    }
}
