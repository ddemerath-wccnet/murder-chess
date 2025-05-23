using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class TutorialPawn : BasePiece
{
    public bool isDangerousMe;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        elements = GetComponents<ElementalPiece>().ToList<ElementalPiece>();
        if (isDangerousMe) isDangerous = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //if (duplicateTimer > 0) duplicateTimer -= GlobalVars.DeltaTimePiece;
        //base.Update();

        if (iFrames > 0) iFrames -= GlobalVars.DeltaTimePiece;

        // State Machine

        // Death
        if (PieceHealth <= 0)
        {
            cycleState = "Death";
            if (Death()) SelfDestruct();
        }

        if (isDangerous) // isDangerous Visualizer
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (iFrames > 0)
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color - new Color(0, 0, 0, 0.5f);
        }

        // Status Effects
        foreach (BaseStatusEffect baseStatusEffect in new List<BaseStatusEffect>(activeEffects))
        {
            baseStatusEffect.Run();
        }
    }

    public override Vector3? SelectTarget()
    {
        //Add randomness to movetimer (so pieces dont sync up and look bad)
        PieceCycleTimer += Random.Range(-0.25f, 0.25f);

        //select player as target
        return GlobalVars.getTarget();
    }

    public override bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized)
    {
        if (Mathf.Abs(distance.magnitude) >= 1) // if piece is more than 1 unit away
        {
            transform.position += GlobalVars.DeltaTimePiece * PieceSpeed * (Vector3)moveDir;
            return false;  //continue moving
        }
        else
        {
            return true; //stop moving and go to next stage
        }
    }

    public override bool ShouldAttack()
    {
        if (Mathf.Abs((GlobalVars.getTarget() - transform.position).magnitude) <= 3) // if piece is more than 3 unit away
        {
            attackTimer = 1.5f;
            attackTarget = GlobalVars.getTarget();
            return true;  //continue moving
        }
        else
        {
            return false;
        }
    }

    float attackTimer;
    Vector3 attackTarget;
    public override bool Attack(out bool isDangerous)
    {
        attackTimer -= GlobalVars.DeltaTimePiece;
        isDangerous = true; //Piece can hurt you while attacking
        Vector2 moveDir = (attackTarget - transform.position).normalized;
        transform.position += (Vector3)moveDir * PieceSpeed * 2 * GlobalVars.DeltaTimePiece;

        if (attackTimer >= 0) return false; //do attack for attacktimer sec
        else return true;
    }

    public float duplicateTimer; //Duplicate cooldown
    public int duplicateLimit = 3; //Duplicate limit
    public override float HurtPlayerFor() //Extra code on top of Default Implementation
    {
        //if (duplicateTimer <= 0 && duplicateLimit > 0) 
        //{
        //    duplicateTimer = 2;
        //    duplicateLimit -= 1;
        //    GameObject clone = GameObject.Instantiate(gameObject, transform.parent); //Duplicates when hurting player
        //    clone.GetComponent<Pawn>().duplicateTimer = duplicateTimer;
        //    clone.GetComponent<Pawn>().duplicateLimit = duplicateLimit;
        //    clone.GetComponent<Pawn>().isDangerous = false;
        //    clone.GetComponent<WaveItem>().PieceCoinValue = 0;
        //}
        return base.HurtPlayerFor();
    }
}
