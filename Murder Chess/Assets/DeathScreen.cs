using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TMP_Text>().text = "Wave Reached:\n" + GlobalVars.lastWave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
