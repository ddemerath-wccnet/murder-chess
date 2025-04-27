using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    /*
     Waves hold Wave Instances, which hold wave parts, the wave parts hold pieces to spawn.
    the wave instances try to spawn each wave part one after another, and the wave parts can hold 0 pieces (which just makes them a cooldown)
    waves can also have instantDone = true which means go immediately to the next part even while we're spawning
     so you could have a wave instance hold a wave part with instantdone, and another exact same one, and they would spawn at the same time
    then follow up with a 0 piece wave part, which would then wait for maxInbetweenPieceCooldown secconds before moving on
     
    ask me questions if you have trouble figuring it out!
     */

    public List<WaveInstance> waves = new List<WaveInstance>();
    public int waveInt = 0;
    public string controllerState = null;
    public float maxInbetweenWaveCooldown = 3;
    public float InbetweenWaveCooldown;
    public Transform PieceParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controllerState = "Starting";
        InbetweenWaveCooldown = maxInbetweenWaveCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //UPDATE: Change this if/else block to a switch statement.
        if (controllerState == "Starting")
        {
            controllerState = "Spawning";
            waves[0].StartWave();
            waveInt++;
            GlobalVars.lastWave = waveInt;
            new ModifierEntry("multiplier_PlayerCoinGain", -0.025f);
        }
        else if (controllerState == "Spawning")
        {
            if (waves[0].IsDone())
            {
                controllerState = "Cooldown";
            }
        }
        else if (controllerState == "Cooldown")
        {
            InbetweenWaveCooldown -= Time.deltaTime;
            if (InbetweenWaveCooldown > 6 && PieceParent.childCount == 0) InbetweenWaveCooldown = 5;
            if (InbetweenWaveCooldown < 0)
            {
                GameObject waveGameObject = waves[0].gameObject;
                waves.RemoveAt(0);
                GameObject.Destroy(waveGameObject);
                controllerState = "Starting";
                InbetweenWaveCooldown = maxInbetweenWaveCooldown;

                if (waves.Count == 0) controllerState = "Done";
            }
        }
    }
}
