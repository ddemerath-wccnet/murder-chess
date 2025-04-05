using UnityEngine;

public class CardTradeOff : BaseCard
{
    public string tradeOffVarName;
    [SerializeField]
    private float base_tradeOffVarModifier = -1;
    public float tradeOffVarModifier
    {   //Uses multipliers to correctly calculate var
        get { if (inHand) return base_tradeOffVarModifier * GlobalVars.multiplier_CardHandMultiplier;
            else return base_tradeOffVarModifier * GlobalVars.multiplier_CardDeckMultiplier; }
        set { if (inHand) base_tradeOffVarModifier = value / GlobalVars.multiplier_CardHandMultiplier;
            else base_tradeOffVarModifier = value / GlobalVars.multiplier_CardDeckMultiplier; }
    }
    public ModifierEntry tradeOffModifier;

    public override void ActivateCard()
    {
        modifier = new ModifierEntry(varName, VarModifier);
        tradeOffModifier = new ModifierEntry(tradeOffVarName, tradeOffVarModifier);
    }

    public override void DeactivateCard()
    {
        modifier.DestroySelf();
        modifier = null;
        tradeOffModifier.DestroySelf();
        tradeOffModifier = null;
    }
}
