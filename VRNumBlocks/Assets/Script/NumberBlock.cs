using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberBlock : MonoBehaviour
{
    [Range(0, 99)]
    public int value = 1;

    [SerializeField] private TMP_Text[] labels;

    void Awake()
    {
        CacheLabelsIfNeeded();
        UpdateVisual();
    }

    public void SetValue(int newValue)
    {
        value = Mathf.Clamp(newValue, 0, 99);
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        CacheLabelsIfNeeded();

        if (labels == null) return;

        string s = value.ToString();
        for (int i = 0; i < labels.Length; i++)
        {
            if (labels[i] != null)
                labels[i].text = s;
        }
    }

    void CacheLabelsIfNeeded()
    {
        if (labels == null || labels.Length == 0)
            labels = GetComponentsInChildren<TMP_Text>(true);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        CacheLabelsIfNeeded();
        UpdateVisual();
    }
#endif
}
