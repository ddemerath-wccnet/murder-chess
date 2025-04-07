using UnityEngine;

public class StatusEffectVisualizer : MonoBehaviour
{
    public HealthBar healthBar;
    public int statusNum;
    public BaseStatusEffect baseStatusEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (statusNum < healthBar.activeEffects.Count)
        {
            baseStatusEffect = healthBar.activeEffects[statusNum];
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1,1,1, baseStatusEffect.duration / baseStatusEffect.maxDuration);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, baseStatusEffect.duration / baseStatusEffect.maxDuration);
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = baseStatusEffect.image;

            transform.position = healthBar.currentHealthBar.transform.position + ((new Vector3(0.75f, 1f, 0) + (new Vector3(2, 0, 0) * statusNum)) * transform.parent.lossyScale.x);
        }
        else
        {
            baseStatusEffect = null;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
