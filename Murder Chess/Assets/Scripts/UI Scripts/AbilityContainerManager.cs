using UnityEngine;

public class AbilityContainer : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private AbilityButtonManager[] abilityButtons;

    private void Update()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            BaseAbility ability = GetAbilityByIndex(i);
            if (ability != null)
            {
                abilityButtons[i].gameObject.SetActive(true);
                abilityButtons[i].Bind(ability);
            }
            else
            {
                abilityButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private BaseAbility GetAbilityByIndex(int index)
    {
        switch (index)
        {
            case 0: return player.Ability1;
            case 1: return player.Ability2;
            case 2: return player.Ability3;
            case 3: return player.Ability4;
            case 4: return player.Ability5;
            default: return null;
        }
    }
}
