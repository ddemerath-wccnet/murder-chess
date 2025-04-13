using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BasePiece : MonoBehaviour
{
    /* Piece Properties
     * Private Base are their actual values, 
    while the public get/set version uses the multipliers 
    so cards and other things can easily modify them */
    protected Vector2 target;
    [SerializeField]
    private float base_MaxPieceHealth = 10;
    public float MaxPieceHealth
    {   //Uses multipliers to correctly calculate var
        get { return base_MaxPieceHealth * GlobalVars.multiplier_PieceHealth; }
        set { base_MaxPieceHealth = value / GlobalVars.multiplier_PieceHealth; }
    }
    [SerializeField]
    private float base_PieceHealth;
    public float PieceHealth
    {   //Uses multipliers to correctly calculate var
        get { return base_PieceHealth * GlobalVars.multiplier_PieceHealth; }
        set { base_PieceHealth = value / GlobalVars.multiplier_PieceHealth; }
    }
    [SerializeField]
    private float base_PieceSpeed = 1;
    public float PieceSpeed
    {   //Uses multipliers to correctly calculate var
        get { return base_PieceSpeed * GlobalVars.multiplier_PieceSpeed; }
        set { base_PieceSpeed = value / GlobalVars.multiplier_PieceSpeed; }
    }
    [SerializeField]
    private float base_PieceDamage;
    public float PieceDamage
    {   //Uses multipliers to correctly calculate var
        get { return base_PieceDamage * GlobalVars.multiplier_PieceDamage; }
        set { base_PieceDamage = value / GlobalVars.multiplier_PieceDamage; }
    }
    [SerializeField]
    private float base_MaxPieceCycleTimer = 5;
    public float MaxPieceCycleTimer
    {   //Uses multipliers to correctly calculate var
        get { return base_MaxPieceCycleTimer * GlobalVars.multiplier_PieceCycleTimer; }
        set { base_MaxPieceCycleTimer = value / GlobalVars.multiplier_PieceCycleTimer; }
    }
    [SerializeField]
    protected float PieceCycleTimer;

    public string cycleState = null;

    public float maxIFrames = 1;
    float iFrames = 0;
    public bool isDangerous;

    public List<BaseStatusEffect> activeEffects = new List<BaseStatusEffect>();
    List<ElementalPiece> elements = new List<ElementalPiece>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        base_PieceHealth = base_MaxPieceHealth;
        cycleState = "Select Target";
        elements = GetComponents<ElementalPiece>().ToList<ElementalPiece>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (iFrames > 0) iFrames -= GlobalVars.DeltaTimePiece;

        // State Machine

        // Death
        if (PieceHealth <= 0)
        {
            cycleState = "Death";
            if (Death()) SelfDestruct();
        }

        if (cycleState == "Select Target")
        {
            PieceCycleTimer = MaxPieceCycleTimer;
            Vector2? nullable = SelectTarget();
            if(nullable != null)
                target = nullable.Value;
            if (target != null) cycleState = "Move";
        }

        if (cycleState == "Move")
        {
            // Move
            Vector2 distance = target - (Vector2)transform.position;
            Vector2 moveDir = distance.normalized;
            float moveTimerNormalized = PieceCycleTimer / MaxPieceCycleTimer;
            if (Move(target, distance, moveDir, PieceCycleTimer, moveTimerNormalized))
            {
                cycleState = "Move Done";
            }

            // End of Move
            PieceCycleTimer -= GlobalVars.DeltaTimePiece;
            if (PieceCycleTimer <= 0)
            {
                cycleState = "Should Attack";
            }
        }

        if (cycleState == "Move Done")
        {
            // Move Done
            if (MoveDone())
            {
                cycleState = "Should Attack";
            }

            // End of Move
            PieceCycleTimer -= GlobalVars.DeltaTimePiece;
            if (PieceCycleTimer <= 0)
            {
                cycleState = "Should Attack";
            }
        }

        // Should I Attack? Logic
        if (cycleState == "Should Attack")
        {
            if (ShouldAttack())
            {
                cycleState = "Attack";
            }
            else
            {
                cycleState = "Select Target";
            }
        }

        // Attacking State
        if (cycleState == "Attack")
        {
            if (Attack(out isDangerous))
            {
                cycleState = "Select Target";
                isDangerous = false;
            }
        }


        if (isDangerous) // isDangerous Visualizer
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (iFrames > 0)
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color - new Color(0, 0, 0, 0.5f);
        }

        // Status Effects
        foreach (BaseStatusEffect baseStatusEffect in new List<BaseStatusEffect>(activeEffects))
        {
            baseStatusEffect.Run();
        }
    }

    /// <summary> Used to select the target to be passed into the move method </summary>
    /// <returns> Return vector 3 target </returns>
    public abstract Vector3? SelectTarget();

    /// <summary> Called once a frame durring the movement state, defines GameObject movement using input params. </summary>
    /// <param name="target">The Location of the Target in space.</param>
    /// <param name="distance">The signed Distance to the Target (difference between piece location and target).</param>
    /// <param name="moveDir">The Normalized Direction to the Target.</param>
    /// <param name="moveTime">Time Remaining to move, starts at <c>MaxPieceCycleTimer</c> and ends at 0.</param>
    /// <param name="moveTimerNormalized">Scaled/Normalized Time Remaining to move, starts at 1, and ends at 0.</param>
    /// <returns> Return False to continue to <c>Move()</c>, return true to go to <c>MoveDone()</c> method. </returns>
    public abstract bool Move(Vector2 target, Vector2 distance, Vector2 moveDir, float moveTime, float moveTimerNormalized);

    /// <summary> Called once a frame while done moving and cycle timer still has time left. </summary>
    /// <returns> Return False to continue to <c>MoveDone()</c>, return true to go to <c>ShouldAttack()</c> method. </returns>
    public virtual bool MoveDone() //Default Implementation
    {
        return false; // wait for next step
    }

    /// <summary> Determines Whether piece moves to Attack state </summary>
    /// <returns> Return False to go back to movement, return true to go to attacking </returns>
    public abstract bool ShouldAttack();

    /// <summary> Called once per frame durring attack state, no timer, so you must define when the attack ends by returning true. 
    /// isDangerous represents whether the piece can hurt you if you touch it (ex red piece in the demo)</summary>
    /// <returns> Return true to finish attack </returns>
    public abstract bool Attack(out bool isDangerous);

    /// <summary> Damages Piece for '<c>damage</c>' damage </summary>
    public virtual void DamagePiece(float damage) //Default Implementation
    {
        if (iFrames > 0)
        {

        }
        else
        {
            PieceHealth = Mathf.Clamp(PieceHealth - damage, -0.01f, float.MaxValue);
            iFrames = maxIFrames;
        }
    }

    /// <summary> Called when piece will damage player </summary>
    /// <returns> Return amount of damage to do </returns>
    public virtual float HurtPlayerFor() //Default Implementation
    {
        foreach (ElementalPiece element in elements)
        {
            element.EffectPlayer();
        }
        return PieceDamage;
    }

    /// <summary> Called once per frame while at 0hp </summary>
    /// <returns> Return true to delete piece </returns>
    public virtual bool Death() //Default Implementation
    {
        return true;
    }

    /// <summary> Called once after death </summary>
    public virtual void SelfDestruct() //Default Implementation
    {
        WaveItem waveItem;
        if (TryGetComponent<WaveItem>(out waveItem))
        {
            float PieceCoinValue = waveItem.PieceCoinValue;
            GameObject particleParent = GameObject.Find("ParticleParent");
            if (particleParent != null) CoinParticle.SpawnParticles(PieceCoinValue, transform.position, 0.5f, particleParent.transform);
            else CoinParticle.SpawnParticles(PieceCoinValue, transform.position);
        }
        GameObject.Destroy(gameObject);
    }
}
