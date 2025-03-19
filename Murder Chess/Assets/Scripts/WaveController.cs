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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controllerState = "Starting";
        InbetweenWaveCooldown = maxInbetweenWaveCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerState == "Starting")
        {
            controllerState = "Spawning";
            waves[waveInt].StartWave();
        }
        else if (controllerState == "Spawning")
        {
            if (waves[waveInt].IsDone())
            {
                controllerState = "Cooldown";
            }
        }
        else if (controllerState == "Cooldown")
        {
            InbetweenWaveCooldown -= Time.deltaTime;
            if (InbetweenWaveCooldown < 0)
            {
                waveInt++;
                controllerState = "Starting";
                InbetweenWaveCooldown = maxInbetweenWaveCooldown;

                if (waveInt >= waves.Count) controllerState = "Done";
            }
        }
    }
}
