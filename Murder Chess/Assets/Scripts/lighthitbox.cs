using UnityEngine;

public class lighthitbox : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("Player"))
        {
            //do stuff
        }
    }
}


/*
 
wahtever.SetActive(true);
wahtever.SetParent(GameObject.Find("PieceParent").Transform, true);
//When over
wahtever.SetActive(false);
*/