using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Microsoft.VisualBasic;

public class DataCollection : MonoBehaviour 
{
    /* This class collects data for the reinforcement algorithm to make decisition.
     * It will store a log of this as a csv.
     * --------------
     * frame, positionX, positionY, directionX, approval 
     * 
     * 
     * 
     */
    private List<string> entries;
    private long frameCount;
    [SerializeField] Transform agentPosition; // get the informantion of the location.
    // Start is called before the first frame update
    void Start()
    {
        entries = new List<string>();
        frameCount = 0;
    }
    
    // Called every 1/50th of a second.
    private void FixedUpdate() {

        // converted to integers in order for simplicity
        int posX = (int)Mathf.Floor(agentPosition.position.x);
        int posY = (int)Mathf.Floor(agentPosition.position.y);

        // Not fully functional, yet.
        int directionX = 0; // will be either -1, 0, 1
        int approval = 0; // 0 or 1: no or yes

        string line = frameCount + "," + posX + "," + posY + "," + directionX + "," + approval;
        entries.Add(line);
        print(line);

        frameCount += 1;
    }

}
