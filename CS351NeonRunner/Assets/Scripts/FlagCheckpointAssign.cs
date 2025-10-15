using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCheckpointAssign : MonoBehaviour
{
    private Transform lastCheckpoint;
    private Vector3 startPos;


    void Start()
    {
        //If no checkpoint is touched, respawn at start
        startPos = transform.position; 
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    public void Respawn()
    {
        if(lastCheckpoint != null)
        {
            transform.position = lastCheckpoint.position;
        }
        else
        {
            transform.position = startPos;
        }
    }
}

