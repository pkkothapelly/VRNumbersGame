using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner Instance;

    [Header("Prefab")]
    public GameObject numberBlockPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Settings")]
    public int targetBlockCount = 20;
    public int minValue = 1;
    public int maxValue = 20;              // inclusive
    public float topUpDelay = 10f;

    private bool topUpScheduled;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnInitial();
    }

    void SpawnInitial()
    {
        for (int i = 0; i < targetBlockCount; i++)
        {
            SpawnRandom();
        }
    }

    void SpawnRandom()
    {
        if (numberBlockPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
            return;

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject go = Instantiate(numberBlockPrefab, sp.position, sp.rotation);

        int value = Random.Range(minValue, maxValue + 1);
        var nb = go.GetComponent<NumberBlock>();
        if (nb != null) nb.SetValue(value);
    }

    int CountLiveBlocks()
    {
        return FindObjectsByType<NumberBlock>(FindObjectsSortMode.None).Length;
    }

    // Call this ONLY when a merge consumes/destroys a block
    public void NotifyBlockConsumed()
    {
        if (!topUpScheduled)
            StartCoroutine(TopUpAfterDelay());
    }

    IEnumerator TopUpAfterDelay()
    {
        topUpScheduled = true;
        yield return new WaitForSeconds(topUpDelay);

        // Top up back to target count (in case multiple merges happened)
        int live = CountLiveBlocks();
        int missing = Mathf.Max(0, targetBlockCount - live);

        for (int i = 0; i < missing; i++)
            SpawnRandom();

        topUpScheduled = false;
    }
}
