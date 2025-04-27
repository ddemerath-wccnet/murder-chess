using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public List<BasePiece> collisionList = new List<BasePiece>();
    public float damageMulti;
    public bool selfDamage;
    public bool burns = false;
    public float burnDuration = 0f;
    public bool bleeds = false;
    public float bleedDuration = 0f;
    public bool allowRepeats = false;
    private void start() {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.LogWarning("Hit");
        BasePiece basePiece;
        Player player;
        if (collision.TryGetComponent<BasePiece>(out basePiece))
        {
            if (allowRepeats || !collisionList.Contains(basePiece))
            {
                collisionList.Add(basePiece);
                GlobalVars.player.GetComponent<Player>().HitPiece(basePiece, 0, damageMulti, 1f);
                if(burns) {

                    new BurnEffect(basePiece, burnDuration);

                }
                if(bleeds) {

                    //bleed enemy

                }
            }
        }


        if (selfDamage && collision.TryGetComponent<Player>(out player)) 
        {

            Debug.LogWarning("Player Hit");

            // GlobalVars.player.GetComponent<Player>().DamagePlayer(damageMulti);
            if(burns) {

                    new BurnEffect(GlobalVars.player.GetComponent<Player>(), burnDuration);

                }
                if(bleeds) {

                    //bleed enemy

            }

            GlobalVars.player.GetComponent<Player>().DamagePlayer(damageMulti);

        }
        // if(selfDamage && collision.gameObject.CompareTage(Player)) 
        // {
        //     GlobalVars.player.GetComponent<Player>().DamagePlayer(damageMulti);
        //     if(burns) {

        //             new BurnEffect(GlobalVars.player.GetComponent<Player>(), burnDuration);

        //         }
        //         if(bleeds) {

        //             //bleed enemy

        //     }
        // }
    }

}
