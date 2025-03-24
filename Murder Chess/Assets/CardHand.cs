using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    public Transform Deck;
    public int handSize = 5;
    //public List<BaseCard> handCards = new List<BaseCard>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (handCards.Count > handSize)
        //{
        //    handCards[handCards.Count - 1].transform.SetParent(Deck);
        //}

        int i = 0;
        foreach (Transform child in transform)
        {
            BaseCard card;
            if (child.TryGetComponent<BaseCard>(out card))
            {
                i++;
                card.inHand = true;
                if (i > handSize) card.transform.SetParent(Deck);
            }
        }
        foreach (Transform child in Deck)
        {
            BaseCard card;
            if (child.TryGetComponent<BaseCard>(out card))
            {
                card.inHand = false;
            }
        }
    }
}
