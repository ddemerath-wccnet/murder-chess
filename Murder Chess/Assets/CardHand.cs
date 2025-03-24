using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    public Transform Deck;
    public int handSize = 5;
    public List<BaseCard> handCards = new List<BaseCard>();
    public string handComboName = "";
    public float handComboValue = 0;
    public ModifierEntry handComboModifier = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() /// TODO: STUPID CODE, MAKE THIS RUN ONCE, NOT EVERY FRAME!!!!!!
    {
        //if (handCards.Count > handSize)
        //{
        //    handCards[handCards.Count - 1].transform.SetParent(Deck);
        //}
        handCards = new List<BaseCard>();
        int i = 0;
        foreach (Transform child in transform)
        {
            BaseCard card;
            if (child.TryGetComponent<BaseCard>(out card))
            {
                i++;
                card.inHand = true;
                if (i > handSize) card.transform.SetParent(Deck);
                else handCards.Add(card);
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
        float tempHandComboValue = CalculateHandCombo(out handComboName);
        if (handComboValue != tempHandComboValue)
        {
            handComboValue = tempHandComboValue;
            if (handComboModifier != null) handComboModifier.DestroySelf();
            handComboModifier = new ModifierEntry("multiplier_CardHandMultiplier", handComboValue);
        }
    }

    public float CalculateHandCombo(out string myHandComboName)
    {
        float myHandComboValue = 0;
        myHandComboName = "";
        Dictionary<int, int> ranks = new Dictionary<int, int>();
        Dictionary<string, int> suits = new Dictionary<string, int>();
        bool straight = false;
        bool flush = false;

        foreach (BaseCard card in handCards)
        {
            if (!ranks.ContainsKey(card.rank)) ranks.Add(card.rank, 0);
            ranks[card.rank]++;

            if (!suits.ContainsKey(card.suit)) suits.Add(card.suit, 0);
            suits[card.suit]++;
        }

        int consecutiveCount = 0;
        int lastRank = -1; // Start with an invalid rank

        int twoKindCount = 0;
        int threeKindCount = 0;
        int fourKindCount = 0;

        foreach (var keyValuePair in ranks.OrderBy(k => k.Key))
        {
            if (keyValuePair.Key == lastRank + 1)
            {
                consecutiveCount++;
                if (consecutiveCount == 5)
                {
                    straight = true;
                }
            }
            else
            {
                consecutiveCount = 1; // Reset if gap found
            }

            lastRank = keyValuePair.Key;

            if (keyValuePair.Value == 2) twoKindCount++;
            if (keyValuePair.Value == 3) threeKindCount++;
            if (keyValuePair.Value == 4) fourKindCount++;
        }

        foreach (var keyValuePair in suits)
        {
            if (keyValuePair.Value == 5) flush = true;
        }

        // Royal Flush
        if (flush && ranks.ContainsKey(1) && ranks.ContainsKey(13) && ranks.ContainsKey(12) && ranks.ContainsKey(11) && ranks.ContainsKey(10))
        {
            myHandComboValue = 1.5f;
            myHandComboName = "Royal Flush";
        }
        else if (straight && flush)
        {
            myHandComboValue = 1f;
            myHandComboName = "Straight Flush";
        }
        else if (fourKindCount >= 1)
        {
            myHandComboValue = 0.7f;
            myHandComboName = "Four of a Kind";
        }
        else if (threeKindCount >= 1 && twoKindCount >= 1)
        {
            myHandComboValue = 0.5f;
            myHandComboName = "Full House";
        }
        else if (flush)
        {
            myHandComboValue = 0.4f;
            myHandComboName = "Flush";
        }
        else if (straight)
        {
            myHandComboValue = 0.3f;
            myHandComboName = "Straight";
        }
        else if (threeKindCount >= 1)
        {
            myHandComboValue = 0.2f;
            myHandComboName = "Three of a Kind";
        }
        else if (twoKindCount >= 2)
        {
            myHandComboValue = 0.15f;
            myHandComboName = "Two Pair";
        }
        else if (twoKindCount >= 1)
        {
            myHandComboValue = 0.1f;
            myHandComboName = "Pair";
        }

        return myHandComboValue;
    }
}
