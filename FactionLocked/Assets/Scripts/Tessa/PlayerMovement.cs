using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float airSpeed = 3f;
    float currSpeed;
    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3f;
    bool jumpHeightReached = false;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            currSpeed = speed;
            jumpHeightReached = false;
        }
        if (!isGrounded)
        {
            currSpeed = airSpeed;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Braydon addition to run faster
        if (Input.GetKey("left shift")) {
            x *= 1.6f;
            z *= 1.6f;
        }
        
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currSpeed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Braydon addition to help with jump control
        if (!isGrounded && velocity.y <= 0f) {
            jumpHeightReached = true;
        }
        if (jumpHeightReached) {
            velocity.y += gravity * Time.deltaTime * 1.5f; // fall faster
        } else {
            velocity.y += gravity * Time.deltaTime * 1.0f; // rise slower
        }
        // ----------------------------

        controller.Move(velocity * Time.deltaTime);
        
    }
}
