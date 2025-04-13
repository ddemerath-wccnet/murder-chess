using UnityEngine;

public class CardTimeBuff : BaseCard
{
    public float multiplierPerSec = 1/30f;
    public float maxMultiplier = 1f;
    public float currentMultiplier;
    public int time;
    public int timeSinceHit;

    public override void Update()
    {
        base.Update();
        int tempTime = Mathf.FloorToInt(Time.time);
        if (time != tempTime)
        {
            time = tempTime;
            if (time <= GlobalVars.timeOfLastHit + 1)
            {
                timeSinceHit = 0;
                currentMultiplier = 0;
            }
            else
            {
                timeSinceHit++;
                currentMultiplier = Mathf.Clamp(timeSinceHit * multiplierPerSec, 0, maxMultiplier);
            }
            base.UpdateCard();
        }
    }

    public override void ActivateCard()
    {
        modifier = new ModifierEntry(varName, VarModifier * currentMultiplier);

    }

    public override void DeactivateCard()
    {
        modifier.DestroySelf();
        modifier = null;
    }
}
