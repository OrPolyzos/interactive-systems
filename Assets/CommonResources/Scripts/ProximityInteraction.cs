using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;

public class ProximityInteraction : MonoBehaviour
{
    public string sceneName;
    private GameObject interactionCanvas;
    private readonly string interactionCanvasText = "<color=\"white\"><b>Play</b></color> <color=\"{0}\">(F)</color>";
    private GameObject player;


    public float distance = 2.5f;
    public string interactionCanvasTextColor = "black";

    private GameObject myself;
    public GameObject leftController;
    public GameObject rightController; 

    private bool wasEnabledByMe = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
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
            if (Input.GetKeyDown(KeyCode.F) || leftController.GetComponent<VRTK_ControllerEvents>().touchpadPressed || rightController.GetComponent<VRTK_ControllerEvents>().touchpadPressed)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
        else if (wasEnabledByMe)
        {
            interactionCanvas.GetComponent<Text>().enabled = false;
            wasEnabledByMe = false;
        }
    }
}
