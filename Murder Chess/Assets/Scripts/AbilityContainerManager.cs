using UnityEngine;

public class AbilityContainer : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private AbilityButtonManager abilityButton1; 
    [SerializeField] private AbilityButtonManager abilityButton2;
    [SerializeField] private AbilityButtonManager abilityButton3;
    [SerializeField] private AbilityButtonManager abilityButton4;
    [SerializeField] private AbilityButtonManager abilityButton5;

    private void Update()
    {
        if (player.Ability1 != null)
        {
            abilityButton1.gameObject.SetActive(true);
            abilityButton1.Bind(player.Ability1);
        }
        else
        {
            abilityButton1.gameObject.SetActive(false);
        }

        if (player.Ability2 != null)
        {
            abilityButton2.gameObject.SetActive(true);
            abilityButton2.Bind(player.Ability2);
        }
        else
        {
            abilityButton2.gameObject.SetActive(false);
        }

        if (player.Ability3 != null)
        {
            abilityButton3.gameObject.SetActive(true);
            abilityButton3.Bind(player.Ability3);
        }
        else
        {
            abilityButton3.gameObject.SetActive(false);
        }

        if (player.Ability4 != null)
        {
            abilityButton4.gameObject.SetActive(true);
            abilityButton4.Bind(player.Ability4);
        }
        else
        {
            abilityButton4.gameObject.SetActive(false);
        }

        if (player.Ability5 != null)
        {
            abilityButton5.gameObject.SetActive(true);
            abilityButton5.Bind(player.Ability5);
        }
        else
        {
            abilityButton5.gameObject.SetActive(false);
        }
    }
}
