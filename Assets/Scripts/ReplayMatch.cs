using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayMatch : MonoBehaviour
{
    private int counter = 0;

    List<Frame> frames = new List<Frame>();

    List<GameObject> players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // subscribe to 
        GetComponent<DataInput>().FrameReadyEvent += AddFrame;
        GetComponent<DataInput>().GetData();
    }

    void AddFrame(Frame frame)
    {
        frames.Add(frame);
        Debug.Log("Frame ready!");
    }

    // play frames as fast as it can
    private void FixedUpdate()
    {
        if (frames[counter] != null)
        {
            if(counter == 0)
            {
                InstantiateObjects(frames[counter]);
                VisualizeFrame(frames[counter]);
            }
            VisualizeFrame(frames[counter]);
            counter++;
        }
    }

    private void VisualizeFrame(Frame f)
    {
        // move all objects according to dataset in frame
    }

    private void InstantiateObjects(Frame f)
    {
        foreach(TrackedObject tObject in f.trackedObjects)
        {
            // instantiate all objects present in dataset
        }

        // instantiate ball
    }

}
