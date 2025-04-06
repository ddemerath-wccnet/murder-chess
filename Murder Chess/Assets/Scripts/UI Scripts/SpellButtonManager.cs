using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellButtonManager : MonoBehaviour
{
    [SerializeField] private Player player;                     
    [SerializeField] private Image iconImage;
    [SerializeField] private Image cooldownOverlay;        
    [SerializeField] private TextMeshProUGUI cooldownText;

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

        cooldownOverlay.fillAmount = current / cost;
    }
}
