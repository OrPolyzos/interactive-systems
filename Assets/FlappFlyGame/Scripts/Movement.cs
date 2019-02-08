using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour 
{
    private static readonly float MAX_POS_Y = 110F;
    private static readonly float TOP_DELTA_VALUE = 400F;
    private static readonly float ROTATION_SPEED = 250F;
    private static readonly float ROTATION_SPEED_LIMIT = 50F;
    private static readonly float ROTATION_MAX_ANGLE = 0.3F;
    private static readonly float PLAYER_SPEED = 0.15F;

    public Rigidbody player;
    public AudioSource flapSound;
    public AudioSource collisionSound;

    public Transform leftWing;
    public Transform rightWing;

    private Quaternion initialLeftWing;
    private Quaternion initialRifghtWing;

    private float waitTime = 1f;
    private float timer = 0.0f;
    int rotationDirection = 1;

    private Vector3 lastPos = Vector3.zero;

    void Start()
    {
        initialLeftWing = leftWing.transform.rotation;
        initialRifghtWing = rightWing.transform.rotation;
    }


    void Update()
    {
        float posY = Mathf.Clamp(transform.position.y, 0, MAX_POS_Y);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z + PLAYER_SPEED);

        float delta = System.Math.Min(TOP_DELTA_VALUE, System.Math.Abs((Input.mousePosition - lastPos).y));

        if (leftWing.transform.rotation.x < -ROTATION_MAX_ANGLE || leftWing.transform.rotation.x > ROTATION_MAX_ANGLE)
        {
            leftWing.rotation = initialLeftWing;
            rightWing.rotation = initialRifghtWing;
        }

        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            rotationDirection = -1 * rotationDirection;
            timer = timer - waitTime;
        }

        flapSound.Play();
        
        leftWing.Rotate(new Vector3(0, Mathf.Clamp((-rotationDirection * Vector3.down * Time.deltaTime * delta * ROTATION_SPEED).y, -ROTATION_SPEED_LIMIT, ROTATION_SPEED_LIMIT), 0));
        rightWing.Rotate(new Vector3(0, Mathf.Clamp((rotationDirection * Vector3.down * Time.deltaTime * delta * ROTATION_SPEED).y, -ROTATION_SPEED_LIMIT, ROTATION_SPEED_LIMIT), 0));
        player.AddForce(new Vector3(0, delta, 0));

        lastPos = Input.mousePosition;
    }
    void OnCollisionEnter(Collision collision)
    {   
        collisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("FlappyFly");
    }

    
}
