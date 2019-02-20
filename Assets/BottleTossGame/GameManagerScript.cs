using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;

public class GameManagerScript : MonoBehaviour
{
    public String itemsText;
    public String sceneName;

    public GameObject leftCon;
    public GameObject rightCon;


    public GameObject scoreCanvas;
    public GameObject timeCanvas;
    public GameObject gameOverCanvas;
    public AudioSource WinSound;
    private int score = 0;
    public int maxScore = 1;
    public float timeLeftInSec = 5 * 60;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CountDown", 0.0F, 1.0F);
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeftInSec);
        string time = string.Format("{0:D2}m:{1:D2}s",
                        timeSpan.Minutes,
                        timeSpan.Seconds);
        timeCanvas.GetComponent<Text>().text = time;
        scoreCanvas.GetComponent<Text>().text = string.Format("{0}/{1} {2}", score, maxScore, itemsText);
        if (timeLeftInSec <= 0)
        {
            gameOverCanvas.GetComponent<Text>().text = "<b>Game Over! You Lost!</b>";
        }
        else if (score == maxScore)
        {
            gameOverCanvas.GetComponent<Text>().text = "<b>Congratulations! You Won!</b>";
        }
        if (leftCon.GetComponent<VRTK_ControllerEvents>().touchpadPressed)
        {
            SceneManager.LoadScene("RoomScene", LoadSceneMode.Single);
        }
        else if (rightCon.GetComponent<VRTK_ControllerEvents>().touchpadPressed)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

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

        if (score == maxScore)
        {
            WinSound.Play();
        }
    }

    public void DecreaseScore()
    {
        if (score > 0)
        {
            score--;
        }
    }
}
