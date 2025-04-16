using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public bool isPiece = true;
    public bool isPlayer;
    public Player player;
    public BasePiece piece;

    public float maxHealth;
    public float currentHealth;
    public GameObject maxHealthBar;
    public GameObject currentHealthBar;
    public float SpriteSizeMulti = 1;
    public List<BaseStatusEffect> activeEffects;

    public GameObject statusEffects;
    public GameObject statusPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {                // IMPORTAINT!!!!!!!!!!
        if (isPiece) //find a better way to do this this is stupid, it happens EVERY FRAME
        {
            maxHealth = piece.MaxPieceHealth;
            currentHealth = piece.PieceHealth;

            activeEffects = piece.activeEffects;
        }
        else if (isPlayer)
        {
            maxHealth = player.MaxPlayerHealth;
            currentHealth = player.PlayerHealth;

            activeEffects = player.activeEffects;
        }

        if (activeEffects.Count > statusEffects.transform.childCount)
        {
            GameObject newStatus = GameObject.Instantiate(statusPrefab);
            newStatus.transform.SetParent(statusEffects.transform, false);
            newStatus.name = "Status " + newStatus.transform.GetSiblingIndex();
            newStatus.GetComponent<StatusEffectVisualizer>().statusNum = newStatus.transform.GetSiblingIndex();
        }

        maxHealthBar.transform.localScale = new Vector3(maxHealth * SpriteSizeMulti, 1, 1);
        currentHealthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
        if (currentHealth < 0)
        {
            currentHealthBar.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
