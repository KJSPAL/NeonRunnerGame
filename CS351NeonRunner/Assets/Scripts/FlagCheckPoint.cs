using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlagCheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<FlagCheckpointAssign>(out var player))
        {
            player.SetCheckpoint(transform);
        }
    }
}
