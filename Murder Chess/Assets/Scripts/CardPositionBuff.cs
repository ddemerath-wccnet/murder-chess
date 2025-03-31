using UnityEngine;

public class CardPositionBuff : BaseCard
{
    [SerializeField]
    private float base_whiteSquareVarModifier = -1;
    public float WhiteSquareVarModifier
    {   //Uses multipliers to correctly calculate var
        get { if (inHand) return base_whiteSquareVarModifier * GlobalVars.multiplier_CardHandMultiplier;
            else return base_whiteSquareVarModifier * GlobalVars.multiplier_CardDeckMultiplier; }
        set { if (inHand) base_whiteSquareVarModifier = value / GlobalVars.multiplier_CardHandMultiplier;
            else base_whiteSquareVarModifier = value / GlobalVars.multiplier_CardDeckMultiplier; }
    }

    bool onBlackSquare;
    bool oldOnBlackSquare;

    public override void Update()
    {
        base.Update();
        onBlackSquare = GlobalVars.player.GetComponent<Player>().onBlackSquare;
        if (onBlackSquare != oldOnBlackSquare)
        {
            oldOnBlackSquare = onBlackSquare;
            UpdateCard();
        }
    }

    public override void ActivateCard()
    {
        if (onBlackSquare) modifier = new ModifierEntry(varName, VarModifier);
        else modifier = new ModifierEntry(varName, WhiteSquareVarModifier);

    }

    public override void DeactivateCard()
    {
        modifier.DestroySelf();
        modifier = null;
    }
}
