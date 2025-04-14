using UnityEngine;

public class Insurance : BaseAbility
{
    protected override void AbilityStart()
    {
        new ModifierEntry("multiplier_PlayerHealth", -1f);
        new ModifierEntry("multiplier_PlayerRegen", 999, 1f);
    }

    protected override bool AbilityUpdate()
    {
        return true;
    }

    public override bool CallActivate()
    {
        return false;
    }

    protected override void Update()
    {
        base.Update();
        if (GlobalVars.player.GetComponent<Player>().PlayerHealth <= 0.0001f)
        {
            if (AbilityCooldown <= 0)
            {
                AbilityCooldown = MaxAbilityCooldown;
                startedAbility = true;
                AbilityStart();
            }
        }
    }
}
