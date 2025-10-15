using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTriggerZone : MonoBehaviour
{
    public AudioClip scoreSound;
    private void OnTriggerEnter2D(Collider2D player)
    {
        //set current ability to slow opponent
        PlayerAbilities abilities = player.GetComponent<PlayerAbilities>();
        abilities.SetAbility(AbilityType.SlowOpponent);

        // Play pickup sound
        AudioSource.PlayClipAtPoint(scoreSound, transform.position);

        // Destroy pickup object
        Destroy(gameObject);
    }
}
