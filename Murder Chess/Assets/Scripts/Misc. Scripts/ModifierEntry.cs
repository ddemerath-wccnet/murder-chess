using UnityEngine;

public class ModifierEntry
{
    public string varName;
    public float varModifier;
    public float tempTimer = -1f;

    public ModifierEntry(string varName, float varModifier, float tempTimer = -1)
    {
        this.varName = varName;
        this.varModifier = varModifier;
        this.tempTimer = tempTimer;
        GlobalVars.modifierManager.AddEntry(this);
    }

    public void Update() 
    {
        if (tempTimer > 0) 
        {
            tempTimer -= GlobalVars.DeltaTimePlayer;
        }
        else if (tempTimer != -1f)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        GlobalVars.modifierManager.RemoveEntry(this);
    }
}
