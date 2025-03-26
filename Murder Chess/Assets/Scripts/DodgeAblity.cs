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

        if(Input.GetKey(KeyCode.W)) 
        {
            movementDirection += Vector2.up;
        }
        if(Input.GetKey(KeyCode.A)) 
        {
            movementDirection += Vector2.left;
        }
        if(Input.GetKey(KeyCode.S)) 
        {
            movementDirection += Vector2.down;
        }
        if(Input.GetKey(KeyCode.D)) 
        {
            movementDirection += Vector2.right;
        }
        
        player.GetComponent<Player>().iFrames = 0.5f;

    }

    protected override bool AbilityUpdate()
    {
        if (abilityLengthTimer > 0)
        {
            abilityLengthTimer -= GlobalVars.DeltaTimePlayer;
            // GlobalVars.player.GetComponent<Player>().isDangerous = true;

            player.transform.Translate(movementDirection * speed * Time.deltaTime);

            return false;
        }
        else
        {
            // GlobalVars.player.GetComponent<Player>().isDangerous = false;
            movementDirection = Vector2.zero;
            return true;
        }
    }
}
