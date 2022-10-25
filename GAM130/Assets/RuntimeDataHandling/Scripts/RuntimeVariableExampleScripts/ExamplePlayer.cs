using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlayer : MonoBehaviour
{
    public FloatVariable currentHealth;
    public FloatVariable maxHealth;

    private void Start()
    {
        currentHealth.RuntimeValue = maxHealth.RuntimeValue;
        PlayerDebugHealth();
    }

    public void TakeDamage(float amount)
    {
        currentHealth.RuntimeValue = Mathf.Clamp(currentHealth.RuntimeValue-amount,0,maxHealth.RuntimeValue);
        PlayerDebugHealth();
    }

    public void PlayerDebugHealth()
    {
        Debug.Log("Player Max Health = " + maxHealth.RuntimeValue);
        Debug.Log("Player Current Health = " + currentHealth.RuntimeValue);
    }
}
