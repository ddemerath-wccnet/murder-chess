using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WildMagicSpell : BaseSpell
{
    public List<BaseSpell> spells = new List<BaseSpell>();
    public float spellTimer;
    public float instanceSpellTimer;
    private int numberOfSpells;
    private BaseSpell spellCalled;
    private Vector3 playerPosition;

    void Start() {

        BaseSpell[] spellItemsTemp = Resources.LoadAll<BaseSpell>("Prefabs/Shop/Spells");
        spells = spellItemsTemp.ToList();
        numberOfSpells = spells.Count();
        spells.Remove(Resources.LoadAll<WildMagicSpell>("Prefabs/Shop/Spells")[0]);
    }

    protected override void SpellStart()
    {
        Debug.Log("spell start");
        playerPosition = GlobalVars.player.transform.position;
        spellCalled = Instantiate(spells[Random.Range(0 , (numberOfSpells + 1))], playerPosition, transform.rotation, transform);
        //BaseSpell baseSpellScript = spellCalled.GetComponent<BaseSpell>();
        spellCalled.SpellCost = 0;
        spellCalled.CallActivate();
        instanceSpellTimer = spellTimer;
        //baseSpellScript.SpellStart();
    }

    protected override bool SpellUpdate()
    {
        instanceSpellTimer -= GlobalVars.DeltaTimePlayer;

        if (instanceSpellTimer < 0)
        {
            GameObject.Destroy(spellCalled.gameObject);

            return true;
        }
        else return false;
    }
}
