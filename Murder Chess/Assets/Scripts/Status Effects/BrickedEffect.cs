using UnityEngine;

public class BrickedEffect : BaseStatusEffect
{
    public override bool CanStack => false;
    public override bool CanOverride => false;

    public BrickedEffect(Player player, float duration, int level = 1) : base(player, duration, level){}

    public BrickedEffect(BasePiece piece, float duration, int level = 1) : base(piece, duration, level){}

    float multiplier = 100;

    public override void StartEffect_Generic()
    {
        image = Resources.Load<Sprite>("Sprites/status_effects/bricked_effect");
    }

    public override void StartEffect_Player() //divide value by 2
    {
        GlobalVars.bricked = true;
        new ModifierEntry("multiplier_PlayerDamage", -99, maxDuration);
        new ModifierEntry("multiplier_AbilityCooldown", -99, maxDuration);
        new ModifierEntry("multiplier_SpellCost", -99, maxDuration);
    }
    public override void StartEffect_Piece() //divide value by 2
    {
        piece.bricked = true;
        float globalModifier = GlobalVars.multiplier_PieceDamage;
        float value = piece.PieceDamage;
        piece.PieceDamage = ((value / globalModifier) / multiplier) * globalModifier; //Set in a way that modifies the base value;
    }

    public override void UpdateEffect_Player()
    {

    }
    public override void UpdateEffect_Piece()
    {

    }

    public override void EndEffect_Player() //multiply value by 2
    {
        GlobalVars.bricked = false;
    }
    public override void EndEffect_Piece() //multiply value by 2
    {
        piece.bricked = false;
        float globalModifier = GlobalVars.multiplier_PieceDamage;
        float value = piece.PieceDamage;
        piece.PieceDamage = ((value / globalModifier) * multiplier) * globalModifier; //Set in a way that modifies the base value;
    }
}
