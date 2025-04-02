using UnityEngine;

public class RegenSpell : BaseSpell
{

    public float spellTimer;
    private float instanceSpellTimer;

    protected override void SpellStart()
    {
        Debug.Log("spell start");
        GlobalVars.player.GetComponent<Player>().PlayerRegen += 0.5f;
        instanceSpellTimer = spellTimer;
    }

    protected override bool SpellUpdate()
    {
        instanceSpellTimer -= GlobalVars.DeltaTimePlayer;

        if (instanceSpellTimer < 0)
        {
            GlobalVars.player.GetComponent<Player>().PlayerRegen -= 0.5f;
            return true;
        }
        else return false;
    }
}
