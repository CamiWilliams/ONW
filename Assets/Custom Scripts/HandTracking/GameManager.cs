using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Manager References")]
    public HandManager handManager;
    public SoundManager soundManager;

    [Header("Displayed Dialogue")]
    [TextArea]
    public string[] dialogueText;

    [Header("Manipulated Objects")]
    public TextMesh speechText;
    public GameObject leftHandPic;
    public GameObject rightHandPic;

    private readonly float extendedValue = 0.44f;
    private int gestureIndex = 0;

    private int currentDialogue = 0;
    private bool speaking = false;
    private string[] gestureOrder = new string[6] { "Thumbs Up", "Thumbs Down", "Paper", "Rock", "Scissors", "Thumbs Up" };

    // Sets up libraries for demo
    void Start()
    {
        StartCoroutine(DelayDialogue(2, currentDialogue));
    }

    /// <summary>
    /// Called when dialogue finishes playing in SoundManager
    /// </summary>
    /// <param name="i"> Dialogue index that JUST played </param>
    public void DialogueFinished(int i)
    {
        switch (i)
        {
            case 1:
                currentDialogue = 2;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;

            case 2:
                StartCoroutine(DetectPrimaryHand());
                break;

            case 3:
                currentDialogue = 5;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;

            case 4:
                currentDialogue = 5;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;

            case 5:
                StopAllCoroutines();
                currentDialogue = 6;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;

            case 6:
                StartCoroutine(GetNextGestureText(1));

                StartCoroutine(DetectThumbsUp());
                break;

            case 7: // Thumb's Up Gesture
                speaking = false;
                StopCoroutine(DetectThumbsUp());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(1));
                StartCoroutine(DetectThumbsDown());
                break;

            case 8: // Thumb's Down Gesture
                speaking = false;
                StopCoroutine(DetectThumbsDown());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(1));
                StartCoroutine(DetectPaper());
                break;

            case 9: // Paper Gesture
                speaking = false;
                StopCoroutine(DetectPaper());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(1));
                StartCoroutine(DetectRock());
                break;

            case 10: // Rock Gesture
                speaking = false;
                StopCoroutine(DetectRock());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(1));
                StartCoroutine(DetectPeace());
                break;

            case 11: // Peace Gesture
                speaking = false;
                StopCoroutine(DetectPeace());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(1));
                StartCoroutine(DetectPinkyPromise());
                break;

            case 12: // Waving Gesture
                StopAllCoroutines();

                currentDialogue = 13;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;

            case 13: // Go back to main scene
                StopAllCoroutines();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                break;

            default:
                currentDialogue++;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;
        }
    }

    /// <summary>
    /// Ends the demo and activates Free Play mode
    /// </summary>
    public void EndDemo()
    {
        speechText.text = "...";

        leftHandPic.GetComponent<MeshRenderer>().enabled = false;
        rightHandPic.GetComponent<MeshRenderer>().enabled = false;

        // Turns on all gestures
        handManager.gameObject.GetComponent<SocialGestures>().enabled = true;
        foreach (string g in handManager.gameObject.GetComponent<SocialGestures>().GetGestureList())
        {
            handManager.gameObject.GetComponent<SocialGestures>().SetGestureActive(g, true);
        }

        handManager.gameObject.GetComponent<ASLphabetGestures>().enabled = true;
        foreach (string g in handManager.gameObject.GetComponent<ASLphabetGestures>().GetGestureList())
        {
            handManager.gameObject.GetComponent<ASLphabetGestures>().SetGestureActive(g, true);
        }
    }




    /// <summary>
    /// Delays dialogue by secs seconds
    /// </summary>
    /// <param name="secs"> Number of seconds to delay dialogue </param>
    /// <param name="dialogue"> Dialogue index to play </param>
    /// <returns></returns>
    IEnumerator DelayDialogue(int secs, int dialogue)
    {
        yield return new WaitForSeconds(secs);

        currentDialogue = dialogue;
        speechText.text = dialogueText[currentDialogue];
        soundManager.PlayDialogue(currentDialogue);
    }

    /// <summary>
    /// Grabs the hand that is extended and sets it as the PrimaryHand
    /// </summary>
    /// <returns></returns>
    IEnumerator DetectPrimaryHand()
    {
        yield return new WaitUntil(() => handManager.SecondaryLocation().x > extendedValue || handManager.PrimaryLocation().x > extendedValue);

        soundManager.PlayDing();

        if (handManager.SecondaryLocation().x > extendedValue)
        {
            handManager.SetPrimaryHand(HandManager.HandType.Left);
            currentDialogue = 3;
            speechText.text = dialogueText[currentDialogue];
            soundManager.PlayDialogue(currentDialogue);
        }
        else
        {
            handManager.SetPrimaryHand(HandManager.HandType.Right);
            currentDialogue = 4;
            speechText.text = dialogueText[currentDialogue];
            soundManager.PlayDialogue(currentDialogue);
        }
    }

    /// <summary>
    /// Updates the text to show the next gesture
    /// </summary>
    /// <param name="secs"> Number of seconds to delay by </param>
    /// <returns></returns>
    IEnumerator GetNextGestureText(int secs)
    {
        yield return new WaitForSeconds(secs);
        speechText.text = gestureOrder[gestureIndex];
    }


    /// <summary>
    /// Detects the thumb's up gesture
    /// </summary>
    /// <returns></returns>
    IEnumerator DetectThumbsUp()
    {
        yield return new WaitUntil(() => handManager.GetCurrentGesture() == "Thumb's Up" && !speaking);
        soundManager.PlayDing();
        speaking = true;
        currentDialogue = 7;
        speechText.text = dialogueText[currentDialogue];
        soundManager.PlayDialogue(currentDialogue);
    }

    /// <summary>
    /// Detects the thumb's down gesture
    /// </summary>
    /// <returns></returns>
    IEnumerator DetectThumbsDown()
    {
        yield return new WaitUntil(() => handManager.GetCurrentGesture() == "Thumb's Down" && !speaking);
        soundManager.PlayDing();
        speaking = true;
        currentDialogue = 8;
        speechText.text = dialogueText[currentDialogue];
        soundManager.PlayDialogue(currentDialogue);
    }

    /// <summary>
    /// Detects the paper gesture
    /// </summary>
    /// <returns></returns>
    IEnumerator DetectPaper()
    {
        yield return new WaitUntil(() => handManager.GetCurrentGesture() == "Paper" && !speaking);
        speaking = true;
        currentDialogue = 9;
        speechText.text = dialogueText[currentDialogue];
        soundManager.PlayDialogue(currentDialogue);
    }

    /// <summary>
    /// Detects the rock gesture
    /// </summary>
    /// <returns></returns>
    IEnumerator DetectRock()
    {
        yield return new WaitUntil(() => handManager.GetCurrentGesture() == "Rock" && !speaking);
        speaking = true;
        currentDialogue = 10;
        speechText.text = dialogueText[currentDialogue];
        soundManager.PlayDialogue(currentDialogue);
    }

    /// <summary>
    /// Detects the peace gesture
    /// </summary>
    /// <returns></returns>
    IEnumerator DetectPeace()
    {
        yield return new WaitUntil(() => handManager.GetCurrentGesture() == "Peace" && !speaking);
        soundManager.PlayDing();
        speaking = true;
        currentDialogue = 11;
        speechText.text = dialogueText[currentDialogue];
        soundManager.PlayDialogue(currentDialogue);
    }

    /// <summary>
    /// Detects the waving gesture
    /// </summary>
    /// <returns></returns>
    IEnumerator DetectPinkyPromise()
    {
        //yield return new WaitUntil(() => handManager.GetCurrentGesture() == "Pinky Promise" && !speaking);
        yield return new WaitUntil(() => handManager.GetCurrentGesture() == "Thumb's Up" && !speaking);
        soundManager.PlayDing();
        speaking = true;
        currentDialogue = 12;
        speechText.text = dialogueText[currentDialogue];
        soundManager.PlayDialogue(currentDialogue);
    }
}
