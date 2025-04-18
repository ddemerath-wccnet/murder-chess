using Unity.VisualScripting;
using UnityEngine;

public class Queen : BasePiece
{
    //Distance until the queen will go after the player.
    public float range;
    //The distance at which the queen will remain while following the player.
    public float closeRange;
    //Stops the move function from updating every frame so that the piece will wait in place for a time.
    private bool canUpdateMove;
    [SerializeField]
    //The current time the queen is waiting when she finishes an attack.
    private float waitTimer;
    [SerializeField]
    //The maximum amount of time the Queen should wait when she finishes an attack.
    private float maxWaitTimer;
    //Should the piece wait. true = yes, false = no
    private bool shouldWait;
    [SerializeField]
    //Counts up to trigger the delayed attack during the Queen chase. When this float is equal to maxQueenStrikeTimer, 
    //execute attack and reset.
    private float queenStrikeTimer;
    [SerializeField]
    //Max amt that the queen strike countdown can take. This number represents the # of seconds before the queen strike is initiated.
    private float maxQueenStrikeTimer;
    //Distance that the Queen Strike attack will move
    private float queenStrikeDistance;
    //Initial distance between the queen and her target when starting to follow. Default -1 to show inactive.
    private float initialFollowDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        canUpdateMove = true;
        queenStrikeTimer = 0;
        queenStrikeDistance = range;
        initialFollowDistance = -1f;
        shouldWait = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    //Checks if the target is less than or equal to (within) the range value defined above
    // and returns the boolean result.
    private bool inRange(Vector2 target)
    {
        return Vector2.Distance(transform.position, target) <= range;
    }

    //Checks if the target is less than or equal to (within) the close range value defined
    // above and returns the boolean result.
    private bool inCloseRange(Vector2 target)
    {
        return Vector2.Distance(transform.position, target) <= closeRange;
    }
    //Check whether the player is in range and recheck targets if player is not friendly.
    private bool isPlayerInRange()
    {
        return Vector2.Distance(transform.position, GlobalVars.player.transform.position) <= range;
    }
    //Check where the player is in close range
    private bool isPlayerInCloseRange()
    {
        return Vector2.Distance(transform.position, GlobalVars.player.transform.position) <= closeRange;
    }

    public override Vector3? SelectTarget()
    {
        if (shouldWait)
        {
            if (waitTimer < maxWaitTimer)
            {
                waitTimer += Time.deltaTime;
                return null;
            }
            else
            {
                waitTimer = 0;
                shouldWait = false;
            }
        }

        //Add randomness to movetimer (so pieces dont sync up and look bad)
        PieceCycleTimer += Random.Range(-0.75f, 0.125f);

        //If !inRange, find a direction that is between the players position and a perpendicular position.
        if (!inRange(GlobalVars.player.transform.position))
        {
            //find distance to player.
            Vector2 distToPlayer = GlobalVars.player.transform.position - transform.position;
            Vector2 perpendicularPos = Vector2.Perpendicular(distToPlayer);
            //use the player's pos to get a Vector2 and then run it through Perpendicular.
            //50/50 odds for direction; reverse Perp Vec2 using -1.
            Vector2 perp_moveDir = Random.Range(0, 99) > 49 ? perpendicularPos : perpendicularPos * -1;
            //Lerp between perp_moveDir and normalizedDistToPlayer using a random value
            float randomT = Random.value;   //random value between 0 and 1
            Vector2 randomDir = Vector2.Lerp(distToPlayer, perp_moveDir, randomT);
            //return a target that is a Vector2 in between the perpendicular direction and the direction to the player.
            return randomDir;
        }
        //Else, find the direction to the player.
        else
        {
            //Return a position that is a % of the way between the player and this piece.
            //0 is exactly on top of the player, 1 is exactly on top of this piece.
           return Vector2.Lerp(GlobalVars.player.transform.position, transform.position, 0.15f); 
        }

        //select player as target
        //return GlobalVars.player.transform.position;
    }

    public override bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized)
    {
        //this logic should be moved to the selectTarget function.
        //If player is in range, but not in close range, then recalculate the target.
        if (isPlayerInRange() && !isPlayerInCloseRange())
        {
            float followDistance = 0.15f;  //A value used in the LERP function to determine how far behind the player
                                           //the piece should follow. Must be a value between 0 and 1.
            target = Vector2.Lerp(GlobalVars.player.transform.position, transform.position, followDistance);
        }
        //Define the speed of the piece for easy manipulation and pass-through.
        float speed = PieceSpeed;
        //if inRange(target), canUpdateMove=true, make queen move quickly into closeRange.
        if (inRange(target))
        {
            //Move the enemy towards the player.
            
            //Multiply the speed by the inverse square of the distance between the queen and target to make her slow down.
            speed = PieceSpeed * Mathf.Pow((initialFollowDistance/distance.magnitude), 2f);
            //Don't let speed drop far below player's speed (should not be hard coded like this; fix later by replacing the #f's below).
            //if (speed < 0.75f) speed = 0.75f;
            //Similarly, don't let the speed rise above the PieceSpeed value.
            if(speed > PieceSpeed) speed = PieceSpeed;
            canUpdateMove = true;
            if (!inCloseRange(target))
            {
                //Increase queenStrikeTimer each frame
                queenStrikeTimer += Time.deltaTime;
                if (initialFollowDistance == -1f)
                {
                    initialFollowDistance = distance.magnitude;
                }
                transform.position = Vector2.MoveTowards(transform.position,
                    target,
                    speed * Time.deltaTime);
                return false;
            }
            else
            {
                //Reset the initialFollowDistance to -1.
                initialFollowDistance = -1;
                //Allow Queen to quickly reset to continue following the King and/or immediately start the attack state.
                PieceCycleTimer = 0;
                //While in closeRange, increase the queenStrikeTimer by the deltaTime * x.
                queenStrikeTimer += Time.deltaTime * 2;
                if (queenStrikeTimer >= maxQueenStrikeTimer)
                {
                    //If queenStrikeCount is 0 or less, move to attack.
                    return true;
                }
                else
                {
                    //If queenStrikeCount is greater than 0, continue with the move function.
                    transform.position = Vector2.MoveTowards(transform.position,
                        target,
                        speed * Time.deltaTime);
                    return false;
                }

            }
        }
        else    //else if the target is not inRange
        {
            //set canUpdateMove to false and start the piece on a movement towards the random target it was given.
            canUpdateMove = false;
            //Reduce piece speed to make the movement more stalker-like
            speed /= 2;
            transform.position = Vector2.MoveTowards(transform.position,
                target,
                speed * Time.deltaTime);
            return false;
        }
    }

    public override bool ShouldAttack()
    {
        float abs_magnitude = Mathf.Abs((GlobalVars.player.transform.position - transform.position).magnitude);
        if (queenStrikeTimer >= maxQueenStrikeTimer) // if 
        {
            //attackTimer = 1.1875f; //timer for the Queen Strike attack at close range
            Vector2 dirVector = GlobalVars.player.transform.position - transform.position;
            Vector2 distVector = dirVector.normalized * queenStrikeDistance;
            Vector2 strikePos = (Vector2)transform.position + distVector;
            //Timer for Queen Strike. The attack continues for as long as the movement needs to plus a half second.
            attackTimer = distVector.magnitude / PieceSpeed + 0.5f;
            attackTarget = (Vector3)strikePos;
            return true;
        }
        /*
        else if (abs_magnitude <= 6 && abs_magnitude >= 2.5)
        {
            attackTimer = 1.75f; //1 and 3/4 timer for the Queen strike attack at long range.
            attackTarget = GlobalVars.player.transform.position;
            return true;
        }
        */
        else
        {
            return false;  //continue moving
        }
    }

    float attackTimer;
    Vector3 attackTarget;
    public override bool Attack(out bool isDangerous)
    {
        queenStrikeTimer = 0;
        attackTimer -= GlobalVars.DeltaTimePiece;
        isDangerous = true; //Piece can hurt you while attacking
        Vector2 moveDir = (attackTarget - transform.position).normalized;
        transform.position += (Vector3)moveDir * PieceSpeed * 1.5f * GlobalVars.DeltaTimePiece;

        if (attackTimer >= 0)
        { 
            return false; //do attack for attacktimer sec
        }
        else
        {
            //reset the piece cycle timer to max so that the piece cannot chain attacks endlessly.
            PieceCycleTimer = MaxPieceCycleTimer;
            //Make the piece wait before it starts moving again.
            shouldWait = true;
            return true;
        }
    }
    //When the queen collides with a wall, cause her to stop immediately and return to the select target state.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            GetComponent<Rigidbody2D>().linearVelocity = 8  * Vector2.Reflect(collision.contacts[0].normal, collision.contacts[0].normal);
            //Make piece wait after reset.
            shouldWait = true;
            //Make piece recalc target during reset.
            cycleState = "Select Target";
            //make piece take as long as possible to continue after reset and wait.
            PieceCycleTimer = MaxPieceCycleTimer;
            //make the piece not dangerous, just in case it is.
            isDangerous = false;
        }
    }

    public override float HurtPlayerFor() //Extra code on top of Default Implementation
    {
        return base.HurtPlayerFor();
    }
}
