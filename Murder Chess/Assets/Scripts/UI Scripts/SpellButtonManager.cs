using UnityEngine;
using UnityEngine.UI;

public class SpellButtonManager : MonoBehaviour
{
    [SerializeField] private Player player;                     
    [SerializeField] private Image iconImage;
    [SerializeField] private Image cooldownOverlay;

    private BaseSpell trackedSpell;

    public void Bind(BaseSpell spell)
    {
        trackedSpell = spell;

        iconImage.sprite = spell.GetComponent<ShopItem>().image;

        gameObject.SetActive(trackedSpell != null);
    }

    private void Update()
    {
        if (trackedSpell == null) return;

        float cost = trackedSpell.SpellCost;
        float current = player.PlayerMana;

        if (cost <= 0)
        {
            cooldownOverlay.fillAmount = 1f;
            return;
        }

        float fillAmount = Mathf.Clamp01(1 - (current / cost));
        cooldownOverlay.fillAmount = fillAmount;
    }
}
