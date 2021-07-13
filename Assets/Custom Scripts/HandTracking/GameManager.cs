using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject gestureText;
    public GameObject slidingWallN;
    public GameObject slidingWallW;
    public GameObject slidingWallS;
    public GameObject slidingWallE;
    public GameObject leftHandPic;
    public GameObject rightHandPic;
    public GameObject rpsPic;
    public GameObject[] gestureListObjs;
    public GameObject[] aslGuides;
    public Material[] guideMats;

    /// <summary> Dialogue index that needs to be played next </summary>
    private int currentDialogue = 0;

    /// <summary> Distance the player's arm must be extended for primary hand setting </summary>
    private readonly float extendedValue = 0.44f;

    /// <summary> Used in the demo to track how many gestures have been made in the first test </summary>
    private int gestureIndex = 0;

    /// <summary>
    /// Tracks if the demo is running. If the demo is not active, gesture coroutines stay on and turn themselves on after running
    /// </summary>
    private bool demoActive = true;

    /// <summary>
    /// Tracks if the narrator is speaking due to gesture detection and prevents overlap if so
    /// </summary>
    private bool speaking = false;

    private string[] gestureOrder = new string[6] { "Thumbs Up", "Thumbs Down", "Paper", "Rock", "Scissors", "Okay" };

    // Sets up libraries for demo
    void Start()
    {
        // Opens wall
        StartCoroutine(ScaleOverTime(slidingWallN, 3, 0.5f));
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
                StopCoroutine(DetectPrimaryHand());
                currentDialogue = 5;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;

            case 4:
                StopCoroutine(DetectPrimaryHand());
                currentDialogue = 5;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;

            /*case 5:
                currentDialogue = 6;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;*/

            case 6:
                StartCoroutine(GetNextGestureText(1));
                //StartCoroutine(ScaleOverTime(slidingWallW, 3, 0.5f));
                //StartCoroutine(DelayGestureLists(3));

                StartCoroutine(DetectThumbsUp());
                break;

            case 7: // Thumb's Up Gesture
                speaking = false;
                StopCoroutine(DetectThumbsUp());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(2));
                StartCoroutine(DetectThumbsDown());
                break;

            case 8: // Thumb's Down Gesture
                speaking = false;
                StopCoroutine(DetectThumbsDown());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(2));
                StartCoroutine(DetectPaper());
                break;

            case 9: // Paper Gesture
                speaking = false;
                StopCoroutine(DetectPaper());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(2));
                StartCoroutine(DetectRock());
                break;

            case 10: // Rock Gesture
                speaking = false;
                StopCoroutine(DetectRock());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(2));
                StartCoroutine(DetectPeace());
                break;

            case 11: // Peace Gesture
                speaking = false;
                StopCoroutine(DetectPeace());

                gestureIndex++;
                StartCoroutine(GetNextGestureText(2));
                StartCoroutine(DetectOkay());
                break;

            case 12: // Okay Gesture
                StopAllCoroutines();

                currentDialogue = 13;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
                break;

            case 13: // Final, loops to selection
                StopAllCoroutines();
                rpsPic.GetComponent<MeshRenderer>().enabled = false;
                foreach (string g in handManager.gameObject.GetComponent<SocialGestures>().GetGestureList())
                {
                    handManager.gameObject.GetComponent<SocialGestures>().SetGestureActive(g, true);
                }
                currentDialogue = 0;
                speechText.text = dialogueText[currentDialogue];
                soundManager.PlayDialogue(currentDialogue);
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
        demoActive = false;
        speechText.text = "...";

        // Cleans room
        foreach (GameObject g in gestureListObjs)
        {
            g.GetComponent<MeshRenderer>().enabled = false;
        }
        leftHandPic.GetComponent<MeshRenderer>().enabled = false;
        rightHandPic.GetComponent<MeshRenderer>().enabled = false;
        rpsPic.GetComponent<MeshRenderer>().enabled = false;
        gestureText.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(ScaleOverTime(slidingWallE, 2, 0f));
        StartCoroutine(ScaleOverTime(slidingWallW, 2, 0f));
        foreach (GameObject g in aslGuides)
        {
            g.GetComponent<MeshRenderer>().enabled = true;
        }

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

        StartCoroutine(DetectThumbsUp());
        StartCoroutine(DetectThumbsDown());
        StartCoroutine(DetectOkay());
        StartCoroutine(DetectPeace());
        StartCoroutine(DetectRock());
        StartCoroutine(DetectPaper());
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
    /// Scales wall over time seconds up to destY y
    /// </summary>
    /// <param name="wall"> Object to scale (preferably wall) </param>
    /// <param name="time"> Number of seconds </param>
    /// <param name="destY"> Desired Y value </param>
    /// <returns></returns>
    IEnumerator ScaleOverTime(GameObject wall, float time, float destY)
    {
        Vector3 originalScale = wall.transform.localScale;
        Vector3 destinationScale = new Vector3(0.5f, destY, 1f);

        float currentTime = 0.0f;

        do {
            wall.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

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
    /// Detects the okay gesture
    /// </summary>
    /// <returns></returns>
    IEnumerator DetectOkay()
    {
        yield return new WaitUntil(() => handManager.GetCurrentGesture() == "Okay" && !speaking);
        soundManager.PlayDing();
        speaking = true;
        currentDialogue = 12;
        speechText.text = dialogueText[currentDialogue];
        soundManager.PlayDialogue(currentDialogue);
    }
}
