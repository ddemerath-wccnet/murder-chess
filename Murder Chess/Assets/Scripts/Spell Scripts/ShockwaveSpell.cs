using UnityEngine;

public class ShockwaveSpell : BaseSpell
{
    public GameObject shockwavePrefab;
    public AudioSource sound;

    protected override void SpellStart()
    {
        Vector3 spawnOffset = new Vector3(0, -0.3f, 0); // 20 pixels = 0.2 units in Unity (assuming 100 pixels/unit)
        Instantiate(shockwavePrefab, GlobalVars.player.transform.position + spawnOffset, Quaternion.identity);
        sound.Play();
        //Instantiate(shockwavePrefab, GlobalVars.player.transform.position, Quaternion.identity);
    }

    protected override bool SpellUpdate()
    {
        return true;
    }
}

