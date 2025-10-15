using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlagCheckpoint : MonoBehaviour
{
    public AudioClip scoreSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Play pickup sound
        AudioSource.PlayClipAtPoint(scoreSound, transform.position);
        if (other.TryGetComponent<FlagCheckpointAssign>(out var player))
        {
            player.SetCheckpoint(transform);
        }
}
