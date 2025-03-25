using UnityEngine;

public class AbilityContainer : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private AbilityButtonManager abilityButton; // Reference to a single slot for now

    private void Start()
    {
        if (player.Ability1 != null)
        {
            abilityButton.Bind(player.Ability1);
        }
        else
        {
            abilityButton.gameObject.SetActive(false);
        }
    }
}
