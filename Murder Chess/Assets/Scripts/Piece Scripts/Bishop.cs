using Unity.VisualScripting;
using UnityEngine;

public class Bishop : BasePiece
{
    private bool CanUpdateMove = true;
    private Vector3 targetPos = new Vector3();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override Vector3? SelectTarget()
    {
        //Add randomness to movetimer
        PieceCycleTimer += Random.Range(-0.85f, 0.85f);

        targetPos = new Vector3();
        Vector2 moveDir = GlobalVars.getTarget() - transform.position;
        Vector2 moveDirNorm = moveDir.normalized;
        float moveAngle = Vector2.SignedAngle(moveDirNorm, Vector2.up);
        
        if (moveAngle < 0) moveAngle += 360;
        if (moveAngle >= 0 && moveAngle < 90)
        {
            targetPos += Vector3.up + Vector3.right;
        }
        else if (moveAngle >= 90 && moveAngle < 180)
        {
            targetPos += Vector3.down + Vector3.right;
        }
        else if (moveAngle >= 180 && moveAngle < 270)
        {
            targetPos += Vector3.down + Vector3.left;
        }
        else if (moveAngle >= 270 && moveAngle < 360)
        {
            targetPos += Vector3.up + Vector3.left;
        }
        
        //select player as target
        return GlobalVars.getTarget();;
    }

    public override bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized)
    {
        if (CanUpdateMove && Mathf.Abs(distance.magnitude) >= 3 && Mathf.Abs(distance.magnitude) < 7) // if piece is more than 1 unit away
        {
            transform.position += GlobalVars.DeltaTimePiece * PieceSpeed * targetPos;
            CanUpdateMove = false;
            //return false;  //continue moving
        }
        else if (!CanUpdateMove)
        {
            transform.position += GlobalVars.DeltaTimePiece * PieceSpeed * targetPos;
            //return false;  //continue moving
        }
        else
        {
            CanUpdateMove = true;
            //return true; //stop moving and go to next stage
        }

        return CanUpdateMove;
    }

    public override bool MoveDone()
    {
        return false; // wait for next step
    }

    public override bool ShouldAttack()
    {

        if (Mathf.Abs((GlobalVars.getTarget() - transform.position).magnitude) <= 3) // if piece is more than 1 unit away
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
        else
        {
            return true;
        }
    }
}
