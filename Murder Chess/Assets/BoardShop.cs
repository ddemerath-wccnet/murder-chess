using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardShop : MonoBehaviour
{
    Dictionary<GameObject, bool> shopScene = new Dictionary<GameObject, bool>();
    float timeSinceLastSwitch;

    void Update()
    {
        timeSinceLastSwitch += Time.unscaledDeltaTime;
    }

    private void Awake()
    {
        SceneManager.LoadScene("BuyItems", LoadSceneMode.Additive);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FreezeShopScene();
    }

    public void FreezeShopScene()
    {
        timeSinceLastSwitch = 0;
        shopScene.Clear();
        shopScene = GlobalVars.GetObjectsInScene("BuyItems", true);
    }

    public void ThawShopScene()
    {
        timeSinceLastSwitch = 0;
        GlobalVars.RestoreObjects(shopScene);
        FindFirstObjectByType<ShopBoard>().FreezeBoardScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.TryGetComponent<Player>(out player) && timeSinceLastSwitch > 1)
        {
            ThawShopScene();
        }
    }
}
