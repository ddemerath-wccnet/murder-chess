using UnityEngine;

public class StunnedEffect : BaseStatusEffect
{
    public override bool CanStack => false;

    public StunnedEffect(Player player, float duration, int level = 1) : base(player, duration, level){}

    public StunnedEffect(BasePiece piece, float duration, int level = 1) : base(piece, duration, level){}

    float multiplier = -1;

    public override void StartEffect_Generic()
    {
        image = Resources.Load<Sprite>("Sprites/status_effects/stunned_effect");
    }

    public override void StartEffect_Player() //divide value by 2
    {
        float globalModifier = GlobalVars.multiplier_PlayerSpeed;
        float value = player.PlayerSpeed;
        player.PlayerSpeed = ((value / globalModifier) * multiplier) * globalModifier; //Set in a way that modifies the base value;
    }
    public override void StartEffect_Piece() //divide value by 2
    {
        float globalModifier = GlobalVars.multiplier_PieceSpeed;
        float value = piece.PieceSpeed;
        piece.PieceSpeed = ((value / globalModifier) * multiplier) * globalModifier; //Set in a way that modifies the base value;
    }

    public override void UpdateEffect_Player()
    {

    }
    public override void UpdateEffect_Piece()
    {

    }

    public override void EndEffect_Player() //multiply value by 2
    {
        float globalModifier = GlobalVars.multiplier_PlayerSpeed;
        float value = player.PlayerSpeed;
        player.PlayerSpeed = ((value / globalModifier) / multiplier) * globalModifier; //Set in a way that modifies the base value;
    }
    public override void EndEffect_Piece() //multiply value by 2
    {
        float globalModifier = GlobalVars.multiplier_PieceSpeed;
        float value = piece.PieceSpeed;
        piece.PieceSpeed = ((value / globalModifier) / multiplier) * globalModifier; //Set in a way that modifies the base value;
    }
}
