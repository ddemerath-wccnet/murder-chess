using UnityEngine;
using UnityEngine.WSA;

public abstract class BaseAbility : MonoBehaviour
{
    [SerializeField]
    private float base_MaxAbilityCooldown = 10;
    public float MaxAbilityCooldown
    {   //Uses multipliers to correctly calculate var
        get { return base_MaxAbilityCooldown / GlobalVars.multiplier_AbilityCooldown; }
        set { base_MaxAbilityCooldown = value * GlobalVars.multiplier_AbilityCooldown; }
    }
    [SerializeField]
    private float base_AbilityCooldown = 0;
    public float AbilityCooldown
    {   //Uses multipliers to correctly calculate var
        get { return base_AbilityCooldown / GlobalVars.multiplier_AbilityCooldown; }
        set { base_AbilityCooldown = value * GlobalVars.multiplier_AbilityCooldown; }
    }

    public bool startedAbility;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        AbilityCooldown = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (startedAbility)
        {
            if (AbilityUpdate())
            {
                startedAbility = false;
            }
        }
        else if (AbilityCooldown > 0)
        {
            AbilityCooldown = AbilityCooldown - GlobalVars.DeltaTimePlayer;
        }
    }

    /// <summary> Called from Player when a key is pressed
    public virtual bool CallActivate()
    {
        if (AbilityCooldown <= 0)
        {
            base_AbilityCooldown = base_MaxAbilityCooldown;
            startedAbility = true;
            AbilityStart();
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary> Called once when starting ability
    protected abstract void AbilityStart();

    /// <summary> Called once per frame durring ability </summary>
    /// <returns> Return true to finish ability </returns>
    protected abstract bool AbilityUpdate();
}
