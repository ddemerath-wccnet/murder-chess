using Unity.Mathematics;
using UnityEngine;

public class JacksAbility : BaseAbility
{
    public GameObject jackPrefab;
    public int2 amountToSpawn_MinToMax;
    float spread = 5;

    protected override void AbilityStart()
    {
        int amountToSpawn = UnityEngine.Random.Range(amountToSpawn_MinToMax.x, amountToSpawn_MinToMax.y + 1);
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject jack = GameObject.Instantiate(jackPrefab, transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(-180f, 180f)));
            Rigidbody2D jackBody = jack.GetComponent<Rigidbody2D>();
            jackBody.angularVelocity = UnityEngine.Random.Range(-spread, spread);
            jackBody.linearVelocityX = UnityEngine.Random.Range(-spread, spread);
            jackBody.linearVelocityY = UnityEngine.Random.Range(-spread, spread);
        }
    }

    protected override bool AbilityUpdate()
    {
        return true;
    }
}
