using UnityEngine;

public class Gigantify : BaseSpell
{
    public float spellTimerMax = 10f;
    public float spellTimer;

    protected override void SpellStart()
    {
        spellTimer = spellTimerMax;
        new ModifierEntry("multiplier_PlayerHealth", 3, spellTimerMax);
        new ModifierEntry("multiplier_PlayerRegen", 3, spellTimerMax);
        new ModifierEntry("multiplier_PlayerSpeed", 3, spellTimerMax);
        new ModifierEntry("multiplier_PlayerDamage", 3, spellTimerMax);
        GlobalVars.player.transform.localScale *= 2;
    }


    protected override bool SpellUpdate()
    {
        spellTimer -= GlobalVars.DeltaTimePlayer;
        Debug.Log("spelltimer = " + spellTimer);

        if (spellTimer <= 0)
        {
            GlobalVars.player.transform.localScale *= 1/2f;
            return true;
        }
        return false;
    }
}
