using UnityEngine;

public class DecoyAbility : BaseAbility
{
    [SerializeField] private float decoyLifetime = 3f;
    public GameObject decoyPrefab;
    private GameObject spawnedDecoy;
    private float decoyTimer;

    protected override void AbilityStart()
    {
        
        spawnedDecoy = Instantiate(decoyPrefab, transform.position, transform.rotation);
        decoyTimer = decoyLifetime;
    }

    protected override bool AbilityUpdate()
    {
        decoyTimer -= Time.deltaTime;
        
        if (decoyTimer <= 0)
        {
            Destroy(spawnedDecoy);
            return true;
        }
        return false;

    }
}
