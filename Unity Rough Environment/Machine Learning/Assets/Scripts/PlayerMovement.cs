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



    // Agent related stuff.
    enum Action {
        Left    = -1,
        None    = 0,
        Right   = 1,
        Jump    = 2
    }



    private Stack<Action> actions;
    private Vector2 state; // this will store the distance to the goal.
    private Vector2 goal; // 



    // What is our states? (x, y) axis position
    // Actions (-1, 0, 1): we can move left, none, right
    // How do we know if we reached the goal state?  if we reached the same position as goal.




    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
        state = transform.position;
        goal = new Vector2(120f, 1f);
    }

    
    // Update is called once per frame
    void Update() {

        //print("Is Grounded? " + controller.isGrounded);
        state = transform.position;

        verticalVelocity -= gravity * 2 * Time.deltaTime;
        ////---Player Movement and Jump Controls---\\\\
        if (controller.isGrounded)
        {

            /* verticalVelocity needs to be constant, due to frame rate variation.
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

        Vector3 moveVector = Vector3.zero;

        if (!this.Is_done(state)) {
            // AI controls \\
            Action nextAct = Action.None;
            float rewardForNothing = GetReward(Action.None);
            float rewardForLeft = GetReward(Action.Left);
            float rewardForRight = GetReward(Action.Right);

            // if going left is good
            if (rewardForLeft > 0 && rewardForRight <= 0) {
                nextAct = Action.Left;
            } else if (rewardForRight > 0 && rewardForLeft <= 0) {
                nextAct = Action.Right;
            }
            moveVector.x = ((int)nextAct) * moveSpeed;

            // Determine if we need to jump. Given if we are on the ground, of course..
            if (controller.isGrounded) {

                //print("Checking if we need to jump...");

                Vector3 pntCheck = new Vector3((transform.position.x + ((int)nextAct) * 1.0f + (moveVector.x * Time.deltaTime)), transform.position.y - 1.0f); // y: was -0.5;


                //print("Current position: " + transform.position);
                //print("Checking position: " + pntCheck);


                // if the position is not occupid, then we also jump
                if (!Physics.CheckSphere(pntCheck, 0.2f)) {
                    print("we jumping");
                    verticalVelocity = jumpForce;
                }
            }

        } else {
            print("We are done");
        }


        



        // player controlling: So that the user can override the bots actions 
        if (Input.GetAxis("Horizontal") == 0) {
            //moveVector.x = extraMove;
        } else { 
            moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
        }
        //print("verticalVelo: " + verticalVelocity);
        //print("Our pos y:" + transform.position.y);

        moveVector.y = verticalVelocity;
        moveVector.z = 0;
        controller.Move(moveVector * Time.deltaTime);
        /////////////////////------\\\\\\\\\\\\\\\\\\\\\\
        //print("/////////////////////");
    }




    // Related functions.

    // returns the reward for the given action
    private float GetReward(Action action) {
        Vector2 currentPos = transform.position;
        Vector2 resultAction = this.GetResultOfAction(action);

        float cDist = Vector2.Distance(this.goal, currentPos);
        float rDist = Vector2.Distance(this.goal, resultAction);
        float res = -(rDist - cDist);

        return res;
    }



    private bool Is_done(Vector2 st) {
        Vector2 cpState = new Vector2(Mathf.Floor(st.x), Mathf.Floor(st.y));
        Vector2 cpGoal = new Vector2(Mathf.Floor(goal.x), Mathf.Floor(goal.y));
        return cpState.Equals(cpGoal);
    }

    private Vector2 GetResultOfAction(Action act) {

        Vector2 cPos = transform.position; // this drops the z axis automatically

        if (act != Action.Jump) {
            int direction = (int) act;
            
            cPos.x += direction * (moveSpeed * Time.deltaTime);
        } else {

        }

        return cPos;
    }



















}
