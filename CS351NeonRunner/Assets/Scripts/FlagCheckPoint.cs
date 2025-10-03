// Johcori Starks
//10/3/2025
// this script help with making the player respawn at their last checkint
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCheckPoint : MonoBehaviour
{
    private static Vector3 checkpointPosition;
    private static bool checkpointSet = false;

    private bool isActivated = false;
    public GameObject player;         // Assign in Inspector
   

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated && (collision.CompareTag("Player1") || collision.CompareTag("Player2")))
        {
            isActivated = true;
            checkpointPosition = transform.position;
            checkpointSet = true;

           // To check if checkpoint is being actived by the player in console
            Debug.Log("Checkpoint activated at: " + checkpointPosition);
        }
    }

    public static void RespawnPlayer(GameObject player)
    {
        if (checkpointSet)
        {
            player.transform.position = checkpointPosition;
            Debug.Log("Player respawned at checkpoint.");
        }
        else
        {
            Debug.LogWarning("No checkpoint set yet.");
        }
    }


// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
