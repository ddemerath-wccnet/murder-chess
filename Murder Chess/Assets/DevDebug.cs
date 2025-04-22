using UnityEngine;
using TMPro;

public class DevDebug : MonoBehaviour
{
    public bool devModeOnStart;
    public TMP_Text fpsCounter;
    bool devModeLast;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (devModeOnStart) GlobalVars.devMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
        {
            if (GlobalVars.devMode) GlobalVars.devMode = false;
            else GlobalVars.devMode = true;
        }

        if (devModeLast != GlobalVars.devMode)
        {
            devModeLast = GlobalVars.devMode;
            if (GlobalVars.devMode)
            {
                fpsCounter.gameObject.SetActive(true);
            }
            else
            {
                fpsCounter.gameObject.SetActive(false);
            }
        }

        if (GlobalVars.devMode)
        {
            fpsCounter.text = Mathf.RoundToInt(1f / Time.unscaledDeltaTime) + " FPS";
        }
    }
}
