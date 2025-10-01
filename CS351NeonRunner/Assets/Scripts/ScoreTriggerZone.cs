using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    bool isActive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
        
            isActive = false;

            ScoreManager.score++;

            Destroy(gameObject);
        }
    }
}
