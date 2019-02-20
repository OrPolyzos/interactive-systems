using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleTossMovement : MonoBehaviour
{
    public GameObject gameManager;
    public float force = 2f;
    public bool isTarget = false;
    public float zFactor = 2f;
    public AudioSource ThrowSound;
    public AudioSource HitSound;
 
    public Vector3 startPosition;
 
    private Vector2 startSwipe;
    private Vector2 endSwipe;
 
 	private bool wasMoved = false;

    public Rigidbody rigidbody;
    public GameObject tossPrefab;
 
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        startPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
    }
   
    // Update is called once per frame
    void Update () {
        if (!wasMoved) {
            if (Vector3.Distance(startPosition, transform.position) > 2f)  {
                Invoke("Spawn", 1);
                wasMoved = true;
            }
            if (Input.GetMouseButtonDown(0))
	        {
	            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	        }
	 
	        if (Input.GetMouseButtonUp(0))
	        {
	            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	            if (isTarget == true)
	            {
                    wasMoved = true;
                    isTarget = false;
                    Swipe();
                    ThrowSound.Play();
	            }
	        }
    	}
    }
 
    void Swipe()
    {
        Vector3 swipe = endSwipe - startSwipe;
        swipe.z = swipe.y / zFactor;

        rigidbody.AddForce(swipe * force, ForceMode.Impulse);
        //Invoke("Spawn", 1);
    }

    void Spawn() {
        Debug.Log("asdas");
        Instantiate(tossPrefab, startPosition, transform.rotation);
        Destroy(gameObject, 3);
    }
 
    private void OnMouseDown()
    {
        rigidbody.constraints = RigidbodyConstraints.None;
        isTarget = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bottle")
        {
            gameManager.GetComponent<GameManagerScript>().IncreaseScore();
            collision.gameObject.tag = "None";
            HitSound.Play();
        }
    }
}
