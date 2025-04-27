using UnityEngine;

public class RookSpell : BaseSpell
{
    public GameObject rookPrefab;
    public GameObject rook;
    public SpellObject rookSpellObject;
    public float travelDistance = 5;
    public float maxTravelTimer = 2;
    float travelTimer;
    public float spellDamageMulti = 1;
    public AudioSource sound;
    Vector3 origPos;
    Vector3 targetPos;
    public Vector3 movedir = Vector3.right;

    protected override void SpellStart()
    {
        Debug.Log("spell start");
        rook = GameObject.Instantiate(rookPrefab, transform.position, transform.rotation);
        rookSpellObject = rook.GetComponent<SpellObject>();
        rookSpellObject.damageMulti = spellDamageMulti;
        travelTimer = maxTravelTimer;
        origPos = transform.position;
        if (movedir.magnitude == 0) movedir = Vector3.right;
        targetPos = transform.position +
            movedir * travelDistance;

        sound.Play();
    }

    protected override bool SpellUpdate()
    {
        travelTimer -= GlobalVars.DeltaTimePiece;
        float lerpPos = 1 - (travelTimer / maxTravelTimer);
        rook.transform.position = Vector3.Lerp(origPos, targetPos, lerpPos);

        if (travelTimer < 0)
        {
            GameObject.Destroy(rook);
            return true;
        }
        else return false;
    }

    protected override void Update()
    {
        if (GlobalVars.player.GetComponent<Player>().moveDir.magnitude != 0) movedir = GlobalVars.player.GetComponent<Player>().moveDir;
        base.Update();
    }
}
