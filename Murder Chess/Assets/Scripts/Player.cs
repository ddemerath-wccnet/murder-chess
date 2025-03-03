using UnityEngine;

public class Player : MonoBehaviour
{
    //Keybinds
    public KeyCode key_Up = KeyCode.W;
    public KeyCode key_Left = KeyCode.A;
    public KeyCode key_Down = KeyCode.S;
    public KeyCode key_Right = KeyCode.D;

    public KeyCode key_Movement = KeyCode.Space;

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
    private float base_PlayerSpeed = 1;
    public float PlayerSpeed
    {   //Uses multipliers to correctly calculate var
        get { return base_PlayerSpeed * GlobalVars.multiplier_PlayerSpeed; }
        set { base_PlayerSpeed = value / GlobalVars.multiplier_PlayerSpeed; }
    }

    private void Awake()
    {
        GlobalVars.ResetClass();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base_PlayerHealth = base_MaxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Define move dir based on user input
        Vector3 moveDir = new Vector3();
        if (Input.GetKey(key_Up)) moveDir += Vector3.up;
        if (Input.GetKey(key_Left)) moveDir += Vector3.left;
        if (Input.GetKey(key_Down)) moveDir += Vector3.down;
        if (Input.GetKey(key_Right)) moveDir += Vector3.right;

        //Executes Movement
        transform.position += GlobalVars.DeltaTimePlayer * PlayerSpeed * moveDir;
    }

    /// <summary> Damages Player for '<c>damage</c>' damage </summary>
    public void DamagePlayer(float damage)
    {
        PlayerHealth = PlayerHealth - damage;
    }
}
