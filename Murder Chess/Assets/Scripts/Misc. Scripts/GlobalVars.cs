using System.Reflection;
using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    public static ModifierManager modifierManager;
    public static float multiplier_PlayerHealth = 1;
    public static float multiplier_PlayerRegen = 1;
    public static float multiplier_PlayerSpeed = 1;
    public static float multiplier_PlayerDamage = 1;
    public static float multiplier_PlayerMana = 1;
    public static float multiplier_PlayerManaGain = 1;
    public static float multiplier_PlayerCoinGain = 1;

    public static float multiplier_CardHandMultiplier = 1;
    public static float multiplier_CardDeckMultiplier = 0.5f;

    public static float multiplier_PieceHealth = 1;
    public static float multiplier_PieceSpeed = 1;
    public static float multiplier_PieceDamage = 1;
    public static float multiplier_PieceCycleTimer = 1;
    public static float multiplier_PieceCoinValue = 1;

    public static float multiplier_AbilityCooldown = 1; //More is better, x2 would be 'cooldown/2'
    public static float multiplier_SpellCost = 1; //More is better, x2 would be 'cooldown/2'

    public static Vector3? decoyPosition = null;

    /// <summary> Sets all variables back to default. </summary>
    public static void ResetClass()
    {
        player = GameObject.FindWithTag("Player");
        modifierManager = GameObject.FindFirstObjectByType<ModifierManager>();

        timeScale_Player = 1;
        timeScale_Piece = 1;

        multiplier_PlayerHealth = 1;
        multiplier_PlayerRegen = 1;
        multiplier_PlayerSpeed = 1;
        multiplier_PlayerDamage = 1;
        multiplier_PlayerMana = 1;
        multiplier_PlayerManaGain = 1;
        multiplier_PlayerCoinGain = 1;

        multiplier_CardHandMultiplier = 1;
        multiplier_CardDeckMultiplier = 0.5f;

        multiplier_PieceHealth = 1;
        multiplier_PieceSpeed = 1;
        multiplier_PieceDamage = 1;
        multiplier_PieceCycleTimer = 1;
        multiplier_PieceCoinValue = 1;

        multiplier_AbilityCooldown = 1;
        multiplier_SpellCost = 1;
    }

    /// <summary> 
    /// Call to modify a variable of GlobalVars (multiplicatively). 
    /// </summary>
    /// <param name="variableName">The variable to modify.</param>
    /// <param name="amountToAdd">
    /// The amount to modify the variable by (multiplicatively). 
    /// +1 is +100%, and -1 is -100% (cuts value in half).
    /// </param>
    /// <returns>Returns true if successful, false if failure.</returns>
    public static bool ModifyVariable(string variableName, float amountToAdd)
    {
        Type type = typeof(GlobalVars);
        FieldInfo fieldInfo = type.GetField(variableName, BindingFlags.Static | BindingFlags.Public);

        if (fieldInfo != null && fieldInfo.FieldType == typeof(float))
        {
            float currentValue = (float)fieldInfo.GetValue(null);
            float newValue;
            if (amountToAdd >= 0) newValue = currentValue * (1 + amountToAdd);
            else newValue = currentValue / (1 - amountToAdd);

            fieldInfo.SetValue(null, newValue);
            return true;
        }

        return false;
    }

    public static Dictionary<GameObject, bool> GetObjectsInScene(string sceneName, bool freeze)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        Dictionary<GameObject, bool> objectsInScene = new Dictionary<GameObject, bool>();

        if (scene.IsValid() && scene.isLoaded)
        {
            GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach (GameObject obj in allObjects)
            {
                if (obj.scene == scene) // Check if the object belongs to the scene
                {
                    objectsInScene.Add(obj, obj.activeSelf);
                    if (freeze)
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Scene not found or not loaded: " + sceneName);
        }

        return objectsInScene;
    }

    public static void RestoreObjects(Dictionary<GameObject, bool> objects)
    {
        foreach (KeyValuePair<GameObject, bool> obj in objects)
        {
            try
            {
                if (obj.Value)
                {
                    obj.Key.SetActive(true);
                }
                else
                {
                    obj.Key.SetActive(false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public static Vector3 getTarget() {
        if(decoyPosition == null) {
            return GlobalVars.player.transform.position;
        }
        Debug.Log("Targeting Decoy" + decoyPosition.Value);
        return decoyPosition.Value;
    }
}
