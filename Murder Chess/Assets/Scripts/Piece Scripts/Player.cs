using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Keybinds")] //Keybinds
    public KeyCode key_Up = KeyCode.W;
    public KeyCode key_Left = KeyCode.A;
    public KeyCode key_Down = KeyCode.S;
    public KeyCode key_Right = KeyCode.D;

    public KeyCode key_Movement = KeyCode.Space;

    public KeyCode key_Ability1 = KeyCode.Alpha1;
    public KeyCode key_Ability2 = KeyCode.Alpha2;
    public KeyCode key_Ability3 = KeyCode.Alpha3;
    public KeyCode key_Ability4 = KeyCode.Alpha4;
    public KeyCode key_Ability5 = KeyCode.Alpha5;

    public KeyCode key_Spell1 = KeyCode.Mouse0;
    public KeyCode key_Spell2 = KeyCode.Mouse2;
    public KeyCode key_Spell3 = KeyCode.Mouse1;

    [Header("Player Properties")]
    /* Player Properties
     * Private Base are their actual values, 
    while the public get/set version uses the multipliers 
    so cards and other things can easily modify them */
    [SerializeField]
    private float base_MaxPlayerHealth = 10;
    public float MaxPlayerHealth
    {   //Uses multipliers to correctly calculate var
        get { return base_MaxPlayerHealth * GlobalVars.multiplier_PlayerHealth; }
        set { base_MaxPlayerHealth = value / GlobalVars.multiplier_PlayerHealth; }
    }
    [SerializeField]
    private float base_PlayerHealth;
    public float PlayerHealth
    {   //Uses multipliers to correctly calculate var
        get { return base_PlayerHealth * GlobalVars.multiplier_PlayerHealth; }
        set { base_PlayerHealth = value / GlobalVars.multiplier_PlayerHealth; }
    }
    [SerializeField]
    private float base_PlayerRegen = 0.1f;
    public float PlayerRegen
    {   //Uses multipliers to correctly calculate var
        get { return base_PlayerRegen * GlobalVars.multiplier_PlayerRegen; }
        set { base_PlayerRegen = value / GlobalVars.multiplier_PlayerRegen; }
    }
    [SerializeField]
    private float base_PlayerSpeed = 1;
    public float PlayerSpeed
    {   //Uses multipliers to correctly calculate var
        get { return base_PlayerSpeed * GlobalVars.multiplier_PlayerSpeed; }
        set { base_PlayerSpeed = value / GlobalVars.multiplier_PlayerSpeed; }
    }
    [SerializeField]
    private float base_PlayerDamage = 1;
    public float PlayerDamage
    {   //Uses multipliers to correctly calculate var
        get { return base_PlayerDamage * GlobalVars.multiplier_PlayerDamage; }
        set { base_PlayerDamage = value / GlobalVars.multiplier_PlayerDamage; }
    }
    [SerializeField]
    private float base_MaxPlayerMana = 10;
    public float MaxPlayerMana
    {   //Uses multipliers to correctly calculate var
        get { return base_MaxPlayerMana * GlobalVars.multiplier_PlayerMana; }
        set { base_MaxPlayerMana = value / GlobalVars.multiplier_PlayerMana; }
    }
    [SerializeField]
    private float base_PlayerMana;
    public float PlayerMana
    {   //Uses multipliers to correctly calculate var
        get { return base_PlayerMana * GlobalVars.multiplier_PlayerMana; }
        set { base_PlayerMana = value / GlobalVars.multiplier_PlayerMana; }
    }

    public float Coins;

    public float maxIFrames = 1;
    public float iFrames = 0;
    public bool isDangerous;

    [Header("Abilities")]
    public BaseAbility Ability1 = null;
    public BaseAbility Ability2 = null;
    public BaseAbility Ability3 = null;
    public BaseAbility Ability4 = null;
    public BaseAbility Ability5 = null;

    public BaseAbility AbilityMovement = null;

    [Header("Spells")]
    public BaseSpell Spell1 = null;
    public BaseSpell Spell2 = null;
    public BaseSpell Spell3 = null;

    public Vector3 moveDir = new Vector3();

    public GameObject ChessBoard;
    public bool onBlackSquare;

    public List<BaseStatusEffect> activeEffects = new List<BaseStatusEffect>();

    private void Awake()
    {
        GlobalVars.ResetClass();
        CoinParticle.StartUp();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base_PlayerHealth = base_MaxPlayerHealth;
        base_PlayerMana = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (iFrames > 0) iFrames -= GlobalVars.DeltaTimePlayer;

        //Define move dir based on user input
        moveDir = new Vector3();
        if (Input.GetKey(key_Up)) moveDir += Vector3.up;
        if (Input.GetKey(key_Left)) moveDir += Vector3.left;
        if (Input.GetKey(key_Down)) moveDir += Vector3.down;
        if (Input.GetKey(key_Right)) moveDir += Vector3.right;
        moveDir.Normalize();

        //Executes Movement
        transform.position += GlobalVars.DeltaTimePlayer * PlayerSpeed * moveDir;

        // Visualizer
        if (isDangerous) 
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

        // Ability Activation
        if (Input.GetKeyDown(key_Ability1) && Ability1 != null) Ability1.CallActivate();
        if (Input.GetKeyDown(key_Ability2) && Ability2 != null) Ability2.CallActivate();
        if (Input.GetKeyDown(key_Ability3) && Ability3 != null) Ability3.CallActivate();
        if (Input.GetKeyDown(key_Ability4) && Ability4 != null) Ability4.CallActivate();
        if (Input.GetKeyDown(key_Ability5) && Ability5 != null) Ability5.CallActivate();

        if (Input.GetKeyDown(key_Movement) && AbilityMovement != null) AbilityMovement.CallActivate();

        // Spell Activation
        if (Input.GetKeyDown(key_Spell1) && Spell1 != null) Spell1.CallActivate();
        if (Input.GetKeyDown(key_Spell2) && Spell2 != null) Spell2.CallActivate();
        if (Input.GetKeyDown(key_Spell3) && Spell3 != null) Spell3.CallActivate();

        // Health Regen
        if (GlobalVars.timeScale_Player > 0 && PlayerHealth < MaxPlayerHealth)
        {
            PlayerHealth = PlayerHealth + (PlayerRegen * GlobalVars.DeltaTimePlayer);
        }

        // Check if on black square
        Vector3 ChessBoardRelativePos = transform.position - ChessBoard.transform.position;
        ChessBoardRelativePos = ChessBoardRelativePos / ChessBoard.transform.localScale.x;
        int CBR_x = Mathf.FloorToInt(ChessBoardRelativePos.x);
        int CBR_y = Mathf.FloorToInt(ChessBoardRelativePos.y);
        if ((CBR_x + CBR_y) % 2 == 0) onBlackSquare = false;
        else onBlackSquare = true;

        // Status Effects
        foreach (BaseStatusEffect baseStatusEffect in new List<BaseStatusEffect>(activeEffects))
        {
            baseStatusEffect.Run(true);
        }

        // Testing Effect
        if (Input.GetKeyDown(KeyCode.F))
        {
            new PoisonEffect(this, 10);
            foreach (BasePiece basePiece in FindObjectsByType<BasePiece>(FindObjectsSortMode.None))
            {
                new PoisonEffect(basePiece, 10);
            }
        }
        Debug.Log(activeEffects.Count);
    }

    /// <summary> Damages Player for '<c>damage</c>' damage </summary>
    public void DamagePlayer(float damage)
    {
        if (iFrames > 0)
        {

        }
        else
        {
            PlayerHealth = Mathf.Clamp(PlayerHealth - damage, -0.01f, float.MaxValue);
            iFrames = maxIFrames;
        }
    }

    /*
     A piece Colliding with player, do damage calculations!
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BasePiece collisionPiece;

        if (collision.TryGetComponent<BasePiece>(out collisionPiece))
        {
            HitPiece(collisionPiece);
        }
    }

    public void HitPiece(BasePiece collisionPiece, float playerDmgMulti = 1, float pieceDmgMulti = 1, float manaMulti = 1)
    {
        if (collisionPiece.isDangerous == false && this.isDangerous == false)
        {
            playerDmgMulti *= 0.5f;
            pieceDmgMulti *= 0.5f;
        }
        else if (collisionPiece.isDangerous == false && this.isDangerous == true)
        {
            playerDmgMulti *= 0;
            pieceDmgMulti *= 1;
        }
        else if (collisionPiece.isDangerous == true && this.isDangerous == false)
        {
            playerDmgMulti *= 1;
            pieceDmgMulti *= 0;
        }
        else if (collisionPiece.isDangerous == true && this.isDangerous == true)
        {
            playerDmgMulti *= 0.75f;
            pieceDmgMulti *= 0.75f;
        }

        if (playerDmgMulti > 0 && iFrames <= 0) DamagePlayer(collisionPiece.HurtPlayerFor() * playerDmgMulti);
        if (pieceDmgMulti > 0)
        {
            collisionPiece.DamagePiece(PlayerDamage * pieceDmgMulti);
            PlayerMana = Mathf.Clamp(PlayerMana + (1 * GlobalVars.multiplier_PlayerManaGain * manaMulti) , 0, MaxPlayerMana);
        }
    }
}
