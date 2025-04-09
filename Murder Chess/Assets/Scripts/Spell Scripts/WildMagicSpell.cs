using System.Collections.Generic;
using UnityEngine;

public class WildMagicSpell : BaseSpell
{
    public List<GameObject> spells = new List<GameObject>();
    public int numberOfSpells;
    public float spellTimer;
    public float instanceSpellTimer;
    private GameObject spellCalled;
    private Vector3 playerPosition;

    protected override void SpellStart()
    {
        Debug.Log("spell start");
        instanceSpellTimer = spellTimer;
        playerPosition = GlobalVars.player.transform.position;
        spellCalled = Instantiate(spells[Random.Range(0 , numberOfSpells)], playerPosition, transform.rotation, transform);
        BaseSpell baseSpellScript = spellCalled.GetComponent<BaseSpell>();

        //baseSpellScript.SpellStart();
    }

    protected override bool SpellUpdate()
    {
        instanceSpellTimer -= GlobalVars.DeltaTimePlayer;

        if (instanceSpellTimer < 0)
        {
            GameObject.Destroy(spellCalled);

            return true;
        }
        else return false;
    }
}
