using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWall : MonoBehaviour
{
    public GameObject Stages;

    private GameObject Rockwall;

    // Start is called before the first frame update
    void Start()
    {
        Rockwall = GameObject.Find("Rockwall");
        Rockwall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRocks()
    {
        Rockwall.SetActive(true);
    }
}
