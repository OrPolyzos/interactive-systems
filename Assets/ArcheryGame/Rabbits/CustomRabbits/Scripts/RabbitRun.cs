using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitRun : MonoBehaviour
{

    private string[] m_buttonNames = new string[] { "Idle", "Run", "Dead" };

    private Animator m_animator;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        // starts running
        m_animator.SetTrigger("Next");
        m_animator.SetInteger("AnimIndex", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //If this tag is evilrabbit add points, if this tag is good remove points
        m_animator.SetTrigger("Next");
        m_animator.SetInteger("AnimIndex", 3);
        Destroy(gameObject, 2);
    }
}
