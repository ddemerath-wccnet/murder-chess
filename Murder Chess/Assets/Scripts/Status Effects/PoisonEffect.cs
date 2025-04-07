using UnityEngine;

public class PoisonEffect : BaseStatusEffect
{
    public override bool CanStack => true;

    public PoisonEffect(Player player, float duration, int level = 1) : base(player, duration, level){}

    public PoisonEffect(BasePiece piece, float duration, int level = 1) : base(piece, duration, level){}

    float poison = 0.5f;

    public override void StartEffect_Player()
    {
        image = Resources.Load<Sprite>("Sprites/status_effects/poison_effect");
    }
    public override void StartEffect_Piece() 
    {
        image = Resources.Load<Sprite>("Sprites/status_effects/poison_effect");
    }

    public override void UpdateEffect_Player()
    {
        player.PlayerHealth = player.PlayerHealth - (poison * GlobalVars.DeltaTimePlayer);
    }
    public override void UpdateEffect_Piece()
    {
        piece.PieceHealth = piece.PieceHealth - (poison * GlobalVars.DeltaTimePiece);
    }

    public override void EndEffect_Player()
    {

    }
    public override void EndEffect_Piece()
    {

    }
}
