using UnityEngine;
using UnityEngine.UI;

public class WaveTimerVisualizer : MonoBehaviour
{
    public WaveController waveController;
    public Slider slider;
    public Image fill;
    float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveController.controllerState == "Spawning")
        {
            timer += Time.deltaTime;
            fill.color = Color.green;
            slider.value = timer / waveController.waves[0].estimatedWaveTime;
        }
        else if (waveController.controllerState == "Cooldown")
        {
            timer = 0;
            fill.color = Color.cyan;
            slider.value = 1 - (waveController.InbetweenWaveCooldown / waveController.maxInbetweenWaveCooldown);
        }
    }
}
