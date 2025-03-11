using Unity.VisualScripting;
using UnityEngine;

public class Bishop : BasePiece
{
    private bool CanUpdateMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        this.PieceSpeed *= 4;   //Increase Bishop movement speed over base speed.
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override Vector3 SelectTarget()
    {
        //Add randomness to movetimer
        PieceCycleTimer += Random.Range(-0.85f, 0.85f);

        //select player as target
        return GlobalVars.player.transform.position;
    }

    public override bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized)
    {
        
        if (CanUpdateMove && Mathf.Abs(distance.magnitude) >= 3 && Mathf.Abs(distance.magnitude) < 8) // if piece is more than 1 unit away
        {
            var rotation = Quaternion.AngleAxis(45f, Vector3.forward);
            Vector3 direction = rotation * (Vector3)moveDir;
            transform.position += GlobalVars.DeltaTimePiece * PieceSpeed * direction;
            CanUpdateMove = false;
            //return false;  //continue moving
        }
        else if (!CanUpdateMove)
        {
            transform.position += GlobalVars.DeltaTimePiece * PieceSpeed * (Vector3)moveDir;
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

        if (Mathf.Abs((GlobalVars.player.transform.position - transform.position).magnitude) <= 3) // if piece is more than 1 unit away
        {
            attackTimer = 1.5f;
            attackTarget = GlobalVars.player.transform.position;
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
