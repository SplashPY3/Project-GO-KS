using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // all the necessary variables

    public CharacterController controller;

    public float speed = 8;
    public float gravity = -50f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // groundcheck

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -6f; // tiny speed added to stop the player from constantly gathering more speed
        }

        float x = Input.GetAxis("Horizontal"); // user keyboard input
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; // easy movement
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // jump formula
        }

        velocity.y += gravity * Time.deltaTime; // gravity
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown("left shift"))
        {
            Debug.Log("Shift down");
            speed -= 4;
        }

        if (Input.GetKeyUp("left shift"))
        {
            Debug.Log("Shift up");
            speed += 4;
        }
    }
}