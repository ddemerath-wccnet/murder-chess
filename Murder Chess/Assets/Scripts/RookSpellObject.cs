using System.Collections.Generic;
using UnityEngine;

public class RookSpellObject : MonoBehaviour
{
    public float damageMulti;
    public List<BasePiece> collisionList = new List<BasePiece>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BasePiece basePiece;
        if (collision.TryGetComponent<BasePiece>(out basePiece))
        {
            if (!collisionList.Contains(basePiece))
            {
                collisionList.Add(basePiece);
                GlobalVars.player.GetComponent<Player>().HitPiece(basePiece, 0, damageMulti, 0.5f);
            }
        }
    }
}
