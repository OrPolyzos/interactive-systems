﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityInteraction : MonoBehaviour
{
    private GameObject interactionCanvas;
    private GameObject player;
    //private static GameObject instructionCanvas = GameObject.FindWithTag("InstructionCanvas");

    public float distance = 2f;

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
            interactionCanvas.GetComponent<Text>().enabled = true;
            wasEnabledByMe = true;
        }
        else if (wasEnabledByMe)
        {
            interactionCanvas.GetComponent<Text>().enabled = false;
            wasEnabledByMe = false;
        }
    }
}
