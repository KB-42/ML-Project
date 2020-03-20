using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;

    private float verticalVelocity;
    public float jumpForce = 10.0f;
    public float gravity = 14.0f;

    private float jumpCount = 0;
    private CharacterController controller;
    public float extraMove = 0;

    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    public GameObject Camera;
    // Update is called once per frame
    void Update() {
        
        ////---Player Movement and Jump Controls---\\\\
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
            verticalVelocity -= gravity * 2 * Time.deltaTime;

        Vector3 moveVector = Vector3.zero;
        if (Input.GetAxis("Horizontal") == 0)
        {
            moveVector.x = extraMove;
        } else
        { 
            moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
        }
        moveVector.y = verticalVelocity;
        moveVector.z = 0;
        controller.Move(moveVector * Time.deltaTime);
        /////////////////////------\\\\\\\\\\\\\\\\\\\\\\

    }

}
