using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;

public class FFGameManager : MonoBehaviour
{
    public String sceneName;

    public GameObject leftCon;
    public GameObject rightCon;

    public GameObject gameOverCanvas;

    public AudioSource WinSound;

    public bool hasWon;
    public bool hasLost;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLost)
        {
            gameOverCanvas.GetComponent<Text>().text = "<b>Game Over! You Lost!</b>";
        }
        if (hasWon)
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

}
