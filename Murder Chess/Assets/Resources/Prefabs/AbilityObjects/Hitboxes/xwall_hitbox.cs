using UnityEngine;

public class xwall_hitbox : MonoBehaviour
{
    private float speed_multiplier = 0.75f;
    private float health_drain = 0.0005f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        //Check if obj is a player
        if (obj.CompareTag("Player"))
        {
            //make a player variable to hold the Player component
            Player player = obj.GetComponent<Player>();
            //slow the player's speed to a lower value
            player.PlayerSpeed *= speed_multiplier;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        //Check if obj is a player
        if (obj.CompareTag("Player"))
        {
            Player player = obj.GetComponent<Player>();
            //Player loses 0.5 hp / second in the collision zone.
            player.PlayerHealth -= health_drain;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        //Check if obj is a player
        if (obj.CompareTag("Player"))
        {
            //make a player variable to hold the Player component
            Player player = obj.GetComponent<Player>();
            //set the player's speed back to normal.
            player.PlayerSpeed /= speed_multiplier;
        }
    }
}
