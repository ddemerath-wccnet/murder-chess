using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WaveGenerator : MonoBehaviour
{
    public int waveInt = 0;
    public float difficulty;
    public WaveController waveController;
    public Dictionary<int, List<WaveItem>> pieceList = new Dictionary<int, List<WaveItem>>();
    public int maxRarity;
    public GameObject Board;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WaveItem[] waveItems = Resources.LoadAll<WaveItem>("Prefabs/Pieces");
        foreach (WaveItem item in waveItems)
        {
            if (!pieceList.ContainsKey(item.rarity)) pieceList.Add(item.rarity, new List<WaveItem>());
            pieceList[item.rarity].Add(item);
            Debug.Log(item.rarity + ": " + item.gameObject.name);
            if (item.rarity > maxRarity) maxRarity = item.rarity;
        }
    }

    public WaveInstance current_WaveInstance;
    public WavePart current_WavePart;
    // Update is called once per frame
    void Update()
    {
        if (waveController.waves.Count <= 5 && waveInt - 5 < waveController.waveInt)
        {
            waveInt++;
            GameObject aWave = new GameObject("Wave " + waveInt);
            aWave.transform.SetParent(transform, false);
            waveController.waves.Add(GenerateWaveInstance(aWave, difficulty));
            difficulty += 1;
        }
    }
    public float estimatedInstanceTime;
    public float difficultyBudget;
    public WaveInstance GenerateWaveInstance(GameObject aWave, float difficulty)
    {
        estimatedInstanceTime = 0;
        current_WaveInstance = aWave.AddComponent<WaveInstance>();

        difficultyBudget = DifficultyBudget(difficulty);
        float difficultyBudgetMax = difficultyBudget;
        int amountOfParts = PickAmountOfParts(difficulty);
        for (int i = 0; i < amountOfParts; i++)
        {
            float myDifficultyBudget;
            if (i < amountOfParts - 1) myDifficultyBudget = difficultyBudgetMax / (amountOfParts * Random.Range(0.75f, 1.25f));
            else myDifficultyBudget = difficultyBudget;
            difficultyBudget -= myDifficultyBudget;

            if (Random.Range(0f, 2f) < 1) //Two Part Wave
            {
                Vector3 spawnLocation1 = PickSpawnLocation();
                Vector3 spawnLocation2 = new Vector3();

                if (Random.Range(0f, 3f) < 1) //flip horizontally
                {
                    spawnLocation2 = spawnLocation1 - Board.transform.position;
                    spawnLocation2.x *= -1;
                    spawnLocation2 += Board.transform.position;
                }
                else if (Random.Range(0f, 2f) < 1) //flip vertically
                {
                    spawnLocation2 = spawnLocation1 - Board.transform.position;
                    spawnLocation2.y *= -1;
                    spawnLocation2 += Board.transform.position;
                }
                else //flip both
                {
                    spawnLocation2 = spawnLocation1 - Board.transform.position;
                    spawnLocation2.y *= -1;
                    spawnLocation2 += Board.transform.position;
                }

                float remainder;
                WavePart wavePart1 = GenerateWavePart(aWave, difficulty, myDifficultyBudget / 2, spawnLocation1, out remainder);
                current_WaveInstance.parts.Add(wavePart1);
                myDifficultyBudget = (myDifficultyBudget / 2) + remainder;
                float estimatedPartTime1 = estimatedPartTime;

                WavePart wavePart2 = GenerateWavePart(aWave, difficulty, myDifficultyBudget, spawnLocation2, out remainder);
                current_WaveInstance.parts.Add(wavePart2);
                difficultyBudget += remainder;

                estimatedInstanceTime += Mathf.Max(estimatedPartTime1, estimatedPartTime);

                wavePart1.instantDone = true;
            }
            else //One Part Wave
            {
                float remainder;
                WavePart wavePart = GenerateWavePart(aWave, difficulty, myDifficultyBudget, PickSpawnLocation(), out remainder);
                estimatedInstanceTime += estimatedPartTime; 
                current_WaveInstance.parts.Add(wavePart);
                difficultyBudget += remainder;
            }

            if (i < amountOfParts - 1 && Random.Range(0f, 2f) < 1) //Add Pause
            {
                current_WavePart = aWave.AddComponent<WavePart>();
                float waitTime = Random.Range(3f, 5f); //between 3 - 5 secconds
                estimatedInstanceTime += waitTime;
                current_WavePart.maxInbetweenPieceCooldown = waitTime; 
                current_WaveInstance.parts.Add(current_WavePart);
            }
        }

        current_WaveInstance.estimatedWaveTime = estimatedInstanceTime;

        Debug.Log("amountOfParts: " + amountOfParts + ", budget: " + difficultyBudgetMax + ", estimated time: " + estimatedInstanceTime);

        return current_WaveInstance;
    }

    public float estimatedPartTime;
    public WavePart GenerateWavePart(GameObject aWave, float difficulty, float budget, Vector3 location, out float remainder)
    {
        estimatedPartTime = 0.5f;
        current_WavePart = aWave.AddComponent<WavePart>();

        int escape = 0;
        while (budget > 0 && escape < 30)
        {
            escape++;
            int rarity = PickRarity(difficulty);
            //Debug.Log("max rarity: " + rarity + ", budget: " + budget);
            float pieceCost;
            WaveItem item = PickPiece(budget, rarity, out pieceCost);
            if (item == null || item.difficultyCost == 0)
            {
                break;
            }
            int toSpawn = Random.Range(item.groupSize_MinToMax.x, item.groupSize_MinToMax.y + 1);
            for (int j = 0; j < toSpawn; j++)
            {
                if (budget >= pieceCost)
                {
                    current_WavePart.pieces.Add(item.GetComponent<BasePiece>());
                    budget -= pieceCost;
                    estimatedPartTime += 0.5f;
                }
            }
        }

        current_WavePart.spawnLocation = location;
        remainder = budget;
        return current_WavePart;
    }
    public WaveItem PickPiece(float budget, int rarity, out float pieceCost)
    {
        WaveItem toAdd = null;

        for (int fallbackRarity = rarity; fallbackRarity >= 0; fallbackRarity--)
        {
            if (toAdd != null) break;
            //Debug.Log("fallbackRarity " + fallbackRarity + ", budget: " + budget);
            if (pieceList.ContainsKey(fallbackRarity) && pieceList[fallbackRarity].Count > 0)
            {
                List<WaveItem> pieceCanidates = new List<WaveItem>(pieceList[fallbackRarity]);
                int count = pieceCanidates.Count;
                for (int i = 0; i < count; i++)
                {
                    //Debug.Log("pieceCanidates: " + pieceCanidates.Count);
                    WaveItem canidate = pieceCanidates[Random.Range(0, pieceCanidates.Count)];
                    if (canidate.difficultyCost < budget)
                    {
                        toAdd = canidate;
                        break;
                    }
                    else
                    {
                        pieceCanidates.Remove(canidate);
                    }
                }
            }
        }

        if (toAdd == null)
        {
            Debug.LogWarning("No Suitable Piece Found below rarity: " + rarity);
            pieceCost = 0;
            return null;
        }

        pieceCost = toAdd.difficultyCost;
        if (!current_WaveInstance.piecesVisualized.ContainsKey(toAdd.image)) current_WaveInstance.piecesVisualized.Add(toAdd.image, 0);
        current_WaveInstance.piecesVisualized[toAdd.image]++;
        return toAdd;
    }
    public float DifficultyBudget(float difficulty)
    {
        float outputBudget = 0;
        outputBudget = (difficulty * 0.30f) + 1f;
        return outputBudget * Random.Range(10f, 20f);
    }

    float rarityScaler = 2;
    public int PickRarity(float difficulty)
    {
        int rarityToSpawn = RarityToSpawn(RollValue(difficulty), rarityScaler, maxRarity);
        return rarityToSpawn;
    }
    public int PickAmountOfParts(float difficulty)
    {
        return RarityToSpawn(RollValue(difficulty), 2f, 10);
    }

    public Vector3 PickSpawnLocation()
    {
        int i = 0;
        while (true)
        {
            i++;
            Vector3 spawnLocation = new Vector3(
            Random.Range(Board.transform.position.x - (4 * Board.transform.localScale.x), Board.transform.position.x + (4 * Board.transform.localScale.x)),
            Random.Range(Board.transform.position.y - (4 * Board.transform.localScale.y), Board.transform.position.y + (4 * Board.transform.localScale.y)),
            0
            );
            if (i < 10 && (Mathf.Abs(spawnLocation.x / Board.transform.localScale.x) < 2 || Mathf.Abs(spawnLocation.y / Board.transform.localScale.y) < 2))
                continue;
            return spawnLocation;
        }
    }

    float k = 0.05f;
    float a = 10f;
    float b = 1f;
    float RollValue(float luck)
    {
        float output = 6;

        output = 1 / (
            (1 / a) +
            (((1 / b) - (1 / a)) *
            (1 - (Mathf.Pow((float)System.Math.E, -(k * luck))))
            ));

        return output;
    }

    int RarityToSpawn(float rollValue, float rarityScaler, int maxRarity)
    {
        int rarityToSpawn = 1;
        for (rarityToSpawn = 1; rarityToSpawn <= Mathf.Pow(rarityScaler, maxRarity); rarityToSpawn++)
        {
            float random = Random.Range(0, rollValue);
            //Debug.Log(debugName + ": " + random + "/" + rollValue);
            if (random > 1) break;
        }
        rarityToSpawn = Mathf.FloorToInt(Mathf.Clamp(Mathf.Log(rarityToSpawn, rarityScaler), 1.1f, int.MaxValue));
        return rarityToSpawn;
    }
}
