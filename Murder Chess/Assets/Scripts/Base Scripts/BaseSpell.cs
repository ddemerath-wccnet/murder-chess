using UnityEngine;
using UnityEngine.WSA;

public abstract class BaseSpell : MonoBehaviour
{
    [SerializeField]
    private float base_SpellCost = 10;
    public float SpellCost
    {   //Uses multipliers to correctly calculate var
        get { return base_SpellCost / GlobalVars.multiplier_SpellCost; }
        set { base_SpellCost = value * GlobalVars.multiplier_SpellCost; }
    }

    bool startedSpell;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (startedSpell)
        {
            if (SpellUpdate())
            {
                startedSpell = false;
            }
        }
    }

    /// <summary> Called from Player when a key is pressed
    public bool CallActivate()
    {
        Debug.Log("CallActivate");
        if (//GlobalVars.player.GetComponent<Player>().PlayerMana >= SpellCost && 
            startedSpell == false && !GlobalVars.bricked)
        {
            GlobalVars.player.GetComponent<Player>().PlayerMana = GlobalVars.player.GetComponent<Player>().PlayerMana - SpellCost;
            startedSpell = true;
            SpellStart();
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary> Called once when starting Spell
    protected abstract void SpellStart();

    /// <summary> Called once per frame durring Spell </summary>
    /// <returns> Return true to finish Spell </returns>
    protected abstract bool SpellUpdate();
}
