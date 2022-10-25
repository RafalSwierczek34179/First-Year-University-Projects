using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HalberdierSupport : MonoBehaviour
{
    [HideInInspector] float radius;
    [SerializeField] Collider[] hitColliders;
    [HideInInspector] GameObject supportRadius;
    public float timerValue;
    public float timerCooldown = 0.85f;

    void Start()
    {
        supportRadius = this.gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        timerValue += Time.deltaTime;
    }

    public void SupportNearbyTowers()
    {
        radius = supportRadius.GetComponent<CapsuleCollider>().radius;
        hitColliders = Physics.OverlapCapsule(transform.position, transform.position, 8);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Tower")
            {
                // Get the tower's current states
                bool isCurrentlyBuffed = hitCollider.gameObject.GetComponent<TowerAttackScript>().isCurrentlyBuffed;
                bool isPlaced = hitCollider.gameObject.GetComponent<TowerAttackScript>().isTowerSpawnedIn;

                if (isCurrentlyBuffed == false && isPlaced)
                {
                    // If not buffed, increase their damage.
                    // Base DMG + (Base DMG / 2) = Buffed DMG
                    int currentDamage = hitCollider.gameObject.GetComponent<TowerAttackScript>().damageValue;
                    currentDamage += (currentDamage / 2);
                    hitCollider.gameObject.GetComponent<TowerAttackScript>().damageValue = currentDamage;
                    hitCollider.gameObject.GetComponent<TowerAttackScript>().isCurrentlyBuffed = true;
                }
                else
                {
                    // If buffed, then remove the buff after a cooldown from Time.deltatime
                    // Buffed DMG - (Buffed DMG / 3) = Base DMG
                    StartCoroutine(BuffCooldown(hitCollider, isCurrentlyBuffed, isPlaced));
                }
            }
        }
    }

    IEnumerator BuffCooldown(Collider buffedTower, bool isBuffed, bool isPlaced)
    {
        if (isBuffed && isPlaced)
        {
            yield return new WaitForSeconds(timerCooldown);

            int currentDamage = buffedTower.gameObject.GetComponent<TowerAttackScript>().damageValue;
            currentDamage -= (currentDamage / 3);
            buffedTower.gameObject.GetComponent<TowerAttackScript>().damageValue = currentDamage;
            buffedTower.gameObject.GetComponent<TowerAttackScript>().isCurrentlyBuffed = false;
        }
    }
}