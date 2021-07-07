using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public GameObject Stages;

    private GameObject CountText;
    private GameObject TimerText;
    private GameObject TimerImage;
    private GameObject MenuText;
    private GameObject ButtonImage;

    private Text countdownText;
    private Text timerText;

    private float timeRemaining;
    private int index;
    private string[] introText = new string[6] { "Get Ready", "3", "2", "1", "Go!", "" };
    private bool gameStarted = false;

    void Start()
    {
        CountText = GameObject.Find("CountText");
        CountText.SetActive(true);
        TimerText = GameObject.Find("TimerText");
        TimerText.SetActive(false);
        TimerImage = GameObject.Find("TimerImage");
        TimerImage.SetActive(false);
        MenuText = GameObject.Find("MenuText");
        MenuText.SetActive(false);
        ButtonImage = GameObject.Find("ButtonImage");
        ButtonImage.SetActive(false);

        countdownText = CountText.GetComponent<Text>();
        timerText = TimerText.GetComponent<Text>();

        timeRemaining = 300;

        index = 0;
        InvokeRepeating("ReadySetGo", 1.0f, 1.5f);
    }

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

    void ShowGameStartScoreObjects()
    {
        CountText.SetActive(false);
        TimerText.SetActive(true);
        TimerImage.SetActive(true);
        MenuText.SetActive(true);
        ButtonImage.SetActive(true);
    }

    void ReadySetGo()
    {
        countdownText.text = introText[index];
        index++;
        if (index == 6)
        {
            CancelInvoke("ReadySetGo");
            gameStarted = true;
            Debug.Log("One Thread");
            Stages.GetComponent<StageManager>().ActivateStage(0);
        }
    }
}
