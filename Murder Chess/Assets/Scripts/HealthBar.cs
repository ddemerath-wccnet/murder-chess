using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Player player;

    public float maxHealth;
    public float currentHealth;
    public GameObject maxHealthBar;
    public GameObject currentHealthBar;
    public float SpriteSizeMulti = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {                // IMPORTAINT!!!!!!!!!!
					//find a better way to do this this is stupid, it happens EVERY FRAME
        maxHealth = player.MaxPlayerHealth;
        currentHealth = player.PlayerHealth;
        maxHealthBar.transform.localScale = new Vector3(maxHealth * SpriteSizeMulti, 1, 1);
        currentHealthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
    }
}
