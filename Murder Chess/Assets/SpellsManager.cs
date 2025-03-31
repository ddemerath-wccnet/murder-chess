using System.Collections.Generic;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    Player player;
    public List<BaseSpell> mySpells = new List<BaseSpell>();
    float oldSpellAmount;
    int maxSpells = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GlobalVars.player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oldSpellAmount != transform.childCount)
        {
            oldSpellAmount = transform.childCount;
            mySpells.Clear();

            int i = 0;
            foreach (Transform child in transform)
            {
                BaseSpell spell;
                if (child.TryGetComponent<BaseSpell>(out spell))
                {
                    i++;
                    if (i > maxSpells) GameObject.Destroy(spell.gameObject);
                    else
                    {
                        mySpells.Add(spell);
                        switch (i)
                        {
                            case 1:
                                player.Spell1 = spell;
                                break;
                            case 2:
                                player.Spell2 = spell;
                                break;
                            case 3:
                                player.Spell3 = spell;
                                break;
                            default:
                                // code block
                                break;
                        }
                    }
                }
            }
        }
    }
}
