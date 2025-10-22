using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{

    public int damage;
    public PlayerHealth playerHealth;

    private void OnCollsionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            playerHealth.TakeDamage(damage);
        }
    }
   
}
