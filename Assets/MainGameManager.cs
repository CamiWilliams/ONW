using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public GameObject Scoreboard;

    void Start()
    {
        DontDestroyOnLoad(Scoreboard);
    }

    void Update()
    {
        var rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

        if (rightHandDevices.Count == 1)
        {
            InputDevice device = rightHandDevices[0];
            bool primaryButtonValue;
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue)
                && primaryButtonValue)
            {
                Debug.Log("primaryButtonValue is pressed");
                QuitGame();
            }
        }
        else if (rightHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }

    }

    public void PlayGame()
    {
        Debug.Log("Play Oculus Ninja Warrior");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Oculus Ninja Warrior");
        Application.Quit();
    }
}
