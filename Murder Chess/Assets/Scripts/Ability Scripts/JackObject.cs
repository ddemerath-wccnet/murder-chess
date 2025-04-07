using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JackObject : MonoBehaviour
{
    public float uses = 1;
    public float duration = 15;
    public int level = 1;
    public float lifeTime = 30;
    private void Update() {
        lifeTime -= GlobalVars.DeltaTimePlayer;
        if (lifeTime < 0 ) GameObject.Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BasePiece basePiece;
        if (collision.transform.TryGetComponent<BasePiece>(out basePiece))
        {
            Physics2D.IgnoreCollision(basePiece.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), basePiece.GetComponent<Collider2D>());
            if (basePiece.activeEffects.Any(effect => effect.GetType() == typeof(IcyEffect) && effect.level >= level))
            {
            }
            else
            {
                new IcyEffect(basePiece, duration, level);
                uses--;
                if (uses <= 0) GameObject.Destroy(gameObject);
            }
        }
    }
}
