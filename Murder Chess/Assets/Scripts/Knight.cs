using System.Collections;
using UnityEngine;

public class Knight : BasePiece
{
    // private float moveCooldown = 0.5f; // Time before next move
    // private float nextMoveTime = 0f;
    // private bool isJumping = false; // Prevents overlapping movement

    private Vector2[] knightMoves = {
        new Vector2(2, 1), new Vector2(2, -1),
        new Vector2(-2, 1), new Vector2(-2, -1),
        new Vector2(1, 2), new Vector2(1, -2),
        new Vector2(-1, 2), new Vector2(-1, -2)
    };

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override Vector3 SelectTarget()
    {
        // Add randomness to move timer (to avoid all pieces moving in sync)
        PieceCycleTimer += Random.Range(-0.25f, 0.25f);


        // Find the best L-shaped move
        Vector3 bestMove = FindBestKnightMove();

        // Compare bestMove's distance to the player
        float distanceToPlayer = Vector3.Distance(bestMove, GlobalVars.player.transform.position);

        // If the best move is within 1 unit of the player, perform a jump
        // if (distanceToPlayer <= 1.0f)
        // {
        //     Debug.Log("Test");
        //     StartCoroutine(JumpAnimation(bestMove));
        //     return bestMove;
        // }

        // Otherwise, move normally
        return bestMove;
    }

    private Vector3 FindBestKnightMove()
    {
        Vector3 playerPos = GlobalVars.player.transform.position;
        Vector3 bestMove = transform.position;
        float bestDistance = Vector3.Distance(transform.position, playerPos);

        foreach (Vector2 move in knightMoves)
        {
            Vector3 potentialPos = transform.position + (Vector3)move;
            float distance = Vector3.Distance(potentialPos, playerPos);

            if (distance < bestDistance) // Pick the L-move that brings the knight closest
            {
                bestDistance = distance;
                bestMove = potentialPos;
            }
        }

        return bestMove; // Return the best L-shaped move found
    }
    
    public override bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized)
    {
        if (Mathf.Abs(distance.magnitude) >= 1 && !canPerformJumpAttack()) // if piece is more than 1 unit away
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
        if (Mathf.Abs((GlobalVars.player.transform.position - transform.position).magnitude) <= 1) // If within attack range
        {
            attackTimer = 1.5f;
            attackTarget = GlobalVars.player.transform.position;
            return true; // Attack
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
        isDangerous = true; // Piece can hurt the player while attacking

        Vector2 moveDir = (attackTarget - transform.position).normalized;
        transform.position += (Vector3)moveDir * PieceSpeed * 2 * GlobalVars.DeltaTimePiece;

        return attackTimer < 0; // Attack lasts for attackTimer duration
    }

    public override float HurtPlayerFor() // Extra code on top of Default Implementation
    {
        return base.HurtPlayerFor();
    }


    private bool canPerformJumpAttack()
    {
        // Vector3 bestMove = FindBestKnightMove();

        // // Compare bestMove's distance to the player
        // float distanceToPlayer = Vector3.Distance(bestMove, GlobalVars.player.transform.position);

        // return distanceToPlayer <= 1.0f;
        return false;
    }
//     IEnumerator JumpAnimation(Vector3 targetPosition)
// {
//     isJumping = true; // Prevent movement during jump
//     float jumpDuration = 1.4f; // Total time for the jump
//     float peakHeight = 1.0f; // How high the jump reaches
//     Vector3 startPosition = transform.position;

//     float elapsedTime = 0f;

//     while (elapsedTime < jumpDuration)
//     {
//         float t = elapsedTime / jumpDuration;
        
//         // Create an arc for the jump using Sin function
//         float heightOffset = Mathf.Sin(t * Mathf.PI) * peakHeight;

//         // Smoothly move the knight toward the target position in an arc
//         transform.position = Vector3.Lerp(startPosition, targetPosition, t) + new Vector3(0, heightOffset, 0);

//         // Scale up at peak of jump, then back down
//         transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.3f, Mathf.Sin(t * Mathf.PI));

//         elapsedTime += Time.deltaTime;
//         yield return null;
//     }

//     // Ensure final position is exact
//     transform.position = targetPosition;
//     transform.localScale = Vector3.one; // Reset scale
    
//     isJumping = false; // Allow movement again
// }

}
