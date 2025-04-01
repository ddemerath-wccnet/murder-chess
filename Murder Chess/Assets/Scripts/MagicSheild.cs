//using System.Diagnostics;
using UnityEngine;

public class MagicShield : BaseSpell
{
    private SpriteRenderer shieldSprite;
    private float spellTimer;


    protected override void SpellStart()
    {
        spellTimer = 8f;
        Debug.Log("Spell started. Initial spellTimer: " + spellTimer);
        new ModifierEntry("multiplier_PieceDamage", -99, 8);
        shieldSprite = GetComponent<SpriteRenderer>();
        Color color = shieldSprite.color;
        color.a = .75f;
        shieldSprite.color = color;
        shieldSprite.enabled = true;


    }

    protected override bool SpellUpdate()
    {
        float previousTimer = spellTimer;

        spellTimer -= GlobalVars.DeltaTimePlayer;
        Debug.Log("spellTimer difference: " + (previousTimer - spellTimer));
        Debug.Log("spellTimer is: " + spellTimer);
        if (spellTimer <= 0)
        {

            Debug.Log("spellTimer is: " + spellTimer);
            //shieldSprite = GetComponent<SpriteRenderer>();
            Color color = shieldSprite.color;
            color.a = 0;
            shieldSprite.color = color;
            shieldSprite.enabled = false;
        }

        return true;
    }
}
