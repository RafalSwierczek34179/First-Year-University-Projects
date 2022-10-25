using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Rigid2D
{
    protected int health;
    protected int damageOutput;
    public int DamageOutput
    {
        get { return damageOutput; }
    }

    protected virtual void Init(int health = 10, int damageOutput = 10)
    {
        this.health = health;
        this.damageOutput = damageOutput;
    }
    protected virtual void Update()
    {
        CheckHealth();
    }
    public void TakeDamage(int damageRecieved)
    {
        health -= damageRecieved;
    }
    protected virtual void OnDeath()
    {
        GameObject.Destroy(this.gameObject);
    }
    protected void CheckHealth()
    {
        if (health <= 0)
        {
            OnDeath();
        }
    }
}
