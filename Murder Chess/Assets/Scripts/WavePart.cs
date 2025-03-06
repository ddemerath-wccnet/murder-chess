using System.Collections.Generic;
using UnityEngine;

public class WavePart : MonoBehaviour
{
    public List<BasePiece> pieces = new List<BasePiece>();
    public int pieceInt = 0;
    public string controllerState = null;
    public float maxInbetweenPieceCooldown = 0.5f;
    public float InbetweenPieceCooldown;
    public Vector3 spawnLocation;
    public bool instantDone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void StartPart()
    {
        controllerState = "Spawning";
        if (pieces.Count == 0) controllerState = "Cooldown";
        InbetweenPieceCooldown = maxInbetweenPieceCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerState == "Spawning")
        {
            if (pieceInt < pieces.Count && pieces.Count != 0) GameObject.Instantiate(pieces[pieceInt], spawnLocation, new Quaternion());
            controllerState = "Cooldown";
        }
        else if (controllerState == "Cooldown")
        {
            InbetweenPieceCooldown -= Time.deltaTime;
            if (InbetweenPieceCooldown < 0)
            {
                pieceInt++;
                controllerState = "Spawning";
                InbetweenPieceCooldown = maxInbetweenPieceCooldown;
                if (pieceInt == pieces.Count) pieceInt++;
            }
        }
    }
    public bool IsDone()
    {
        if (instantDone || pieceInt == pieces.Count + 1) return true;
        else return false;
    }
}
