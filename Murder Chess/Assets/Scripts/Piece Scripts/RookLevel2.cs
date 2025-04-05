using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using UnityEngine;

public class RookLevel2 : BasePiece
{
    public float changeDirectionTime = 0.5f;
    public float attackDuration;
    private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
    private Vector3 currentDirection;
    private Vector3 currentPosition;
    private Vector3 playerAlignment; // positive x means the player is to the right, positive y means the player is up
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
        currentPosition = transform.position;
        playerAlignment = GlobalVars.player.transform.position - currentPosition;
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

    public override Vector3? SelectTarget()
    {
        //Add randomness to movetimer (so pieces dont sync up and look bad)
        PieceCycleTimer += Random.Range(-0.25f, 0.5f);

        return transform.position + currentDirection;
    }

    public override bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized)
    {

        transform.Translate(currentDirection * PieceSpeed * GlobalVars.DeltaTimePiece);
        return false;
    }

    public override bool ShouldAttack()
    {
        if (Mathf.Abs((GlobalVars.player.transform.position - transform.position).magnitude) <= 6 && ((playerAlignment.x > 0.5 || playerAlignment.x < -0.5) || (playerAlignment.y > 0.5 || playerAlignment.y < -0.5))) // if piece is more than 3 unit away
        {
            attackTimer = 1 + attackDuration;
            return true;  //continue moving
        }
        else
        {

            

            return false;
        }
        return false;
    }

    float attackTimer;
    Vector3 attackTarget;
    Vector3 moveDir;
    public override bool Attack(out bool isDangerous)
    {
        attackTimer -= GlobalVars.DeltaTimePiece;
        isDangerous = true; //Piece can hurt you while attacking

        if(playerAlignment.x >= 0 && (playerAlignment.y < 0.5 && playerAlignment.y > -0.5) && attackTimer >= attackDuration) { // if the player is directly to the right

            moveDir.x = 1;
            moveDir.y = 0;

        }
        else if (playerAlignment.x < 0 && (playerAlignment.y < 0.5 && playerAlignment.y > -0.5) && attackTimer >= attackDuration) {

            moveDir.x = -1;
            moveDir.y = 0;

        }
        else if (playerAlignment.y >= 0 && (playerAlignment.x < 0.5 && playerAlignment.x > -0.5) && attackTimer >= attackDuration) {

            moveDir.x = 0;
            moveDir.y = 1;

        }
        else if (playerAlignment.y < 0 && (playerAlignment.x < 0.5 && playerAlignment.x > -0.5) && attackTimer >= attackDuration) {

            moveDir.x = 0;
            moveDir.y = -1;

        }

        //Vector2 moveDir = (attackTarget - transform.position).normalized;
        if(attackTimer < attackDuration) {
            transform.position += moveDir * PieceSpeed * 3 * GlobalVars.DeltaTimePiece;
        }

        if (attackTimer >= 0) return false; //do attack for attacktimer sec
        else return true;
        isDangerous = false;
        return true;
    }
    public override float HurtPlayerFor() //Extra code on top of Default Implementation
    {
        return base.HurtPlayerFor();
    }
}
