using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButtonManager : MonoBehaviour
{
    [SerializeField] private Image iconImage;               
    [SerializeField] private Image cooldownOverlay;         
    [SerializeField] private TextMeshProUGUI cooldownText;

    private BaseAbility trackedAbility;

    public void Bind(BaseAbility ability)
    {
        trackedAbility = ability;

        // iconImage.sprite = ability.icon;

        gameObject.SetActive(trackedAbility != null);
    }

    private void Update()
    {
        if (trackedAbility == null) return;

        float max = trackedAbility.MaxAbilityCooldown;
        float current = trackedAbility.AbilityCooldown;

        cooldownOverlay.fillAmount = current / max;
        cooldownText.text = current > 0 ? Mathf.CeilToInt(current).ToString() : "";
    }
}
