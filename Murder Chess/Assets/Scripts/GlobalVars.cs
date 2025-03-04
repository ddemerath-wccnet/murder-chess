using UnityEngine;

public static class GlobalVars
{
    // This Class contains a list of variables that can be accessed by every single class.
    // //It's a static public, so its on us to set and reset every var

    public static float timeScale_Player = 1;
    public static float timeScale_Piece = 1;

    /// <summary> DeltaTime for player </summary>
    /// <returns> <c>Time.deltaTime</c> multiplied by <c>timeScale_Player</c> </returns>
    public static float DeltaTimePlayer
    {   //Uses multipliers to correctly calculate var
        get { return Time.deltaTime * timeScale_Player; }
    }

    /// <summary> DeltaTime for pieces </summary>
    /// <returns> <c>Time.deltaTime</c> multiplied by <c>timeScale_Piece</c> </returns>
    public static float DeltaTimePiece
    {   //Uses multipliers to correctly calculate var
        get { return Time.deltaTime * timeScale_Piece; }
    }

    public static GameObject player;
    public static float multiplier_PlayerHealth = 1;
    public static float multiplier_PlayerSpeed = 1;
    public static float multiplier_PlayerDamage = 1;

    public static float multiplier_PieceHealth = 1;
    public static float multiplier_PieceSpeed = 1;
    public static float multiplier_PieceDamage = 1;
    public static float multiplier_PieceCycleTimer = 1;

    public static float multiplier_AbilityCooldown = 1; //More is better, x2 would be 'cooldown/2'

    /// <summary> Sets all variables back to default. </summary>
    public static void ResetClass()
    {
        player = GameObject.FindWithTag("Player");

        timeScale_Player = 1;
        timeScale_Piece = 1;

        multiplier_PlayerHealth = 1;
        multiplier_PlayerSpeed = 1;
        multiplier_PlayerDamage = 1;

        multiplier_PieceHealth = 1;
        multiplier_PieceSpeed = 1;
        multiplier_PieceDamage = 1;
        multiplier_PieceCycleTimer = 1;
    }
}
