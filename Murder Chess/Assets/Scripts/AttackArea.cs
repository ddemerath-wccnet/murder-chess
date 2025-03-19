using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public List<BasePiece> collisionList = new List<BasePiece>();

    public float damageMulti;

    public GameObject attackArea;

    private float damage = 0f;

    public SweepAttack attackScript;

    private void start() {

        attackScript = attackArea.GetComponent<SweepAttack>();

        damage = attackScript.damage;

        damageMulti = damageMulti * damage;

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
