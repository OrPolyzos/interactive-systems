using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProximityInteraction : MonoBehaviour
{
    public string sceneName;
    private GameObject interactionCanvas;
    private readonly string interactionCanvasText = "<color=\"white\"><b>Play</b></color> <color=\"{0}\">(F)</color>";
    private GameObject player;


    public float distance = 2.5f;
    public string interactionCanvasTextColor = "black";

    private GameObject myself;

    private bool wasEnabledByMe = false;

    void Start()
    { 
        interactionCanvas = GameObject.FindWithTag("InteractionCanvas");
        player = GameObject.FindWithTag("Player");
        myself = transform.gameObject;
    }

    void Update()
    { 
        if (Vector3.Distance(player.transform.position, myself.transform.position) < distance)
        {
            interactionCanvas.GetComponent<Text>().text = string.Format(interactionCanvasText, interactionCanvasTextColor);
            interactionCanvas.GetComponent<Text>().enabled = true;
            wasEnabledByMe = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        else if (wasEnabledByMe)
        {
            interactionCanvas.GetComponent<Text>().enabled = false;
            wasEnabledByMe = false;
        }
    }
}
