using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAOpen : MonoBehaviour
{
    public AudioSource WoodDoorSound;
    private int open;

    // Use this for initialization
    void Start()
    {
        open = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetTrigger("DoorATrigger");
        {
            open = open + 1;       
            if (open ==1)    
            WoodDoorSound.Play();
            

        } 
    }
}