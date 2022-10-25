using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebuffModifiers  : MonoBehaviour
{
    [Header("Slow Debuff Modifiers")]
    [Range(0, 100)] public float pikeSlowOccurrencePercentage = 12.5f;
    [Range(0, 100)] public float cannonSlowOccurrencePercentage = 50f;
    [Range(1, 10)] public int slowMultiplier = 3;
    [Range(0.1f, 2)] public float secondsPerSlowTick = 1;
    [Range(1, 10)] public int slowDurationInTicks = 4;

    [Header("Stun Debuff Modifiers")]
    [Range(0, 100)] public float cannonStunOccurrencePercentage = 25f;
    [Range(0.1f, 2)] public float secondsPerStunTick = 1;
    [Range(1, 10)] public int stunDurationInTicks = 4;

    [Header("Slow Debuff Modifiers")]
    [Range(0, 100)] public float mortarFireOccurrencePercentage = 10f;
    [Range(1, 20)] public int fireDamagePerSecond = 2;
    [Range(0.1f, 2)] public float secondsPerFireTick = 0.5f;
    [Range(1, 20)] public int fireDurationInTicks = 8;

    // Start is called before the first frame update
    void Start()
    {
        pikeSlowOccurrencePercentage = 100 / pikeSlowOccurrencePercentage;
        cannonSlowOccurrencePercentage = 100 / cannonSlowOccurrencePercentage;
        cannonStunOccurrencePercentage = 100 / cannonStunOccurrencePercentage;
        mortarFireOccurrencePercentage = 100 / mortarFireOccurrencePercentage;
    }
}
