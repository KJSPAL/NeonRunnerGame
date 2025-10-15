using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum AbilityType { None, SlowOpponent, Boost }

public class PlayerAbilities : MonoBehaviour
{
    public AbilityType currentAbility = AbilityType.None;
    public KeyCode useKey = KeyCode.Q;
    public NeonRunnerPlayerController opponent;
    public NeonRunnerPlayerController player;
    public TMP_Text abilityTextBox;

    public AudioClip scoreSound;


    void Start()
    {
        UpdateAbilityText();
    }

    void Update()
    {
        // Show current ability every frame (optional safety)
        // UpdateAbilityText();

        if (currentAbility == AbilityType.None) return;

        if (Input.GetKeyDown(useKey))
        {
            AudioSource.PlayClipAtPoint(scoreSound, transform.position);
            if (currentAbility == AbilityType.SlowOpponent)
            {
                opponent.ApplySlow();
            }
            else if (currentAbility == AbilityType.Boost)
            {
                player.ApplyBoost();
            }

            currentAbility = AbilityType.None;
            UpdateAbilityText();
        }
    }

    public void SetAbility(AbilityType newAbility)
    {
        currentAbility = newAbility;
        UpdateAbilityText();
    }

    private void UpdateAbilityText()
    {
        if (abilityTextBox == null) return;

        if (currentAbility == AbilityType.None)
            abilityTextBox.text = "Ability: None";
        else
            abilityTextBox.text = "Ability: " + currentAbility.ToString();
    }
}
