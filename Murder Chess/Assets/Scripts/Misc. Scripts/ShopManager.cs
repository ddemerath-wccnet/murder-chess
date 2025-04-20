using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    public int restockCost = 1;
    public float luckCount;
    public StatVisualizer MPVisualizer;
    public ImageCarousel cardCarousel;
    public ImageCarousel spellCarousel;
    public ImageCarousel abilityCarousel;
    public Dictionary<int, List<ShopItem>> cardItems = new Dictionary<int, List<ShopItem>>();
    public Dictionary<int, List<ShopItem>> spellItems = new Dictionary<int, List<ShopItem>>();
    public Dictionary<int, List<ShopItem>> abilityItems = new Dictionary<int, List<ShopItem>>();
    public Transform cardParent;
    public Transform spellParent;
    public Transform abilityParent;
    public int maxRarity = 0;
    Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShopItem[] cardItemsTemp = Resources.LoadAll<ShopItem>("Prefabs/Shop/Cards");
        ShopItem[] spellItemsTemp = Resources.LoadAll<ShopItem>("Prefabs/Shop/Spells");
        ShopItem[] abilityItemsTemp = Resources.LoadAll<ShopItem>("Prefabs/Shop/Abilites");

        foreach (ShopItem item in cardItemsTemp)
        {
            if (!cardItems.ContainsKey(item.rarity)) cardItems.Add(item.rarity, new List<ShopItem>());
            cardItems[item.rarity].Add(item);
            Debug.Log(item.rarity + ": " + item.gameObject.name);
            if (item.rarity > maxRarity) maxRarity = item.rarity;
        }

        foreach (ShopItem item in spellItemsTemp)
        {
            if (!spellItems.ContainsKey(item.rarity)) spellItems.Add(item.rarity, new List<ShopItem>());
            spellItems[item.rarity].Add(item);
            Debug.Log(item.rarity + ": " + item.gameObject.name);
            if (item.rarity > maxRarity) maxRarity = item.rarity;
        }

        foreach (ShopItem item in abilityItemsTemp)
        {
            if (!abilityItems.ContainsKey(item.rarity)) abilityItems.Add(item.rarity, new List<ShopItem>());
            abilityItems[item.rarity].Add(item);
            Debug.Log(item.rarity + ": " + item.gameObject.name);
            if (item.rarity > maxRarity) maxRarity = item.rarity;
        }

        MPVisualizer.myScript = GlobalVars.player.GetComponent<Player>();
        player = GlobalVars.player.GetComponent<Player>();
        cardParent = player.transform.Find("Cards").Find("CardHand");
        spellParent = player.transform.Find("Spells");
        abilityParent = player.transform.Find("Abilites");

        DefaultRestock();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(RollValue(luckCount));
    }

    public void DefaultRestock()
    {
        //if (restockCost > player.Coins) return;
        player.Coins = player.Coins - restockCost;
        restockCost *= 2;
        Debug.Log("DefaultRestock");
        RestockShop(RollValue(luckCount));
    }

    public void RestockShop(float rollValue, int minItems = 3, int maxItems = 5)
    {
        RestockCarousel(rollValue, cardItems, cardCarousel, "Cards", minItems, maxItems);
        RestockCarousel(rollValue, spellItems, spellCarousel, "Spells", minItems, maxItems);
        RestockCarousel(rollValue, abilityItems, abilityCarousel, "Ability", minItems, maxItems);
    }

    public float rarityScaler = 2;
    public void RestockCarousel(float rollValue, Dictionary<int, List<ShopItem>> myItemDictionary, ImageCarousel myCarousel, string debugName = "", int minItems = 3, int maxItems = 5)
    {
        int itemsToSpawn;
        itemsToSpawn = Random.Range(minItems, maxItems + 1);
        myCarousel.items.Clear();
        for (int i = 0; i < itemsToSpawn; i++)
        {
            ShopItem toAdd = null;

            int rarityToSpawn = RarityToSpawn(rollValue, rarityScaler, maxRarity);

            for (int fallbackRarity = rarityToSpawn; fallbackRarity >= 0 ; fallbackRarity--)
            {
                if (myItemDictionary.ContainsKey(fallbackRarity) && myItemDictionary[fallbackRarity].Count > 0)
                {
                    toAdd = myItemDictionary[fallbackRarity][Random.Range(0, myItemDictionary[fallbackRarity].Count)];
                    break;
                }
            }

            if (toAdd != null) myCarousel.items.Add(toAdd);
            else Debug.LogWarning("No '" + debugName + "' Shop Item Found below rarity: " + rarityToSpawn);
        }
        myCarousel.UpdateImage();
    }

    public bool AquireItem(ShopItem item)
    {
        luckCount++;

        if (item.price > GlobalVars.player.GetComponent<Player>().Coins && !GlobalVars.devMode) return false;
        GlobalVars.player.GetComponent<Player>().Coins -= item.price;

        BaseCard card;
        BaseSpell spell;
        BaseAbility ability;
        if (item.TryGetComponent<BaseCard>(out card))
        {
            GameObject myItem = GameObject.Instantiate(card, cardParent).gameObject;
            myItem.transform.SetSiblingIndex(0);
            myItem.GetComponent<BaseCard>().active = true;
        }
        if (item.TryGetComponent<BaseSpell>(out spell))
        {
            GameObject myItem = GameObject.Instantiate(spell, spellParent).gameObject;
            myItem.transform.SetSiblingIndex(0);
        }
        if (item.TryGetComponent<BaseAbility>(out ability))
        {
            GameObject myItem = GameObject.Instantiate(ability, abilityParent).gameObject;
            myItem.transform.SetSiblingIndex(0);
        }

        return true;
    }

    float k = 0.05f;
    float a = 10f;
    float b = 1f;
    float RollValue(float luck)
    {
        float output = 6;

        output = 1 / (
            (1 / a) +
            (((1 / b) - (1 / a)) *
            (1-(Mathf.Pow((float)System.Math.E, -(k*luck))))
            ));

        return output;
    }

    int RarityToSpawn(float rollValue, float rarityScaler, int maxRarity)
    {
        int rarityToSpawn = 1;
        for (rarityToSpawn = 1; rarityToSpawn <= Mathf.Pow(rarityScaler, maxRarity); rarityToSpawn++)
        {
            float random = Random.Range(0, rollValue);
            //Debug.Log(debugName + ": " + random + "/" + rollValue);
            if (random > 1) break;
        }
        rarityToSpawn = Mathf.FloorToInt(Mathf.Clamp(Mathf.Log(rarityToSpawn, rarityScaler), 1.1f, int.MaxValue));
        return rarityToSpawn;
    }
}
