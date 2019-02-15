using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
	public Rigidbody rb;
    public float force = 2f;
    public bool isTarget = false;
    public float zFactor = 2f;
 
    public Vector3 startPosition;
 
    private Vector2 startSwipe;
    private Vector2 endSwipe;
 
 	private bool wasMoved = false;
    public GameObject ballObject;
 
    // Use this for initialization
    void Start () {
        startPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
    }
   
    // Update is called once per frame
    void Update () {
    	if(!wasMoved) {

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
	                Swipe();
	                isTarget = false;
	            }
	        }
    	}
    }
 
    void Swipe()
    {
        Vector3 swipe = endSwipe - startSwipe;
        swipe.z = swipe.y / zFactor;
 
        rb.AddForce(swipe * force, ForceMode.Impulse);

		Invoke("Spawn", 2);
    }
    void Spawn() {
        Instantiate(ballObject, startPosition, transform.rotation);
        Destroy(gameObject, 5);
    }
 
    private void OnMouseDown()
    {
        rb.constraints = RigidbodyConstraints.None;
        isTarget = true;
    }
}
