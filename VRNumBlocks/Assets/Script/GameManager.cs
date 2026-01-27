using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text targetText;
    public TMP_Text timerText;
    public TMP_Text statusText;

    [Header("Gameplay")]
    public int targetMin = 10;
    public int targetMax = 40;
    public float roundTime = 15f;

    int target;
    float timeLeft;
    bool roundActive;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartRound();
    }

    void Update()
    {
        if (!roundActive) return;

        timeLeft -= Time.deltaTime;
        UpdateUI();

        if (timeLeft <= 0f)
        {
            LoseRound();
        }
    }

    void StartRound()
    {
        target = Random.Range(targetMin, targetMax + 1);
        timeLeft = roundTime;
        roundActive = true;

        if (statusText) statusText.text = "";
        UpdateUI();
    }

    void UpdateUI()
    {
        if (targetText) targetText.text = $"Simon needs: {target}";
        if (timerText) timerText.text = $"Time: {Mathf.CeilToInt(timeLeft)}";
    }

    void WinRound()
    {
        roundActive = false;
        if (statusText) statusText.text = "✅ Success!";
        Invoke(nameof(StartRound), 2f);
    }

    void LoseRound()
    {
        roundActive = false;
        if (statusText) statusText.text = "❌ Time up!";
        Invoke(nameof(StartRound), 2f);
    }

    // called when any block becomes a new value (after merge)
    public void CheckForWin(int blockValue)
    {
        if (!roundActive) return;
        if (blockValue == target)
            WinRound();
    }
}