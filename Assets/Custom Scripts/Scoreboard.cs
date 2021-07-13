using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public GameObject Stages;

    [Header("Scoreboard Objects")]
    public GameObject CountText;
    public GameObject TimerText;
    public GameObject TimerImage;
    public GameObject MenuText;
    public GameObject ButtonImage;

    private Text countdownText;
    private Text timerText;

    private float timeRemaining;
    private int index;
    private string[] introText = new string[6] { "Get Ready", "3", "2", "1", "Go!", "" };
    private bool gameStarted = false;

    void Start()
    {
        CountText.SetActive(true);
        TimerText.SetActive(false);
        TimerImage.SetActive(false);
        MenuText.SetActive(false);
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
