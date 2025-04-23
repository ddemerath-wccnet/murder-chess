using UnityEngine;
using UnityEngine.UI;

public class SlotSelector : MonoBehaviour
{
    public ImageCarousel imageCarousel;

    public bool isSpell, isAbility, isCard;

    public Button slot1;
    public Button slot2;
    public Button slot3;
    public Button slot4;
    public Button slot5;
    public Button slot6;
    public CardHand cardHand;
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardHand = GameObject.FindFirstObjectByType<CardHand>(FindObjectsInactive.Include);
        player = GameObject.FindFirstObjectByType<Player>(FindObjectsInactive.Include);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpell)
        {
            if (Input.GetKeyDown(KeyCode.Q)) imageCarousel.GetItem(1);
            if (Input.GetKeyDown(KeyCode.W)) imageCarousel.GetItem(2);
            if (Input.GetKeyDown(KeyCode.E)) imageCarousel.GetItem(3);
        }
        else if (isAbility)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) imageCarousel.GetItem(1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) imageCarousel.GetItem(2);
            if (Input.GetKeyDown(KeyCode.Alpha3)) imageCarousel.GetItem(3);
            if (Input.GetKeyDown(KeyCode.Alpha4)) imageCarousel.GetItem(4);
            if (Input.GetKeyDown(KeyCode.Alpha5)) imageCarousel.GetItem(5);
        }
        else if (isCard)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) imageCarousel.GetItem(1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) imageCarousel.GetItem(2);
            if (Input.GetKeyDown(KeyCode.Alpha3)) imageCarousel.GetItem(3);
            if (Input.GetKeyDown(KeyCode.Alpha4)) imageCarousel.GetItem(4);
            if (Input.GetKeyDown(KeyCode.Alpha5)) imageCarousel.GetItem(5);
            if (Input.GetKeyDown(KeyCode.Alpha6)) imageCarousel.GetItem(6);
        }
    }

    public void UpdateImages()
    {
        if (isSpell)
        {
            slot1.image.sprite = player.Spell1.GetComponent<ShopItem>().image;
            slot2.image.sprite = player.Spell2.GetComponent<ShopItem>().image;
            slot3.image.sprite = player.Spell3.GetComponent<ShopItem>().image;
        }
        else if (isAbility)
        {
            slot1.image.sprite = player.Ability1.GetComponent<ShopItem>().image;
            slot2.image.sprite = player.Ability2.GetComponent<ShopItem>().image;
            slot3.image.sprite = player.Ability3.GetComponent<ShopItem>().image;
            slot4.image.sprite = player.Ability4.GetComponent<ShopItem>().image;
            slot5.image.sprite = player.Ability5.GetComponent<ShopItem>().image;
        }
        else if (isCard)
        {
            slot1.image.sprite = cardHand.handCards[0].GetComponent<ShopItem>().image;
            slot2.image.sprite = cardHand.handCards[1].GetComponent<ShopItem>().image;
            slot3.image.sprite = cardHand.handCards[2].GetComponent<ShopItem>().image;
            slot4.image.sprite = cardHand.handCards[3].GetComponent<ShopItem>().image;
            slot5.image.sprite = cardHand.handCards[4].GetComponent<ShopItem>().image;
        }
    }

    public void UpdateImage(int slot, Sprite image)
    {
        if (slot == 1) slot1.image.sprite = image;
        if (slot == 2) slot2.image.sprite = image;
        if (slot == 3) slot3.image.sprite = image;
        if (slot == 4) slot4.image.sprite = image;
        if (slot == 5) slot5.image.sprite = image;
    }
}
