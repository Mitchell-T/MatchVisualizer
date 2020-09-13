using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataInput : MonoBehaviour
{
    // event which can be used to display frames when ready
    public event Action<Frame> FrameReadyEvent;

    public void GetData()
    {
        // open file explorer window to easily switch between normal dataset and test dataset
        string newFilePath = EditorUtility.OpenFilePanel("open file", "", "dat");
        // read all lines from file
        List<string> inputData = new List<string>();
        inputData = File.ReadAllLines(newFilePath).ToList<string>();

        // for every line generate a frame
        // WARNING: highly unoptimized and will generate every single frame in the dataset
        foreach (string s in inputData)
        {
            PrepareFrame(s);
        }

    }

    // prepare and provide a frame
    private void PrepareFrame(string s)
    {
        Frame newFrame = new Frame(GetTrackedObjectData(s), GetBallData(s));
        FrameReadyEvent(newFrame);
    }

    private List<TrackedObject> GetTrackedObjectData(string s)
    {
        List<TrackedObject> trackedObjects = new List<TrackedObject>();

        // filter out other data
        int startObjects = s.IndexOf(':') + 1;
        int endObjects = s.Substring(0, s.Length - 1).LastIndexOf(':') - 1; 
        s = s.Substring(startObjects, endObjects - startObjects);

        // split string into seperate tracked objects
        string[] objects = s.Split(';');

        // assign every object with their individual data
        foreach (string objectData in objects)
        {
            string[] separatedData = objectData.Split(',');
            TrackedObject newObject = new TrackedObject();
            newObject.team = (Teams)int.Parse(separatedData[0]);    // set the team
            newObject.trackingID = int.Parse(separatedData[1]);     // set the tracking ID
            newObject.playerNumber = int.Parse(separatedData[2]);   // set the player number
            newObject.position = new Vector3(float.Parse(separatedData[3]), 0.2f, float.Parse(separatedData[4])); // set the position
            newObject.speed = float.Parse(separatedData[5]);        // set the speed UNUSED

            trackedObjects.Add(newObject);
        }
        return trackedObjects;
    }

    private BallData GetBallData(string s)
    {
        // filter out other data
        s = s.Substring(0, s.Length - 1);
        s = s.Substring(s.LastIndexOf(':') + 1);

        string[] separatedData = s.Split(',');

        BallData newBallData = new BallData();
        newBallData.ballPosition = new Vector3(float.Parse(separatedData[0]), float.Parse(separatedData[1]), float.Parse(separatedData[2])); // set ball position
        newBallData.ballSpeed = float.Parse(separatedData[3]);  // set ball speed UNUSED

        return newBallData;
    }
}