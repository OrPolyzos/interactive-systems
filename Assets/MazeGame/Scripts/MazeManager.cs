using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MazeManager : MonoBehaviour
{
    public string sceneName;
    public GameObject timeCanvas;
    public GameObject gameOverCanvas;
    public AudioSource WinSound;
    public GameObject player;

    public float timeLeftInSec = 5 * 60;
    public bool hasWon = false;
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
        if (timeLeftInSec <= 0)
        {
            player.GetComponent<FirstPersonController>().enabled = false;
            gameOverCanvas.GetComponent<Text>().text = "<b>You Lost!</b>\n<color=\"White\"> Press R to play again</color>\n<color=\"Red\">Press ESC to go back</color>";
            gameOverCanvas.SetActive(true);
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("RoomScene", LoadSceneMode.Single);
            }
            else if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            return;
        }
        else if (hasWon)
        {
            player.GetComponent<FirstPersonController>().enabled = false;
            gameOverCanvas.GetComponent<Text>().text = "<b>Congratulations! You Won!</b>\n<color=\"White\"> Press R to play again</color>\n<color=\"Red\">Press ESC to go back</color>";
            gameOverCanvas.SetActive(true);
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("RoomScene", LoadSceneMode.Single);
            }
            else if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            return;
        }
    }

    void CountDown()
    {
        if (timeLeftInSec > 0)
        {
            timeLeftInSec--;
        }
    }

    public void IncreaseTime(float seconds)
    {
        this.timeLeftInSec += seconds;
    }

    public void DecreaseTime(float seconds)
    {
        this.timeLeftInSec -= seconds;
    }
}
