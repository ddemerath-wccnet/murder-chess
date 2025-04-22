using UnityEngine;

public class xwall : MonoBehaviour
{
    //Lasts for 5 seconds
    private float maxLifespan = 5.0f;

    private float lifespan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     lifespan = maxLifespan;   
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }
}
