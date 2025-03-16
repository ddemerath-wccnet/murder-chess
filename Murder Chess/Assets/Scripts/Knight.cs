using System.Collections;
using UnityEngine;
// Can get stuck in Move Done after jump
public class Knight : BasePiece
{
    private Vector3 bestMove;
    private bool isJumping = false;

    [SerializeField] private ParticleSystem landingDustPrefab;

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
        Debug.Log(cycleState);
        base.Update();
    }

    public override Vector3 SelectTarget()
    {

        // Add randomness to move timer (to avoid all pieces moving in sync)
        PieceCycleTimer += Random.Range(-0.25f, 0.25f);

        // Find the best L-shaped move
        bestMove = FindBestKnightMove();

        return bestMove; // Otherwise, move normally
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

        return bestMove;
    }

    public override bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized)
    {

        if (Mathf.Abs(distance.magnitude) >= 1 && !CanPerformJumpAttack())
        {
            transform.position += GlobalVars.DeltaTimePiece * PieceSpeed * (Vector3)moveDir;
            return false; // Continue moving
        }

        return true; // Stop moving and go to next stage
    }

    public override bool ShouldAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GlobalVars.player.transform.position);
        
        if (distanceToPlayer <= 1)
        {
            attackTimer = 1.5f;
            attackTarget = GlobalVars.player.transform.position;
            return true;
        }

        if (CanPerformJumpAttack())
        {
            return true;
        }

        return false;
    }

    float attackTimer;
    Vector3 attackTarget;
    public override bool Attack(out bool isDangerous)
    {
        isDangerous = true; // Piece can hurt the player while attacking

        if(isJumping) return false;

        if (!isJumping && CanPerformJumpAttack())
        {
            StartCoroutine(JumpAnimation(bestMove));
            return true;
        }

        attackTimer -= GlobalVars.DeltaTimePiece;
        Vector2 moveDir = (attackTarget - transform.position).normalized;
        transform.position += (Vector3)moveDir * PieceSpeed * 2 * GlobalVars.DeltaTimePiece;

        return attackTimer < 0; // Attack lasts for attackTimer duration
    }

    public override float HurtPlayerFor()
    {
        return base.HurtPlayerFor();
    }

    private bool CanPerformJumpAttack()
    {
        if (isJumping) return false; // Prevent multiple jumps

        bestMove = FindBestKnightMove();
        float currentDistanceToPlayer = Vector3.Distance(transform.position, GlobalVars.player.transform.position);
        float projectedDistanceToPlayer = Vector3.Distance(bestMove, GlobalVars.player.transform.position);

        // ✅ Prevents jumping if already close to the player
        return projectedDistanceToPlayer <= 1.0f && currentDistanceToPlayer > 2.0f;
    }

    IEnumerator JumpAnimation(Vector3 targetPosition)
    {
        isJumping = true; // Prevent movement during jump
        float jumpDuration = 0.6f; // Total time for the jump
        float peakHeight = 1.0f; // How high the jump reaches
        Vector3 startPosition = transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;
            
            // Create an arc for the jump using Sin function
            float heightOffset = Mathf.Sin(t * Mathf.PI) * peakHeight;

            // Smoothly move the knight toward the target position in an arc
            transform.position = Vector3.Lerp(startPosition, targetPosition, t) + new Vector3(0, heightOffset, 0);

            // Scale up at peak of jump, then back down
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.3f, Mathf.Sin(t * Mathf.PI));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one; // Reset scale

        if (landingDustPrefab != null)
        {
            ParticleSystem dustInstance = Instantiate(landingDustPrefab, targetPosition - new Vector3(0,0.3f, 0), Quaternion.identity);
            dustInstance.Play();
            Destroy(dustInstance.gameObject, dustInstance.main.duration + dustInstance.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning("Landing dust particle system not assigned.");
        }

        isJumping = false; // Allow movement again
    }
}
