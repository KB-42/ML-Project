using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {



    public float velocity = 1;
    public float distance;
    private bool moveLeft = false;
    private float objectPosition;

    //\\//\\//\\//\\//\\--Moves the Player When They land on the platform--//\\//\\//\\//\\//\\
    PlayerMovement PlayerMovement;
    private void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();//gets the script from the player object
        objectPosition = this.transform.position.x;
    }

    // Update is called once per frame
    void Update ()
    {
        var x = velocity * Time.deltaTime;
       // GameObject.FindWithTag("ForwardMoving").transform.Translate(x, 0, 0);

        ////--Platform Object Moves left then Right after reaching a certain distance--\\\\
        if (moveLeft == false)//object with tag left/right moves right while position is less than distance
        {
            this.transform.Translate(x, 0, 0);
            
            if (this.transform.position.x >= objectPosition+distance)//checks to see if tag reached or went beyond distance
                moveLeft = true;//falsifys the if statement preventing it from running

        }
        else
        {
            this.transform.Translate(-x, 0, 0);//sends platform into negative direction
            if (this.transform.position.x <= objectPosition-distance)//checks to see if tag reached or went beyond negative distance
                moveLeft = false;//reactivates if statement
        }
    }

 
    private void OnTriggerStay(Collider other)
    {
        var x = velocity;
        if (other.gameObject.name == "Player")
        {
            if (moveLeft == false)
            {
                PlayerMovement.extraMove = x;//changes the variable to move by x

                if (this.transform.position.x >= objectPosition+distance)//checks to see if tag reached or went beyond distance
                    moveLeft = true;//falsifys the if statement preventing it from running

            }
            else//switches directions
            {
                PlayerMovement.extraMove = -x;//chenges the variable to move by -x
                if (this.transform.position.x <= objectPosition-distance)//checks to see if tag reached or went beyond negative distance
                    moveLeft = false;//reactivates if statement
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            PlayerMovement.extraMove = 0;//does not move the object when they leave the trigger zone(aka platform)
        }
    }
}
