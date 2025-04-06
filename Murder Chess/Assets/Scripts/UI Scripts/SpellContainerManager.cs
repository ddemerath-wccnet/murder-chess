using UnityEngine;

public class SpellContainerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SpellButtonManager[] spellButtons;

    private void Update()
    {
        for (int i = 0; i < spellButtons.Length; i++)
        {
            BaseSpell spell = GetSpellsByIndex(i);
            if (spell != null)
            {
                spellButtons[i].gameObject.SetActive(true);
                spellButtons[i].Bind(spell);
            }
            else
            {
                spellButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private BaseSpell GetSpellsByIndex(int index)
    {
        switch (index)
        {
            case 0: return player.Spell1;
            case 1: return player.Spell2;
            case 2: return player.Spell3;
            default: return null;
        }
    }
}

