using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressurePad : MonoBehaviour
{
    public GameObject player;
    public GameObject doorToSlide;
    public float steptoSlideUp = 0.01F;
    public float steptoSlideDown = 0.01F;

    public float distance = 1f;
    private float doorUpperLimit;
    private float doorLowerLimit;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        doorUpperLimit = doorToSlide.transform.position.y + doorToSlide.transform.lossyScale.y * 2;
        doorLowerLimit = doorToSlide.transform.position.y;
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            float y = doorToSlide.transform.position.y;
            if (y < doorUpperLimit)
            {
                y = y + steptoSlideUp;
            }
            doorToSlide.transform.position = new Vector3(doorToSlide.transform.position.x, y, doorToSlide.transform.position.z);
        }
        else
        {
            float y = doorToSlide.transform.position.y;
            if (y >= doorLowerLimit)
            {
                y = y - steptoSlideDown;
            }
            doorToSlide.transform.position = new Vector3(doorToSlide.transform.position.x, y, doorToSlide.transform.position.z);
        }


    }
}