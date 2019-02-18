﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseTimeOnTouch : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1F)
        {
            gameManager.GetComponent<MazeManager>().IncreaseTime(30);
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.up, 100F * Time.deltaTime);
    }

}