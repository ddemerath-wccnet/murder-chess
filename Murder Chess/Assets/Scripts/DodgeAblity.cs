using UnityEngine;

public class DodgeAbility : BaseAbility
{
    public float maxAbilityLengthTimer;
    public float abilityLengthTimer;
    public Vector2 movementDirection;
    public GameObject player;
    public float speed;
    protected override void AbilityStart()
    {
        abilityLengthTimer = maxAbilityLengthTimer;

        movementDirection = GlobalVars.player.GetComponent<Player>().moveDir;
        
        player.GetComponent<Player>().iFrames = 0.5f;

    }

    protected override bool AbilityUpdate()
    {
        if (abilityLengthTimer > 0)
        {
            abilityLengthTimer -= GlobalVars.DeltaTimePlayer;

            player.transform.Translate(movementDirection * speed * Time.deltaTime);

            return false;
        }
        else
        {
            movementDirection = Vector2.zero;
            return true;
        }
    }
}
