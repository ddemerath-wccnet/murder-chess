using System.Collections.Generic;
using UnityEngine;

public class WaveInstance : MonoBehaviour
{
    public List<WavePart> parts = new List<WavePart>();
    public int partInt = 0;
    public string controllerState = null;
    public float maxInbetweenPartCooldown = 0;
    public float InbetweenPartCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void StartWave()
    {
        controllerState = "Starting";
        InbetweenPartCooldown = maxInbetweenPartCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerState == "Starting")
        {
            controllerState = "Spawning";
            parts[partInt].StartPart();
        }
        else if (controllerState == "Spawning")
        {
            if (parts[partInt].IsDone())
            {
                controllerState = "Cooldown";
            }
        }
        else if (controllerState == "Cooldown")
        {
            InbetweenPartCooldown -= Time.deltaTime;
            if (InbetweenPartCooldown < 0)
            {
                partInt++;
                controllerState = "Starting";
                InbetweenPartCooldown = maxInbetweenPartCooldown;
            }
        }
    }
    public bool IsDone()
    {
        if (partInt == parts.Count) return true;
        else return false;
    }
}
