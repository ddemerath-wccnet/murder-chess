using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ModifierManager : MonoBehaviour
{
    public List<ModifierEntry> modifiers = new List<ModifierEntry>();
    public bool testAdd;
    public string varName;
    public float varModifier;
    public float tempTimer = -1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            foreach (ModifierEntry entry in modifiers)
            {
                entry.Update();
            }
        }
        catch { }

        if (testAdd)
        {
            testAdd = false;
            new ModifierEntry(varName, varModifier, tempTimer);
        }
    }

    public void AddEntry(ModifierEntry entry)
    {
        Debug.Log("Added Modifier: '" + entry.varName + "' at: " + entry.varModifier + " for: " + entry.tempTimer);
        modifiers.Add(entry);
        RecalculateModifiers();
    }

    public void RemoveEntry(ModifierEntry entry)
    {
        Debug.Log("Removed Modifier: '" + entry.varName + "' at: " + entry.varModifier);
        modifiers.Remove(entry);
        RecalculateModifiers();
    }

    public void RecalculateModifiers() //Dumdum stoopid code, try better next time Dante. signed -Dante
    {
        GlobalVars.ResetClass();
        try
        {
            foreach (ModifierEntry entry in modifiers)
            {
                GlobalVars.ModifyVariable(entry.varName, entry.varModifier);
            }
        }
        catch { }
    }
}
