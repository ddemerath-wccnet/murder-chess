
using UnityEngine;

public class King : BasePiece
{
    [SerializeField]
    private float bossHealth;
    [SerializeField]
    private float bossDamage;
    [SerializeField]
    private float delayTime;
    private float moveDelayTimer = 0f;
    [SerializeField]
    private float bounceDistance;
    Vector3 origPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        MaxPieceHealth += bossHealth;
        base.Start();
        moveDelayTimer = delayTime;
    }

    // Update is called once per frame
    protected override void Update()
    {


        if (moveDelayTimer > 0)
        {
            origPos = transform.position;
            Vector3 directionFromPlayer = (transform.position - GlobalVars.player.transform.position).normalized;
            distanceFromPlayer = Vector3.Distance(transform.position, GlobalVars.player.transform.position);
            targetPos = transform.position + directionFromPlayer * bounceDistance;
            moveDelayTimer -= GlobalVars.DeltaTimePiece;
            lerpPos = 0;
        }
        else
        {
            bounceBack();
            // if (duplicateTimer > 0) duplicateTimer -= GlobalVars.DeltaTimePiece;
            base.Update();
        }
    }
    float lerpPos;
    Vector3 targetPos;
    float distanceFromPlayer;
    public void bounceBack()
    {
        float lerpSpeed = 1f;
        lerpPos += lerpSpeed * GlobalVars.DeltaTimePiece;
        Vector3 directionFromPlayer = (transform.position - GlobalVars.player.transform.position).normalized;

        if (distanceFromPlayer < .1)
        {
            // Vector3 targetPos = transform.position + directionFromPlayer * bounceDistance;
            transform.position = Vector3.Lerp(origPos, targetPos, lerpPos);
            // transform.position += directionFromPlayer * bounceDistance;

            attackTimer = 0;
            if (transform.position == targetPos)
            {
                moveDelayTimer++;
            }

        }
        else
        {
            distanceFromPlayer = Vector3.Distance(transform.position, GlobalVars.player.transform.position);

        }


    }

    public override Vector3? SelectTarget()
    {
        //Add randomness to movetimer (so pieces dont sync up and look bad)
        PieceCycleTimer += Random.Range(0, 1f);

        //select player as target
        return GlobalVars.player.transform.position;
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
        if (Mathf.Abs((GlobalVars.player.transform.position - transform.position).magnitude) <= 3) // if piece is more than 3 unit away
        {
            attackTimer = 1.5f;
            attackTarget = GlobalVars.player.transform.position;
            Debug.Log("King is ready to attack");
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

        if (attackTimer >= 0)
        {
            return false; //do attack for attacktimer sec
        }
        else
        {
            return true;

        }
    }

    public override float HurtPlayerFor() //Extra code on top of Default Implementation
    {
        Debug.Log("King's Damage: " + PieceDamage);
        return base.HurtPlayerFor();

    }
}
