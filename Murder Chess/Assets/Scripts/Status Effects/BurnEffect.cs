using UnityEngine;

public class BurnEffect : BaseStatusEffect
{
    public override bool CanStack => true;

    public BurnEffect(Player player, float duration, int level = 1) : base(player, duration, level){}

    public BurnEffect(BasePiece piece, float duration, int level = 1) : base(piece, duration, level){}

    float burn = 1f;

    public override void StartEffect_Generic()
    {
        image = Resources.Load<Sprite>("Sprites/status_effects/burn_effect");
    }

    public override void StartEffect_Player()
    {

    }
    public override void StartEffect_Piece() 
    {

    }

    public override void UpdateEffect_Player()
    {
        player.PlayerHealth = player.PlayerHealth - (burn * GlobalVars.DeltaTimePlayer);
    }
    public override void UpdateEffect_Piece()
    {
        piece.PieceHealth = piece.PieceHealth - (burn * GlobalVars.DeltaTimePiece);
    }

    public override void EndEffect_Player()
    {

    }
    public override void EndEffect_Piece()
    {

    }
}
