using UnityEngine;

public class IcyEffect : BaseStatusEffect
{
    public override bool CanStack => false;

    public IcyEffect(Player player, float duration, int level = 1) : base(player, duration, level){}

    public IcyEffect(BasePiece piece, float duration, int level = 1) : base(piece, duration, level){}

    float multiplier = 0.5f;

    public override void StartEffect_Player() //divide value by 2
    {
        image = Resources.Load<Sprite>("Sprites/status_effects/icy_effect");

        float globalModifier = GlobalVars.multiplier_PlayerSpeed;
        float value = player.PlayerSpeed;
        player.PlayerSpeed = ((value / globalModifier) * multiplier) * globalModifier; //Set in a way that modifies the base value;
    }
    public override void StartEffect_Piece() //divide value by 2
    {
        image = Resources.Load<Sprite>("Sprites/status_effects/icy_effect");

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
