using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Shockwave : MonoBehaviour
{
    public float maxRadius = 8f;
    public float expansionSpeed = 100f;

    private Vector3 origin;
    private float currentScale = 0f;
    private GameObject playerObj;

    private void Start()
    {
        StartCoroutine(DelayIgnorePlayer());

        origin = transform.position;
        transform.localScale = Vector3.zero;
    }

    private IEnumerator DelayIgnorePlayer()
    {
        yield return null;
        playerObj = GlobalVars.player;

        Collider2D[] playerCols = playerObj.GetComponentsInChildren<Collider2D>();
        Collider2D shockwaveCol = GetComponent<Collider2D>();

        foreach (Collider2D pc in playerCols)
        {
            Physics2D.IgnoreCollision(shockwaveCol, pc, true);
        }
    }

    private void Update()
    {
        // Expand the shockwave visually
        currentScale += expansionSpeed * Time.deltaTime;
        transform.localScale = new Vector3(currentScale, currentScale, 1f);

        if (currentScale >= maxRadius)
        {
            Destroy(gameObject);
        }
    }
}
