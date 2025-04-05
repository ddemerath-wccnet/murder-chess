
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CelestialBeamSpell : BaseSpell
{
    [SerializeField]
    private float spellWidth;
    [SerializeField]
    private float spellLength;
    [SerializeField]
    private Vector2 spellOffset = new Vector2(4f, 0);
    [SerializeField]
    private float aoeDamage = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void SpellStart()
    {


        Debug.Log("SpellStart called");
        Vector2 spellFront = (Vector2)transform.parent.position + spellOffset;
        transform.position = spellFront;





        // Transform cbsTransform = transform.Find("Celestial Beam Spell");
        Animator animation = GetComponent<Animator>();
        GameObject.Find("Celestial Beam Spell");


        if (animation != null)
        {
            Debug.Log("animator is is not null");
            animation.SetTrigger("PlayBeamSpell");
            Debug.Log("PlayBeamSpell trigger fired!");

        }



        Collider2D[] areaColliders = Physics2D.OverlapBoxAll(spellFront, new Vector2(spellLength, spellWidth), 0f);
        foreach (var areaCollider in areaColliders)
        {
            if (areaCollider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy in range");
                BasePiece basePiece = areaCollider.GetComponent<BasePiece>();
                basePiece.DamagePiece(basePiece.PieceDamage * aoeDamage);
            }
        }
    }

    // Update is called once per frame
    protected override bool SpellUpdate()
    {
        return true;
    }
}
