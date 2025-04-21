
//using System.Runtime.CompilerServices;
//using UnityEditor.Build;
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
    private float attackRadius = 5f;
    [SerializeField]
    private float aoeDamage = 2f;
    [SerializeField]
    private float aoeRandMin;
    [SerializeField]
    private float aoeRandMax;
    private float aoeCoolDownTimer;
    public AudioSource sound;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        MaxPieceHealth += bossHealth;
        base.Start();
        moveDelayTimer = delayTime;
        aoeCoolDownTimer = Random.Range(aoeRandMin, aoeRandMax);

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (aoeCoolDownTimer > 0)
        {
            aoeCoolDownTimer -= GlobalVars.DeltaTimePiece;
        }
        else
        {
            Aoe();
            aoeCoolDownTimer = Random.Range(aoeRandMin, aoeRandMax);
        }

        if (moveDelayTimer > 0)
        {
            moveDelayTimer -= GlobalVars.DeltaTimePiece;
        }
        else
        {
            base.Update();


        }
    }

    protected void Aoe()
    {
        // Animator animation = transform.Find("Aoe_Attack_Anim").GetComponent<Animator>();
        // animation.SetTrigger("PlayAOEAnimation");
        Collider2D[] radiusColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);

        foreach (var radiusCollider in radiusColliders)
        {
            if (radiusCollider.gameObject == GlobalVars.player)
            {
                Player playerPiece = GlobalVars.player.GetComponent<Player>();
                if (playerPiece != null)
                {
                    Animator animation = transform.Find("Aoe_Attack_Anim").GetComponent<Animator>();
                    animation.SetTrigger("PlayAOEAnimation");
                    PlayAOESound();
                    playerPiece.DamagePlayer(PieceDamage * aoeDamage);

                }
            }
        }


    }

    public void PlayAOESound()
    {
        sound.Play();
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
    //testing radius, occurence rate
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
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
