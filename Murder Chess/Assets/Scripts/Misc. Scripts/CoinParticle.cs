using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class CoinParticle : MonoBehaviour
{
    public static Dictionary<int, List<Sprite>> coinParticleSprites = new Dictionary<int, List<Sprite>>();
    public static GameObject particlePrefab;
    public int particleValue;
    public float particleValueRemainder;

    public static void StartUp()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/coin_particles");
        foreach (Sprite sprite in sprites)
        {
            // Try to extract the first character of the sprite's name and convert it to an integer
            if (sprite.name.Length > 0 && int.TryParse(sprite.name.Substring(0, 1), out int spriteValue) && spriteValue != 0)
            {
                if (!coinParticleSprites.ContainsKey(spriteValue))
                {
                    coinParticleSprites.Add(spriteValue, new List<Sprite>());
                }
                coinParticleSprites[spriteValue].Add(sprite);
            }
            else
            {
                Debug.LogWarning($"Sprite '{sprite.name}' does not start with a valid integer and was skipped.");
            }
        }

        particlePrefab = (GameObject)Resources.Load("Prefabs/MicroPlasticParticle");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (coinParticleSprites.Count == 0)
        {
            StartUp();
        }

        if (particleValueRemainder >= 0.1)
        {
            Sprite mySprite = coinParticleSprites[1][Random.Range(0, coinParticleSprites[1].Count - 1)];
            GetComponent<SpriteRenderer>().sprite = mySprite;
        }
        else if (particleValue > 0)
        {
            Sprite mySprite = coinParticleSprites[particleValue][Random.Range(0, coinParticleSprites[particleValue].Count -1)];
            GetComponent<SpriteRenderer>().sprite = mySprite;
        }
    }

    public int testTotal;
    public bool test;

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            test = false;
            float remainder;
            List<int> results = particleAmounts(testTotal, out remainder);
            string debugLog = "";
            foreach (int i in results)
            {
                debugLog = debugLog + i + ", ";
            }
            Debug.Log(debugLog);
        }
    }

    public bool reset;

    private void OnValidate()
    {
        if (reset)
        {
            reset = false;
            StartUp();
        }
    }

    public static List<int> particleAmounts(float floatTotal, out float remainder)
    {
        int total = Mathf.FloorToInt(floatTotal);
        remainder = floatTotal - total;
        List<int> result = new List<int>();

        // Extract sorted particle values from dictionary
        List<int> values = coinParticleSprites.Keys.OrderByDescending(v => v).ToList();
        //List<int> values = new List<int> { 1, 4, 6, 9 }; // Example coin values

        // Track the number of times each value is used
        Dictionary<int, int> valueCounts = new Dictionary<int, int>();

        int i = 0;
        while (total > 0 && i <100)
        {
            i++;
            foreach (int value in values.OrderByDescending(v => v))
            {
                if (total >= value)
                {
                    // Check if adding this value would violate the % constraint
                    int currentCount = valueCounts.ContainsKey(value) ? valueCounts[value] : 0;
                    if (currentCount <= result.Count * 0.334)  // Ensure no value is used more than the % constraint
                    {
                        result.Add(value);
                        total -= value;
                        valueCounts[value] = currentCount + 1;
                        break;
                    }
                }
            }
        }

        return result;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.TryGetComponent<Player>(out player))
        {
            player.Coins = player.Coins + ((particleValue + particleValueRemainder) * GlobalVars.multiplier_PlayerCoinGain);
            GameObject.Destroy(gameObject);
        }
    }

    public static void SpawnParticles(float amount, Vector3 location, float randomDistanceRadius = 0.5f, Transform parent = null)
    {
        float remainder;
        List<int> results = particleAmounts(amount, out remainder);
        string debugLog = "Spawining Particles: ";
        foreach (int i in results)
        {
            debugLog = debugLog + i + ", ";
            Vector3 myLocation = location + new Vector3(Random.Range(0f, randomDistanceRadius), Random.Range(0f, randomDistanceRadius), 0);
            GameObject particle = GameObject.Instantiate(particlePrefab, myLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            particle.GetComponent<CoinParticle>().particleValue = i;
            if (parent != null) particle.transform.SetParent(parent, true);
        }
        if (remainder > 0)
        {
            debugLog = debugLog + remainder + ", ";
            Vector3 myLocation = location + new Vector3(Random.Range(0f, randomDistanceRadius), Random.Range(0f, randomDistanceRadius), 0);
            GameObject particle = GameObject.Instantiate(particlePrefab, myLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            particle.GetComponent<CoinParticle>().particleValueRemainder = remainder;
            if (parent != null) particle.transform.SetParent(parent, true);
        }
        Debug.Log(debugLog);
    }
}
