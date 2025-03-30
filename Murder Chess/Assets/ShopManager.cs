using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public StatVisualizer MPVisualizer;
    public ImageCarousel cardCarousel;
    public ImageCarousel spellCarousel;
    public ImageCarousel abilityCarousel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MPVisualizer.myScript = GlobalVars.player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestockShop()
    {

    }

    public bool AquireItem(ShopItem item)
    {
        //if (item.price > GlobalVars.player.GetComponent<Player>().Coins) return false;
        GlobalVars.player.GetComponent<Player>().Coins -= item.price;

        BaseCard card;
        BaseSpell spell;
        BaseAbility ability;
        if (item.TryGetComponent<BaseCard>(out card))
        {

        }
        if (item.TryGetComponent<BaseSpell>(out spell))
        {

        }
        if (item.TryGetComponent<BaseAbility>(out ability))
        {

        }

        return true;
    }
}
