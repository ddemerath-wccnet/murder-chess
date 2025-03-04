using UnityEngine;

public class MeleeAttack : BaseAbility
{
    public float maxAbilityLengthTimer = 2;
    public float abilityLengthTimer;
    protected override void AbilityStart()
    {
        abilityLengthTimer = maxAbilityLengthTimer;
    }

    protected override bool AbilityUpdate()
    {
        if (abilityLengthTimer > 0)
        {
            abilityLengthTimer -= GlobalVars.DeltaTimePlayer;
            GlobalVars.player.GetComponent<Player>().isDangerous = true;
            return false;
        }
        else
        {
            GlobalVars.player.GetComponent<Player>().isDangerous = false;
            return true;
        }
    }
}
