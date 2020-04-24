using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;

    private float verticalVelocity;
    public float jumpForce = 10.0f;
    public float gravity = 14.0f;
    public GameObject Camera;

    private float jumpCount = 0;
    private CharacterController controller;
    public float extraMove = 0;


    // What is our states?
    // Every possible location on the screen..

    // Actions (-1, 0, 1): we can move left, none, right
    





    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    
    // Update is called once per frame
    void Update() {

        print("Is Grounded? " + controller.isGrounded);
        verticalVelocity -= gravity * 2 * Time.deltaTime;
        ////---Player Movement and Jump Controls---\\\\
        if (controller.isGrounded)
        {

            /* verticalVelocity Needs to be constant, due to frame rate variation.
             * My Time.deltaTime could be less than what controller.isGrounded considers a 'move'
             *    (measured by a change in distance)
             * and it would flip it back to false.. Thus missing jump commands
             */
            verticalVelocity = -1f;//-gravity * 2 * Time.deltaTime;


            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        
        // AI controls \\
        






        // player controlling: So that the user can override the bots actions 
        Vector3 moveVector = Vector3.zero;
        if (Input.GetAxis("Horizontal") == 0)
        {
            //moveVector.x = extraMove;
        } else
        { 
            moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
        }
        //print("verticalVelo: " + verticalVelocity);
        //print("Our pos y:" + transform.position.y);

        moveVector.y = verticalVelocity;
        moveVector.z = 0;
        controller.Move(moveVector * Time.deltaTime);
        /////////////////////------\\\\\\\\\\\\\\\\\\\\\\
        print("/////////////////////");
    }

}
