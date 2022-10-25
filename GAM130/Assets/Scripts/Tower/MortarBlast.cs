using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MortarBlast : MonoBehaviour
{
    [SerializeField] Collider[] hitColliders;

    public void ExplodeEnemies(int damageValue, GameObject originTower)
    {
        hitColliders = Physics.OverlapSphere(transform.position, 8f, 1 << 3);

        // For each enemy hit in the overlapsphere, damage them.
        foreach (Collider hitCollider in hitColliders)
        {
            //FindObjectOfType<MoneyManager>().GainMoney(1);
            hitCollider.GetComponent<EnemyHealth>().TakeDamage(damageValue, originTower);
        }
    }
}