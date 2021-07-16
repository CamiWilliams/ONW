using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class that manages the logic behind the game score board
 * which shows the "Ready Set Go" text, timer and winner
 * insignia.
 */
public class Scoreboard : MonoBehaviour
{
    public GameObject Stages;

    [Header("Scoreboard Objects")]
    public GameObject WinnerMenu;
    public GameObject CountDown;
    public GameObject GameMenu;
    public GameObject TimerInfo;

    [Header("Scoreboard Text Areas")]
    public GameObject CountText;
    public GameObject TimerText;

    private Text countdownText;
    private Text timerText;

    private float timeRemaining;
    private int index;
    private string[] introText = new string[6] { "Get Ready", "3", "2", "1", "Go!", "" };
    private bool gameStarted = false;

    void Start()
    {
        if (!StageManager.isStage3Done)
        {
            CountDown.SetActive(true);
            TimerInfo.SetActive(false);
            GameMenu.SetActive(false);
            WinnerMenu.SetActive(false);

            countdownText = CountText.GetComponent<Text>();
            timerText = TimerText.GetComponent<Text>();

            timeRemaining = 300;

            index = 0;
            InvokeRepeating("ReadySetGo", 1.0f, 1.5f);
        }
        else
        {
            CountDown.SetActive(false);
            TimerInfo.SetActive(false);
            GameMenu.SetActive(false);
            WinnerMenu.SetActive(false);
        }
    }

    /**
     * Update called at every frame of game.
     * 
     * Updates the time on the timer as every second passes.
     */
    void Update()
    {
        if (gameStarted)
        {
            if (timeRemaining == 300)
            {
                ShowGameStartScoreObjects();
            }

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                int mins = (int)(timeRemaining / 60);
                int secs = (int)(((timeRemaining / 60) - mins) * 60);
                string timeString = string.Format("{0:D2}:{1:D2}", mins, secs);

                timerText.text = timeString;
            }
        }
    }


    /**
     * Function that shows the ReadySetGo text.
     */
    void ReadySetGo()
    {
        countdownText.text = introText[index];
        index++;
        if (index == 6)
        {
            CancelInvoke("ReadySetGo");
            gameStarted = true;
            Stages.GetComponent<StageManager>().ActivateStage(0);
        }
    }

    /**
     * Function that shows the timer game objects and 
     * hides the "ReadySetGo" text.
     */
    void ShowGameStartScoreObjects()
    {
        CountDown.SetActive(false);
        TimerInfo.SetActive(true);
        GameMenu.SetActive(true);
        WinnerMenu.SetActive(false);
    }
}
