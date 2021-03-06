﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;

public class RabbitRunVR : MonoBehaviour
{
    public Transform explosionEffect;
    private bool isDead = false;
    private float breakForce = 150f;
    public GameObject gameManager;
    private Animator m_animator;
    private Vector3 startPos;
    public AudioSource dieSound;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        Debug.Log(gameManager);
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        m_animator = GetComponent<Animator>();
        m_animator.SetTrigger("Next");
        m_animator.SetInteger("AnimIndex", 1);
        startPos = transform.position;
        Destroy(gameObject, 45);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
    }

    

    private float GetCollisionForce(Collision collision)
    {
        if ((collision.collider.name.Contains("Sword") && collision.collider.GetComponent<Sword>().CollisionForce() > breakForce))
        {
            return collision.collider.GetComponent<Sword>().CollisionForce() * 1.2f;
        }

        if (collision.collider.name.Contains("Arrow"))
        {
            return 500f;
        }

        return 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionForce = GetCollisionForce(collision);
        if (collisionForce > 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            if (gameObject.tag == "EvilRabbit")
            {
                gameManager.GetComponent<GameManagerScript>().IncreaseScore();
            }
            else if (gameObject.tag == "GoodRabbit")
            {
                gameManager.GetComponent<GameManagerScript>().DecreaseScore();
            }
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            dieSound.Play();
            foreach (Transform child in transform)
            {
                child.transform.gameObject.SetActive(false);
            }
            Destroy(gameObject, 2);
            isDead = true;
        }
    }




}
