using UnityEngine;

public class BishopLevel2 : Bishop
{
    //The range at which the Bishop will activate the special ability.
    private int specialRange = 2;
    //The maxSpecialCooldown is the maximum amount of time the Bishop must wait in between using the special ability.
    private float maxSpecialCooldown = 18.5f;
    //The specialCooldown is the current amount time the Bishop must wait in between using the special ability.
    private float specialCooldown;
    //Ability Prefab object for the x-shaped walls that the Bishop can create
    private GameObject abilityPrefab;
    
    
    protected override void Start()
    {
        base.Start();
        //Define the ability prefab
        abilityPrefab = Resources.Load<GameObject>("Prefabs/xwall");
        PieceHealth = MaxPieceHealth;
        specialCooldown = maxSpecialCooldown;
    }
    
    // SpecialCooldown is called once per frame
    protected override void SpecialCooldown()
    {
        //If the player is within range and the cooldown for the Bishop's special ability is not active, the Bishop will
        //activate the special ability.
        //If the player is not in range and the cooldown is more than 0...
        if (Vector2.Distance(GlobalVars.player.transform.position, transform.position) >= specialRange || specialCooldown > 0)
        {
            //Count down the cooldown
            specialCooldown -= Time.deltaTime;
        }
        //If the cooldown is at or below 0 and the player is in range, the Bishop will spawn the special ability prefab
        else if (Vector2.Distance(GlobalVars.player.transform.position, transform.position) <= specialRange && specialCooldown <= 0)
        {
            //Check if abilityPrefab was created properly.
            if (!abilityPrefab)
            {
                abilityPrefab = Resources.Load<GameObject>("Prefabs/AbilityObjects/xwall");
            }
            //Spawn the xwall prefab
            Instantiate(abilityPrefab, transform.position, Quaternion.identity);
            //Reset the cooldown
            specialCooldown = maxSpecialCooldown;
        }
    }
    
}
