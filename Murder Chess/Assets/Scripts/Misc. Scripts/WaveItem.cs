using Unity.Mathematics;
using UnityEngine;

public class WaveItem : MonoBehaviour
{
    [Header("Rendering")]
    public Sprite image;
    public float imageScale = 1;

    [Header("Spawning")]
    public float difficultyCost = 10;
    public int2 groupSize_MinToMax;
    public int rarity = 1;

    [Header("Game")]
    [SerializeField]
    private float base_PieceCoinValue = 25;
    public float PieceCoinValue
    {   //Uses multipliers to correctly calculate var
        get { return base_PieceCoinValue * GlobalVars.multiplier_PieceCoinValue; }
        set { base_PieceCoinValue = value / GlobalVars.multiplier_PieceCoinValue; }
    }

    private void Start()
    {
        image = GetComponent<SpriteRenderer>().sprite;
    }
}
