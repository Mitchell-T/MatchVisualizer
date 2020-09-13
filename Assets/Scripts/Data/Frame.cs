using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public List<TrackedObject> trackedObjects = new List<TrackedObject>();
    public BallData ballData;

    public Frame(List<TrackedObject> tObjects, BallData bData)
    {
        trackedObjects = tObjects;
        ballData = bData;
    }

}
