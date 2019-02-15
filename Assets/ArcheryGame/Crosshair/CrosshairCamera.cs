using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Visyde
{
    public class CrosshairCamera : MonoBehaviour
    {

        public AudioSource arrowHit;
        public AudioSource arrowMiss;
        // For the FPS mouse look:
        float rotationX = 0F;
        float rotationY = 0F;

        // For the raycasting function:
        Vector3 fireDirection;
        Vector3 firePoint;

        public GameObject scoreText;
        public int maxPlayerScore = 15;
        private int playerScore;

        // Use this for initialization
        void Start()
        {
            // Lock and hide the cursor:
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            // FPS mouse look:
            rotationX += Input.GetAxis("Mouse X") * 2;
            rotationY -= Input.GetAxis("Mouse Y") * 2;
            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
            transform.rotation = rotation;

            // Call raycast method:
            if (playerScore == maxPlayerScore)
            {
                scoreText.GetComponent<Text>().text = "<color=\"Red\">You win!</color>";
                return;
            }
            Hit();
            scoreText.GetComponent<Text>().text = "Score: " + playerScore + "/" + maxPlayerScore;
        }

        void Hit()
        {
            // Raycasting variables:
            RaycastHit hit;
            fireDirection = transform.TransformDirection(Vector3.forward) * 10;
            firePoint = transform.position;

            // Do raycasting:
            if (Physics.Raycast(firePoint, (fireDirection), out hit, Mathf.Infinity))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.tag == "GoodRabbit")
                    {
                        if (playerScore > 0)
                        {
                            playerScore--;
                        }
                        arrowHit.Play();
                        hit.transform.gameObject.GetComponent<RabbitRun>().Die();
                    }
                    else if (hit.transform.tag == "EvilRabbit")
                    {
                        if (playerScore < maxPlayerScore)
                        {
                            playerScore++;
                        }
                        arrowHit.Play();
                        hit.transform.gameObject.GetComponent<RabbitRun>().Die();

                    }
                    else
                    {
                        arrowMiss.Play();
                    }
                }
            }

            // Debug the ray out in the editor:
            Debug.DrawRay(firePoint, fireDirection, Color.green);
        }
    }
}
