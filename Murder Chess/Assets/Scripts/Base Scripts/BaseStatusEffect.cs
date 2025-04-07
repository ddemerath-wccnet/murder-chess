using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseStatusEffect
{
    public Sprite image;
    public Player player;
    public BasePiece piece;
    public string state = "Start";
    public float maxDuration;
    public float duration;
    public int level;
    public virtual bool CanStack => false;

    public BaseStatusEffect(Player player, float duration, int level = 1)
    {
        this.player = player;
        this.maxDuration = duration;
        this.level = level;

        //Debug.Log(this.GetType());
        if (!CanStack && player.activeEffects.Any(effect => effect.GetType() == this.GetType())) //check if can stack
        {
            BaseStatusEffect oldEffect = player.activeEffects.FirstOrDefault(effect => effect.GetType() == this.GetType());
            oldEffect.state = "End";
        }
        player.activeEffects.Add(this);
    }
    public BaseStatusEffect(BasePiece piece, float duration, int level = 1)
    {
        this.piece = piece;
        this.maxDuration = duration;
        this.level = level;

        //Debug.Log(this.GetType());
        if (!CanStack && piece.activeEffects.Any(effect => effect.GetType() == this.GetType())) return; //check if can stack
        piece.activeEffects.Add(this);
    }
    public virtual void Run(bool isPlayer = false)
    {
        if (isPlayer)
        {
            if (state == "Start")
            {
                StartEffect_Player();
                duration = maxDuration;
                state = "Update";
            }
            else if (state == "Update")
            {
                UpdateEffect_Player();
                duration -= GlobalVars.DeltaTimePlayer;
                if (duration < 0) state = "End";
            }
            else if (state == "End")
            {
                EndEffect_Player();
                player.activeEffects.Remove(this);
            }
        }
        else
        {
            if (state == "Start")
            {
                StartEffect_Piece();
                duration = maxDuration;
                state = "Update";
            }
            else if (state == "Update")
            {
                UpdateEffect_Piece();
                duration -= GlobalVars.DeltaTimePiece;
                if (duration < 0) state = "End";
            }
            else if (state == "End")
            {
                EndEffect_Piece();
                piece.activeEffects.Remove(this);
            }
        }
    }

    /// <summary> Called once when starting effect for the player </summary>
    public abstract void StartEffect_Player();

    /// <summary> Called once when starting effect for the piece </summary>
    public abstract void StartEffect_Piece();

    /// <summary> Called every frame during the duration for the player </summary>
    public abstract void UpdateEffect_Player();

    /// <summary> Called every frame during the duration for the piece </summary>
    public abstract void UpdateEffect_Piece();

    /// <summary> Called once when ending effect for the player </summary>
    public abstract void EndEffect_Player();

    /// <summary> Called once when ending effect for the piece </summary>
    public abstract void EndEffect_Piece();
}
