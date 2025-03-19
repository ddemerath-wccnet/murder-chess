using UnityEngine;
using TMPro;

public class ManaVisualizer : MonoBehaviour
{
    public Player player;
    public TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.PlayerMana + "/" + player.MaxPlayerMana;
    }
}
