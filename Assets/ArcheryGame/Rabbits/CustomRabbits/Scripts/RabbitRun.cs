using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitRun : MonoBehaviour
{
    public Transform explosionEffect;
    private bool isDead = false;

    private Animator m_animator;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetTrigger("Next");
        m_animator.SetInteger("AnimIndex", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Die();
    }

    public void Die()
    {
        if (!isDead)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 0);
        }
    }
}
