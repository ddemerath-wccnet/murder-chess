using UnityEngine;

public class ManaBoostAbility : BaseAbility
{
    protected override void AbilityStart()
    {
        new ModifierEntry("multiplier_PlayerMana", 9, 5);
        new ModifierEntry("multiplier_PlayerManaGain", 9, 5);
        //new ModifierEntry("multiplier_PieceSpeed", -9, 5);
    }

    protected override bool AbilityUpdate()
    {
        return true;
    }
}
