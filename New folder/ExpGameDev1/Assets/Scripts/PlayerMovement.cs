﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;

    public float speed = 12f;
    public float skateSpeed = 60f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    bool listenToInput = true;

	// Update is called once per frame
	void Update ()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        if (listenToInput) { controller.Move(move * speed * Time.deltaTime); }

        if (listenToInput && Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        if (move.x > 0 && move.z > 0)
        {
            this.gameObject.transform.GetChild(4).GetComponent<Animator>().SetBool("isWalking", true);
        }
        else
        {
            this.gameObject.transform.GetChild(4).GetComponent<Animator>().SetBool("isWalking", false);
        }

        controller.Move(velocity * Time.deltaTime);
	}

    public void Skate()
    {
        speed = skateSpeed;
    }

    public void pauseGame()
    {
        listenToInput = false;
    }
    public void unPauseGame()
    {
        listenToInput = true;
    }
}
