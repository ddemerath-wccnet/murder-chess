using UnityEngine;

public class GetOutOfJailFreeCard : BaseAbility
{
    public float maxAbilityLengthTimer;
    public float abilityLengthTimer;
    // public Vector2 movementDirection;
    public GameObject player;
    public float speed;
    public float regen;
    public float damage;
    protected override void AbilityStart()
    {
        abilityLengthTimer = maxAbilityLengthTimer;

        // movementDirection = GlobalVars.player.GetComponent<Player>().moveDir;

        new ModifierEntry("multiplier_PlayerSpeed", speed, maxAbilityLengthTimer);
        new ModifierEntry("multiplier_PlayerRegen", regen, maxAbilityLengthTimer);
        new ModifierEntry("multiplier_PlayerDamage", damage, maxAbilityLengthTimer);
        
        GlobalVars.player.GetComponent<Player>().iFrames = maxAbilityLengthTimer;

    }

    protected override bool AbilityUpdate()
    {
        if (abilityLengthTimer > 0)
        {
            abilityLengthTimer -= GlobalVars.DeltaTimePlayer;

            // player.transform.Translate(movementDirection * speed * Time.deltaTime);

            return false;
        }
        else
        {
            // movementDirection = Vector2.zero;
            return true;
        }
    }
}
