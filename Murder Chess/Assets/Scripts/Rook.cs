using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using UnityEngine;

public class Rook : BasePiece
{
    public float changeDirectionTime = .5f;
    private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
    private Vector3 currentDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        SetRandomDirection();
        InvokeRepeating(nameof(SetRandomDirection), changeDirectionTime, changeDirectionTime);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // if (duplicateTimer > 0) duplicateTimer -= GlobalVars.DeltaTimePiece;
        // base.Update();
        // transform.Translate(currentDirection * GlobalVars.DeltaTimePiece * PieceSpeed); 
        base.Update();
    }
    void SetRandomDirection()
    {
        currentDirection = directions[Random.Range(0, directions.Length)];
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        transform.Translate(-currentDirection * PieceSpeed * GlobalVars.DeltaTimePiece * 2);
        SetRandomDirection();
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.DamagePlayer(HurtPlayerFor());
        }
        else if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Obstacles")
        {
            Collider2D otherCollider = collision.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherCollider);
        }

    }

    public override Vector3 SelectTarget()
    {
        //Add randomness to movetimer (so pieces dont sync up and look bad)
        PieceCycleTimer += Random.Range(-0.25f, 0.25f);

        //select player as target
        // return GlobalVars.player.transform.position;
        return transform.position + currentDirection;
    }

    public override bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized)
    {
        // if (Mathf.Abs(distance.magnitude) >= 1) // if piece is more than 1 unit away
        // {
        //     transform.position += GlobalVars.DeltaTimePiece * PieceSpeed * (Vector3)moveDir;
        //     return false;  //continue moving
        // }
        // else
        // {
        //     return true; //stop moving and go to next stage
        // }

        transform.Translate(currentDirection * PieceSpeed * GlobalVars.DeltaTimePiece);
        return false;
    }

    public override bool ShouldAttack()
    {
        // if (Mathf.Abs((GlobalVars.player.transform.position - transform.position).magnitude) <= 3) // if piece is more than 3 unit away
        // {
        //     attackTimer = 1.5f;
        //     attackTarget = GlobalVars.player.transform.position;
        //     return true;  //continue moving
        // }
        // else
        // {
        //     return false;
        // }
        return false;
    }

    float attackTimer;
    Vector3 attackTarget;
    public override bool Attack(out bool isDangerous)
    {
        // attackTimer -= GlobalVars.DeltaTimePiece;
        // isDangerous = true; //Piece can hurt you while attacking
        // Vector2 moveDir = (attackTarget - transform.position).normalized;
        // transform.position += (Vector3)moveDir * PieceSpeed * 2 * GlobalVars.DeltaTimePiece;

        // if (attackTimer >= 0) return false; //do attack for attacktimer sec
        // else return true;
        isDangerous = false;
        return true;
    }

    public float duplicateTimer; //Duplicate cooldown
    public int duplicateLimit = 3; //Duplicate limit
    public override float HurtPlayerFor() //Extra code on top of Default Implementation
    {
        // if (duplicateTimer <= 0 && duplicateLimit > 0)
        // {
        //     duplicateTimer = 2;
        //     duplicateLimit -= 1;
        //     GameObject clone = GameObject.Instantiate(gameObject); //Duplicates when hurting player
        //     clone.GetComponent<Pawn>().duplicateTimer = duplicateTimer;
        //     clone.GetComponent<Pawn>().duplicateLimit = duplicateLimit;
        //     clone.GetComponent<Pawn>().isDangerous = false;
        // }
        return base.HurtPlayerFor();
    }
}
