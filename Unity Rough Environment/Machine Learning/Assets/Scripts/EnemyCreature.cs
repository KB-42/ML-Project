using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreature : MonoBehaviour {

    float initalPosition;
	// Use this for initialization
    public float velocity = 1;
    public float distance = 2;
    private bool moveLeft = false;


    void Start()
    {
        initalPosition = this.transform.position.x;
    }

    // Update is called once per frame
    void Update () {
        var x = velocity * Time.deltaTime;
        if (moveLeft == false)//object with tag left/right moves right while position is less than distance
        {
            this.transform.Translate(x, 0, 0);

            if (this.transform.position.x >= ( initalPosition + distance))//checks to see if tag reached or went beyond distance
                moveLeft = true;//falsifys the if statement preventing it from running

        }
        else
        {
            this.transform.Translate(-x, 0, 0);//sends platform into negative direction
            if (this.transform.position.x <= (initalPosition - distance))//checks to see if tag reached or went beyond negative distance
                moveLeft = false;//reactivates if statement
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Application.LoadLevel("SampleScene");
        }
    }
}
