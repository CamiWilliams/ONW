using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWall : MonoBehaviour
{
    public GameObject Rockwall;

    // Start is called before the first frame update
    void Start()
    {
        Rockwall.SetActive(false);
    }

    public void ShowRocks()
    {
        Rockwall.SetActive(true);
    }
}
