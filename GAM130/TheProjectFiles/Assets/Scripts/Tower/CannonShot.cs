using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    //Code by Jared
    [SerializeField]
    Collider[] hitColliders;
    public float attackTime = 0.5f;

    public void BeamAttack(int damageValue, GameObject originTower)
    {
        hitColliders = Physics.OverlapCapsule(transform.position, transform.position, 8f, 1 << 3);

        /*if (this.GetComponentInParent<TowerAttackScript>().isLvl1 == false)
        {
            attackTime = 0.5f;
        }*/

        // For each enemy hit in the overlapcapsule, damage them.
        foreach (Collider hitCollider in hitColliders)
        {
            FindObjectOfType<MoneyManager>().GainMoney(1);
            hitCollider.GetComponent<EnemyHealth>().TakeDamage(damageValue, originTower);
        }
        attackTime -= 0.5f;
            
    }
}
