using UnityEngine;

public class PawnLevel2 : BasePiece
{

    public float regen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start(); 
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (duplicateTimer > 0) duplicateTimer -= GlobalVars.DeltaTimePiece;
        if (PieceHealth < MaxPieceHealth) PieceHealth = PieceHealth + (regen * GlobalVars.DeltaTimePiece);
        base.Update();
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
        if (duplicateTimer <= 0 && duplicateLimit > 0) 
        {
            duplicateTimer = 2;
            duplicateLimit -= 1;
            GameObject clone = GameObject.Instantiate(gameObject, transform.parent); //Duplicates when hurting player
            clone.GetComponent<PawnLevel2>().duplicateTimer = duplicateTimer;
            clone.GetComponent<PawnLevel2>().duplicateLimit = duplicateLimit;
            clone.GetComponent<PawnLevel2>().isDangerous = false;
        }
        return base.HurtPlayerFor();
    }
}
