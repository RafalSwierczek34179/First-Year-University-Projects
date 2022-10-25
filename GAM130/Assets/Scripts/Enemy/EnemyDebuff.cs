using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDebuff : MonoBehaviour
{
    [Header("General")]
    [SerializeField] debuffList[] enemyDebuffs;
    [SerializeField] float enemySpeed;
    [SerializeField] float editedEnemySpeed;
    [SerializeField] int loopCount;
    int pikeSlowChance;
    int cannonSlowChance;
    int cannonStunChance;
    int mortarFireChance;

    int slowMultiplier;
    int fireDamage;

    float secondsPerSlowTick;
    float secondsPerStunTick;
    float secondsPerFireTick;
    int slowTickAmount;
    int stunTickAmount;
    int fireTickAmount;

    enum debuffList
    {
        Slowed,
        Stunned,
        Fire,
        None
    }

    void Start()
    {
        enemySpeed = this.GetComponent<NavMeshAgent>().speed;
        enemyDebuffs = new debuffList[3] { debuffList.None, debuffList.None, debuffList.None };

        pikeSlowChance = (int)FindObjectOfType<EnemyDebuffModifiers>().pikeSlowOccurrencePercentage;
        cannonSlowChance = (int)FindObjectOfType<EnemyDebuffModifiers>().cannonSlowOccurrencePercentage;
        cannonStunChance = (int)FindObjectOfType<EnemyDebuffModifiers>().cannonStunOccurrencePercentage;
        mortarFireChance = (int)FindObjectOfType<EnemyDebuffModifiers>().mortarFireOccurrencePercentage;

        slowMultiplier = FindObjectOfType<EnemyDebuffModifiers>().slowMultiplier;
        fireDamage = FindObjectOfType<EnemyDebuffModifiers>().fireDamagePerSecond;

        secondsPerSlowTick = FindObjectOfType<EnemyDebuffModifiers>().secondsPerSlowTick;
        secondsPerStunTick = FindObjectOfType<EnemyDebuffModifiers>().secondsPerStunTick;
        secondsPerFireTick = FindObjectOfType<EnemyDebuffModifiers>().secondsPerFireTick;
        slowTickAmount = FindObjectOfType<EnemyDebuffModifiers>().slowDurationInTicks;
        stunTickAmount = FindObjectOfType<EnemyDebuffModifiers>().stunDurationInTicks;
        fireTickAmount = FindObjectOfType<EnemyDebuffModifiers>().fireDurationInTicks;
    }

    public void applyDebuff(int debuffNum, GameObject originTower)
    {
        int rand;
        switch (debuffNum)
        {
            case 0: // Slow (Pike)
                rand = Random.Range(0, pikeSlowChance);
                if (rand == 0 && enemyDebuffs[0] != debuffList.Slowed)
                {
                    enemyDebuffs[0] = debuffList.Slowed;
                    //this.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                    StopCoroutine(SlowDebuff());
                    StartCoroutine(SlowDebuff());
                }
                break;
            case 1: // Slow (Cannon)
                rand = Random.Range(0, cannonSlowChance);
                if (rand == 0 && enemyDebuffs[0] != debuffList.Slowed)
                {
                    enemyDebuffs[0] = debuffList.Slowed;
                    //this.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                    StopCoroutine(SlowDebuff());
                    StartCoroutine(SlowDebuff());
                }
                break;
            case 2: // Stun (Cannon)
                rand = Random.Range(0, cannonStunChance);
                if (rand == 0 && enemyDebuffs[1] != debuffList.Stunned)
                {
                    enemyDebuffs[1] = debuffList.Stunned;

                    StopCoroutine(SlowDebuff());
                    StartCoroutine(SlowDebuff());
                }
                break;
            case 3: // Fire (Mortar)
                rand = Random.Range(0, mortarFireChance);
                if (rand == 0 && enemyDebuffs[2] != debuffList.Fire)
                {
                    enemyDebuffs[2] = debuffList.Fire;

                    StopCoroutine(FireDebuff(originTower));
                    StartCoroutine(FireDebuff(originTower));
                }
                break;
        }
    }

    IEnumerator SlowDebuff()
    {
        this.GetComponent<NavMeshAgent>().speed = enemySpeed - (enemySpeed / slowMultiplier);
        editedEnemySpeed = enemySpeed - (enemySpeed / slowMultiplier);
        loopCount = 0;

        while (true)
        {
            loopCount++;
            if (loopCount >= slowTickAmount)
            {
                this.GetComponent<NavMeshAgent>().speed = enemySpeed;
                editedEnemySpeed = enemySpeed;
                enemyDebuffs[0] = debuffList.None;
                yield break;
            }

            yield return new WaitForSeconds(secondsPerSlowTick);
        }
    }

    IEnumerator StunDebuff()
    {
        this.GetComponent<NavMeshAgent>().speed = 0;
        editedEnemySpeed = 0;
        loopCount = 0;

        while (true)
        {
            loopCount++;
            if (loopCount >= stunTickAmount)
            {
                //this.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                this.GetComponent<NavMeshAgent>().speed = enemySpeed;
                editedEnemySpeed = enemySpeed;
                enemyDebuffs[1] = debuffList.None;
                yield break;
            }

            yield return new WaitForSeconds(secondsPerStunTick);
        }
    }

    IEnumerator FireDebuff(GameObject originTower)
    {
        loopCount = 0;
        while (true)
        {
            this.GetComponent<EnemyHealth>().TakeDamage(fireDamage, originTower);

            loopCount++;
            if (loopCount >= fireTickAmount)
            {
                enemyDebuffs[2] = debuffList.None;
                yield break;
            }

            yield return new WaitForSeconds(secondsPerFireTick);
        }
    }
}