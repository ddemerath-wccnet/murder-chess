using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    public string loadScene;
    public void OnButtonClick()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene(loadScene);
    }
}
