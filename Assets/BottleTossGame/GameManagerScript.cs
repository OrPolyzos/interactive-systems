using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public String sceneName;
    public GameObject scoreCanvas;
    public GameObject timeCanvas;
    public GameObject gameOverCanvas;

    private int score = 0;
    public int maxScore = 20;
    public float timeLeftInSec = 5 * 60;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CountDown", 0.0F, 1.0F);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeftInSec <= 0)
        {
            gameOverCanvas.GetComponent<Text>().text = "<b>You Lost!</b>\n<color=\"White\"> Press R to play again</color>\n<color=\"Red\">Press ESC to go back</color>";
            gameOverCanvas.SetActive(true);
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("RoomScene");
            }
            else if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(sceneName);
            }
            return;
        }
        else if (score == maxScore)
        {
            gameOverCanvas.GetComponent<Text>().text = "<b>Congratulations! You Won!</b>\n<color=\"White\"> Press R to play again</color>\n<color=\"Red\">Press ESC to go back</color>";
            gameOverCanvas.SetActive(true);
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("RoomScene");
            }
            else if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(sceneName);
            }
            return;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeftInSec);
        string time = string.Format("{0:D2}m:{1:D2}s",
                        timeSpan.Minutes,
                        timeSpan.Seconds);
        timeCanvas.GetComponent<Text>().text = time;
        scoreCanvas.GetComponent<Text>().text = string.Format("{0}/{1} bottles", score, maxScore);
    }

    void CountDown()
    {
        if (timeLeftInSec > 0)
        {
            timeLeftInSec--;
        }
    }

    public void IncreaseScore()
    {
        if (score < maxScore)
        {
            score++;
        }
    }
}
