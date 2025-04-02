using UnityEngine;

public class BombAbility : BaseAbility
{
    public float maxAbilityLengthTimer;
    public float abilityLengthTimer;
    public float explosionDuration;
    public GameObject attackArea;
    public GameObject bomb;
    private float bombTimer;
    private float instanceBombTimer;
    private Vector3 playerPosition;
    protected override void AbilityStart()
    {
        abilityLengthTimer = maxAbilityLengthTimer;
        bombTimer = maxAbilityLengthTimer - explosionDuration;
        instanceBombTimer = bombTimer;
        playerPosition = GlobalVars.player.transform.position;
        bomb.transform.position = playerPosition;
        attackArea.transform.position = playerPosition;
    }

    protected override bool AbilityUpdate()
    { 
        bomb.transform.position = playerPosition;
        attackArea.transform.position = playerPosition;

        if (abilityLengthTimer > 0)
        {
            if(instanceBombTimer > 0)
            {
                instanceBombTimer -= GlobalVars.DeltaTimePlayer;
                bomb.SetActive(true);
            }
            else 
            {
                bomb.SetActive(false);
                attackArea.SetActive(true);
            }

            abilityLengthTimer -= GlobalVars.DeltaTimePlayer;
            return false;
        }
        else
        {
            attackArea.SetActive(false);
            return true;
        }
    }
}
