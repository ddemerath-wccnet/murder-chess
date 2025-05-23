
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
    private Vector2 boxOffset = new Vector2(4f, 0);
    [SerializeField]
    private float aoeDamage = 10f;
    public AudioSource sound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void SpellStart()
    {


        Debug.Log("SpellStart called");
        Vector2 spellFront = (Vector2)transform.parent.position + spellOffset;
        transform.position = spellFront;

        Vector2 boxFront = (Vector2)transform.parent.position + boxOffset;





        // Transform cbsTransform = transform.Find("Celestial Beam Spell");
        Animator animation = GetComponent<Animator>();
        GameObject.Find("Celestial Beam Spell");


        if (animation != null)
        {
            Debug.Log("animator is is not null");
            animation.SetTrigger("PlayBeamSpell");
            Debug.Log("PlayBeamSpell trigger fired!");

        }



        Collider2D[] areaColliders = Physics2D.OverlapBoxAll(boxFront, new Vector2(spellLength, spellWidth), 0f);
        foreach (var areaCollider in areaColliders)
        {
            if (areaCollider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy in range");
                BasePiece basePiece = areaCollider.GetComponent<BasePiece>();
                basePiece.DamagePiece(basePiece.PieceDamage * aoeDamage);
            }
        }
        sound.Play();
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 spellFront = (Vector2)transform.parent.position + spellOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spellFront, new Vector3(spellLength, spellWidth, 0));
    }

    // Update is called once per frame
    protected override bool SpellUpdate()
    {
        return true;
    }
}
