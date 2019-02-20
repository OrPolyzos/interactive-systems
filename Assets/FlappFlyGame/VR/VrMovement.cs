using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VrMovement : MonoBehaviour
{
    public GameObject gameManager;
    private static readonly float MAX_POS_Y = 110F;
    private static readonly float TOP_DELTA_VALUE = 400F;
    private static readonly float ROTATION_SPEED = 250F;
    private static readonly float ROTATION_SPEED_LIMIT = 50F;
    private static readonly float ROTATION_MAX_ANGLE = 0.3F;
    private static readonly float PLAYER_SPEED = 0.25F;

    public Rigidbody player;
    public AudioSource flapSound;
    public AudioSource collisionSound;
    public AudioSource WinSound;
    public Transform leftWing;
    public Transform rightWing;

    private Quaternion initialLeftWing;
    private Quaternion initialRifghtWing;

    public GameObject rightController;
    public GameObject leftController;

    SteamVR_TrackedObject leftTracked;
    SteamVR_Controller.Device leftDevice;
    SteamVR_TrackedObject rightTracked;
    SteamVR_Controller.Device rightDevice;

    private float waitTime = 1f;
    private float timer = 0.0f;
    int rotationDirection = 1;

    private Vector3 lastPos = Vector3.zero;
    private bool lost;
    private bool won;

    private Vector3 startLeft;

    void Start()
    {
        leftTracked = leftController.GetComponent<SteamVR_TrackedObject>();
        rightTracked = rightController.GetComponent<SteamVR_TrackedObject>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        initialLeftWing = leftWing.transform.rotation;
        initialRifghtWing = rightWing.transform.rotation;
    }


    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PLAYER_SPEED);


        leftDevice = SteamVR_Controller.Input((int)leftTracked.index);
        rightDevice = SteamVR_Controller.Input((int)rightTracked.index);
        float leftVelocity = leftDevice.velocity.magnitude;
        float rightVelocity = rightDevice.velocity.magnitude;
        float velocity = Mathf.Max(leftVelocity, rightVelocity);
        if (leftVelocity > 1F)
        {
            player.AddForce(new Vector3(0, leftVelocity * 25, 0));
        }



        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            rotationDirection = -1 * rotationDirection;
            timer = timer - waitTime;
        }

        flapSound.Play();
        if (leftWing.transform.rotation.x < -ROTATION_MAX_ANGLE || leftWing.transform.rotation.x > ROTATION_MAX_ANGLE)
        {
            leftWing.rotation = initialLeftWing;
            rightWing.rotation = initialRifghtWing;
        }
        leftWing.Rotate(new Vector3(0, Mathf.Clamp((-rotationDirection * Vector3.down * Time.deltaTime * leftVelocity * ROTATION_SPEED).y, -ROTATION_SPEED_LIMIT, ROTATION_SPEED_LIMIT), 0));
        rightWing.Rotate(new Vector3(0, Mathf.Clamp((rotationDirection * Vector3.down * Time.deltaTime * rightVelocity * ROTATION_SPEED).y, -ROTATION_SPEED_LIMIT, ROTATION_SPEED_LIMIT), 0));
        //Debug.Log(Input.mousePosition);
        if (lost)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameManager.GetComponent<FFGameManager>().hasLost = true;
        }
        else if (won)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameManager.GetComponent<FFGameManager>().hasWon = true;
        }

        lastPos = leftController.GetComponent<SteamVR_TrackedObject>().transform.position;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "End")
        {
            won = true;
            WinSound.Play();
        }
        else
        {
            lost = true;
            collisionSound.Play();
        }
    }


}
