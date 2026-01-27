using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMerge : MonoBehaviour
{
    public float minImpactSpeed = 1.5f;   // stops gentle bumps merging
    public int maxValue = 99;

    private NumberBlock self;

    void Awake()
    {
        self = GetComponent<NumberBlock>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < minImpactSpeed)
            return;

        if (!collision.gameObject.TryGetComponent<NumberBlock>(out var other))
            return;

        // Prevent both blocks merging each other in the same hit
        if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
            return;

        int newValue = Mathf.Clamp(self.value + other.value, 0, maxValue);
        self.SetValue(newValue);
        GameManager.Instance?.CheckForWin(newValue);

        Destroy(other.gameObject);
    }
}
