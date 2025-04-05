using UnityEngine;

public class PauseTime : BaseSpell
{


    protected override void SpellStart()
    {
        new ModifierEntry("multiplier_PieceSpeed", -99, 4);

    }

    protected override bool SpellUpdate()
    {
        return true;
    }
}
