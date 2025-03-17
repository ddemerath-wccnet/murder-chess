using UnityEngine;

public class RookSpell : BaseSpell
{
    public GameObject rookPrefab;
    public GameObject rook;
    public RookSpellObject rookSpellObject;
    public float travelDistance = 5;
    public float maxTravelTimer = 2;
    float travelTimer;
    public float spellDamageMulti = 1;
    Vector3 origPos;
    Vector3 targetPos;

    protected override void SpellStart()
    {
        Debug.Log("spell start");
        rook = GameObject.Instantiate(rookPrefab, transform.position, transform. rotation);
        rookSpellObject = rook.GetComponent<RookSpellObject>();
        rookSpellObject.damageMulti = spellDamageMulti;

        travelTimer = maxTravelTimer;
        origPos = transform.position;
        targetPos = transform.position + 
            (Camera.main.ScreenToWorldPoint(Input.mousePosition) - origPos).normalized * travelDistance;
    }

    protected override bool SpellUpdate()
    {
        travelTimer -= GlobalVars.DeltaTimePiece;
        float lerpPos = 1-(travelTimer / maxTravelTimer);
        rook.transform.position = Vector3.Lerp(origPos, targetPos, lerpPos);

        if (travelTimer < 0)
        {
            GameObject.Destroy(rook);
            return true;
        }
        else return false;
    }
}
