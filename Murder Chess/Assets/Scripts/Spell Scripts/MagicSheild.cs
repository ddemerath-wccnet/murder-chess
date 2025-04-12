//using System.Diagnostics;
using UnityEngine;

public class MagicShield : BaseSpell
{
    private SpriteRenderer shieldSprite;
    private float spellTimer;


    protected override void SpellStart()
    {
        spellTimer = 8f;
        new ModifierEntry("multiplier_PieceDamage", -99, 8);

        shieldSprite = GetComponentInChildren<SpriteRenderer>();

        if (shieldSprite == null)
        {
            Debug.LogError("Sheild sprite not found");
        }
        else
        {
            Debug.Log("sheild sprite found ");
            Color color = shieldSprite.color;
            color.a = .75f;
            shieldSprite.color = color;
            shieldSprite.enabled = true;
        }
    }


    protected override bool SpellUpdate()
    {
        spellTimer -= GlobalVars.DeltaTimePlayer;
        Debug.Log("spelltimer = " + spellTimer);

        if (spellTimer <= 0)
        {
            Color color = shieldSprite.color;
            color.a = 0f;
            shieldSprite.color = color;

            shieldSprite.enabled = false;

            return true;
        }

        return false;
    }
}
