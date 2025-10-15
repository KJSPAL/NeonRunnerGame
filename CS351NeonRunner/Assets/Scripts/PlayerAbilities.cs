using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType { None, SlowOpponent, Boost }

public class PlayerAbilities : MonoBehaviour
{
    public AbilityType currentAbility = AbilityType.None;
    public KeyCode useKey = KeyCode.Q;
    public NeonRunnerPlayerController opponent;
    public NeonRunnerPlayerController player;

    void Update()
    {
        if (currentAbility == AbilityType.None) return;

        if (Input.GetKeyDown(useKey))
        {
          
            if (currentAbility == AbilityType.SlowOpponent)
            {
                opponent.ApplySlow();
            }
            else if (currentAbility == AbilityType.Boost)
            {
                player.ApplyBoost();
            }


            currentAbility = AbilityType.None;
        }
    }
}
