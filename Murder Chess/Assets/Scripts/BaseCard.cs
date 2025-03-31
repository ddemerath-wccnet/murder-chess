using UnityEngine;

public class BaseCard : MonoBehaviour
{
    public bool active;
    public bool inHand;
    private bool acitveSelf;
    public string varName;
    [SerializeField]
    private float base_VarModifier = 1;
    public float VarModifier
    {   //Uses multipliers to correctly calculate var
        get { if (inHand) return base_VarModifier * GlobalVars.multiplier_CardHandMultiplier;
            else return base_VarModifier * GlobalVars.multiplier_CardDeckMultiplier; }
        set { if (inHand) base_VarModifier = value / GlobalVars.multiplier_CardHandMultiplier;
            else base_VarModifier = value / GlobalVars.multiplier_CardDeckMultiplier; }
    }
    public ModifierEntry modifier;

    public string suit;
    public int rank;

    private float multiplier_CardHandMultiplier_Self;
    private float multiplier_CardDeckMultiplier_Self;
    private bool inHandSelf;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //if (transform.parent.name == "Hand") inHand = true;
        //else inHand = false;

        if (active && !acitveSelf)
        {
            acitveSelf = true;
            ActivateCard();
        }
        else if (!active && acitveSelf)
        {
            acitveSelf = false;
            DeactivateCard();
        }

        if (inHand != inHandSelf
            || GlobalVars.multiplier_CardHandMultiplier != multiplier_CardHandMultiplier_Self
            || GlobalVars.multiplier_CardDeckMultiplier != multiplier_CardDeckMultiplier_Self)
        {
            inHandSelf = inHand;
            multiplier_CardHandMultiplier_Self = GlobalVars.multiplier_CardHandMultiplier;
            multiplier_CardDeckMultiplier_Self = GlobalVars.multiplier_CardDeckMultiplier;
            UpdateCard();
        }
    }

    public virtual void ActivateCard()
    {
        modifier = new ModifierEntry(varName, VarModifier);
    }

    public virtual void DeactivateCard()
    {
        modifier.DestroySelf();
        modifier = null;
    }

    public virtual void UpdateCard()
    {
        if (active)
        {
            DeactivateCard();
            ActivateCard();
        }
    }
}
