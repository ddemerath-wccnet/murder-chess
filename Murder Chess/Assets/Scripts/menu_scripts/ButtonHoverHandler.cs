using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI hoverText;

    void Start()
    {
        SetAlpha(0);
    }

    private void SetAlpha(float alpha)
    {
        Color currentColor = hoverText.color;
        currentColor.a = alpha;
        hoverText.color = currentColor;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        SetAlpha(1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetAlpha(0);
    }
}
