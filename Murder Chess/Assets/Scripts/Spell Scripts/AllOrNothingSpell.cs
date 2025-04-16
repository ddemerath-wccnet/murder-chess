using UnityEngine;

public class AllOrNothingSpell : BaseSpell
{

    public float spellTimer;
    public float playerDamageModifier;
    public float pieceDamageModifier;
    private float instanceSpellTimer;

    protected override void SpellStart()
    {
        Debug.Log("spell start");
        new ModifierEntry("multiplier_PlayerDamage", playerDamageModifier, 10);
        new ModifierEntry("multiplier_PieceDamage", pieceDamageModifier, 10);
        instanceSpellTimer = spellTimer;
    }

    protected override bool SpellUpdate()
    {
        instanceSpellTimer -= GlobalVars.DeltaTimePlayer;

        if (instanceSpellTimer < 0)
        {
            // GlobalVars.player.GetComponent<Player>().PlayerRegen -= 0.5f;
            return true;
        }
        else return false;
    }
}
