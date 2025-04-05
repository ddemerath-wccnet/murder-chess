using UnityEngine;

public class DecoyAbility : BaseAbility
{
    [SerializeField] private float decoyLifetime = 30f;
    public GameObject decoyPrefab;
    private GameObject spawnedDecoy;
    private float decoyTimer;

    protected override void AbilityStart()
    {
        
        spawnedDecoy = Instantiate(decoyPrefab, GlobalVars.player.transform.position, GlobalVars.player.transform.rotation);
        GlobalVars.decoyPosition = spawnedDecoy.transform.position;
        decoyTimer = decoyLifetime;
    }

    protected override bool AbilityUpdate()
    {
        decoyTimer -= Time.deltaTime;
        
        if (decoyTimer <= 0)
        {
            GlobalVars.decoyPosition = null;
            Destroy(spawnedDecoy);
            return true;
        }
        return false;

    }
}
