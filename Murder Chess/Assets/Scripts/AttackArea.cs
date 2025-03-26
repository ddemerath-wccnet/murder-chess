using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public List<BasePiece> collisionList = new List<BasePiece>();
    public float damageMulti;
    private void start() {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BasePiece basePiece;
        if (collision.TryGetComponent<BasePiece>(out basePiece))
        {
            if (!collisionList.Contains(basePiece))
            {
                collisionList.Add(basePiece);
                GlobalVars.player.GetComponent<Player>().HitPiece(basePiece, 0, damageMulti, 1f);
            }
        }
    }

}
