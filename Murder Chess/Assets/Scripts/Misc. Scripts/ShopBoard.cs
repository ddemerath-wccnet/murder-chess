using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class ShopBoard : MonoBehaviour
{
    Dictionary<GameObject, bool> boardScene = new Dictionary<GameObject, bool>();
    float timeSinceLastSwitch;
    float timeScale;

    void Start()
    {

    }

    void Update()
    {
        timeSinceLastSwitch += Time.unscaledDeltaTime;
        if (Input.GetKeyDown(KeyCode.Escape) && timeSinceLastSwitch > 1)
        {
            ThawBoardScene();
        }
    }

    public void FreezeBoardScene()
    {
        timeScale = Time.timeScale;
        Time.timeScale = 0;
        timeSinceLastSwitch = 0;
        boardScene.Clear();
        boardScene = GlobalVars.GetObjectsInScene("Board", true);
    }

    public void ThawBoardScene()
    {
        Time.timeScale = timeScale;
        timeSinceLastSwitch = 0;
        GlobalVars.RestoreObjects(boardScene);
        FindFirstObjectByType<BoardShop>().FreezeShopScene();
    }
}
