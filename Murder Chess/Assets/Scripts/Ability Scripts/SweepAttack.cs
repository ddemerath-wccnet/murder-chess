using UnityEngine;

public class SweepAttack : BaseAbility
{
    public float maxAbilityLengthTimer = 0.5f;
    public float abilityLengthTimer;
    public GameObject attackArea;
    protected override void AbilityStart()
    {
        abilityLengthTimer = maxAbilityLengthTimer;
        attackArea = transform.GetChild(0).gameObject;
    }

    protected override bool AbilityUpdate()
    {
        if (abilityLengthTimer > 0)
        {
            attackArea.SetActive(true);

            abilityLengthTimer -= GlobalVars.DeltaTimePlayer;
            GlobalVars.player.GetComponent<Player>().isDangerous = true;
            return false;
        }
        else
        {
            attackArea.SetActive(false);

            GlobalVars.player.GetComponent<Player>().isDangerous = false;
            return true;
        }
    }
}
