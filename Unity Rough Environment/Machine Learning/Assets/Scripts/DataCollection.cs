using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
    private int fileID;
    private bool isAllowedToCollect;
    private int directionX = 0; // will be either -1, 0, 1: This will specify the movement direction it is traveling
    private int approval = 0; // 0 or 1: no or yes

    // Start is called before the first frame update
    void Start()
    {
        entries = new List<string>();
        frameCount = 0;
        fileID = 0;
        isAllowedToCollect = true;
    }

    private void Update() {
        // this is to print/write to file 
        if (Input.GetKeyDown(KeyCode.P)) {
            //print("Got here");
            isAllowedToCollect = false;
            this.WriteToFile();
        } else if (Input.GetKeyDown(KeyCode.Y)) {

            approval = 1;

        } else if (Input.GetKeyDown(KeyCode.N)) {

            approval = 0;

        }
    }


    // Called every 1/50th of a second.
    private void FixedUpdate() {

        if (isAllowedToCollect) { 

            // converted to integers in order for simplicity
            int posX = (int)Mathf.Floor(agentPosition.position.x);
            int posY = (int)Mathf.Floor(agentPosition.position.y);

            // Not fully functional, yet.
            

            string line = frameCount + "," + posX + "," + posY + "," + directionX + "," + approval;
            entries.Add(line);
            //print(line);
            frameCount += 1;

        }

    }

    private void WriteToFile() {
        print("Is writing to file");
        StreamWriter writer = File.CreateText(Application.persistentDataPath + "/data_collection_" + fileID + ".csv");

        for (int i = 0; i < entries.Count; i++) {
            writer.WriteLine(entries[i]);
        }
        writer.Close();
        print("done writing");

        isAllowedToCollect = true;
        entries.Clear();
        fileID++;
    }

    // gives the content of a file.
    public List<string> GetContentOfCSV(string fName) {
        List<string> data = new List<string>();
        StreamReader read = File.OpenText(Application.persistentDataPath + "/" + fName + ".csv");

        print("Reading from file..");
        while (!read.EndOfStream) {
            data.Add(read.ReadLine());
        }
        read.Close();

        print("Done reading!");

        return data;
    }


}
