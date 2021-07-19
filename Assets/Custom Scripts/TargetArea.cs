using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

/**
 * Class to define logic for entering a target area,
 * triggering the audio for the current stage.
 */
public class TargetArea : MonoBehaviour
{
    [Header("Manager References")]
    public MainSoundManager soundManager;

    [Header("Player")]
    public XRRig rig;

    [Header("Current Stage")]
    public int StageNum;

    /**
     * Function that gets called when a player enters
     * the target area.
     * @param other The object that enters the collider.
     */
    private void OnTriggerEnter(Collider other)
    {
        switch (StageNum)
        {
            case 0:
                soundManager.PlayDialogue(2);
                break;
            case 1:
                soundManager.PlayDialogue(3);
                break;
            case 2:
                soundManager.PlayDialogue(4);
                break;
            case 3:
                soundManager.PlayDialogue(5);
                break;
            case 4:
                if( other.name != "PlatformRock")
                {
                    soundManager.PlayDialogue(6);
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
