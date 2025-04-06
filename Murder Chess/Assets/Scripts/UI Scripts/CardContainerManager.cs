using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContainerManager : MonoBehaviour
{
    public List<Image> cardDisplays = new List<Image>();
    public List<BaseCard> handCards = new List<BaseCard>();
    public CardHand cardHand;
    public float scale = 100;
    public int deckSize;
    public GameObject deckCard;
    public GameObject deck;
    public Image deckTop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handCards = cardHand.handCards;

        int handCount = handCards.Count;
        float cardOffset = -((handCount / 2f) - 0.5f);
        for (int i = 0; i < 5; i++)
        {
            cardDisplays[i].gameObject.SetActive(false);
            if (i < handCount)
            {
                cardDisplays[i].gameObject.SetActive(true);
            }

            cardDisplays[i].sprite = handCards[i].GetComponent<ShopItem>().image;
            cardDisplays[i].transform.localPosition = transform.localPosition + (new Vector3(cardOffset, -0.1f * (Mathf.Pow(cardOffset, 2))) * scale);
            cardDisplays[i].transform.localRotation = Quaternion.Euler(0, 0, (cardOffset) * -5);
            cardOffset += 1;
        }

        if (deckSize < cardHand.Deck.childCount)
        {
            deck.SetActive(true);
            //deckTop.sprite = cardHand.Deck.GetChild(deckSize).GetComponent<ShopItem>().image;
            deckSize++;

            if (deckSize < 30)
            {
                GameObject card = GameObject.Instantiate(deckCard, deck.transform);
                card.transform.localPosition = new Vector3(4 * deckSize, -6 * deckSize, 0);
                card.GetComponent<Image>().color = new Color(1 - (0.04f * deckSize), 1 - (0.04f * deckSize), 1 - (0.04f * deckSize));
                card.transform.SetSiblingIndex(0);
            }
        }
    }
}
