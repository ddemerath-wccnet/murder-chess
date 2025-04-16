using UnityEngine;
using TMPro;

public class DiceRoll : BaseAbility
{
    public float maxAbilityLengthTimer = 0.1f;
    public float abilityLengthTimer;
    public TextMeshPro roll1Text;
    public GameObject roll1Object;
    public TextMeshPro roll2Text;
    public GameObject roll2Object;
    public int roll1;
    public int roll2;
    public int highestRoll;
    public float playerHealthChange;

    protected override void AbilityStart()
    {
        Player player = GlobalVars.player.GetComponent<Player>();

        abilityLengthTimer = maxAbilityLengthTimer;

        roll1 = Random.Range(1, 21);
        roll2 = Random.Range(1, 21);      

        roll1Text.text = roll1.ToString();
        roll2Text.text = roll2.ToString();

        if(roll1 > 11) 
        {
            roll1Text.color = Color.green;
        }
        else if (roll1 < 9) 
        {
            roll1Text.color = Color.red;
        }
        else 
        {
            roll1Text.color = Color.yellow;
        }

        if(roll2 > 11) 
        {
            roll2Text.color = Color.green;
        }
        else if (roll2 < 9) 
        {
            roll2Text.color = Color.red;
        }
        else 
        {
            roll2Text.color = Color.yellow;
        }

        if(roll1 >= roll2) 
        {
            highestRoll =  roll1;
        }
        else 
        {
            highestRoll = roll2;
        }

        if (roll1 == 20 && roll2 == 20) 
        {
            playerHealthChange = 10;
        }
        else if (roll1 == 1 && roll2 == 1) 
        {
            playerHealthChange = -7.5f;
        }
        else
        {
            playerHealthChange = (highestRoll - 10) / 2;
        }

        if(player.MaxPlayerHealth - player.PlayerHealth < playerHealthChange) {

            playerHealthChange = player.MaxPlayerHealth - player.PlayerHealth;

        }

        player.DamagePlayer(-playerHealthChange);

        roll1Object.SetActive(true);
        roll2Object.SetActive(true);

    }

    protected override bool AbilityUpdate()
    {

        abilityLengthTimer -= GlobalVars.DeltaTimePlayer;

        if (abilityLengthTimer > 0)
        {
            // attackArea.SetActive(true);

            // abilityLengthTimer -= GlobalVars.DeltaTimePlayer;
            // GlobalVars.player.GetComponent<Player>().isDangerous = true;
            return false;
        }
        else
        {
            roll1Object.SetActive(false);
            roll2Object.SetActive(false);

            // attackArea.SetActive(false);

            // GlobalVars.player.GetComponent<Player>().isDangerous = false;
            return true;
        }
    }
}
