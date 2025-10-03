using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnFall : MonoBehaviour
{
    public float lowestY = -10f;

    void Update()
    {
        if (transform.position.y < lowestY)
        {
            GetComponent<FlagCheckpointAssign>().Respawn();
        }
    }
}
