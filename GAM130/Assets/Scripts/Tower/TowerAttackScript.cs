using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackScript : MonoBehaviour
{
    public bool isSelected = false;
    public bool isTowerSpawnedIn = false;
    public int damageValue;
    public int towerIndex;
    public float timerValue;
    public float timerLimit;
    public bool isCurrentlyBuffed;
    public bool isRailgunTower;
    //public bool isLvl1 = false;
    public int towerCost;
    public float stayingPower = 0.5f; // Testing cannon stuff, remove if unsuccessful

    public bool[] upgradesOwned;
    public TowerTypes currentTowerType;

    public List<GameObject> enemyQueue;
    public GameObject[] towerRadi;

    public GameObject mortarExplosionPrefab;
    public GameObject cannonShotPrefab;
    public GameObject temporaryExplosion;

    public AudioSource AttackSFX;
    public AudioSource Damage1;
    public AudioSource Damage2;
    public AudioSource Damage3;
    public AudioSource Damage4;
    public AudioSource None;

    void Start()
    {
        enemyQueue = new List<GameObject>();
        upgradesOwned = new bool[3];
    }

    void Update()
    {
        if (isTowerSpawnedIn)
        {
            DoDamage();
        }
    }

    void DoDamage()
    {
        timerValue += Time.deltaTime;
        if (timerValue >= timerLimit && enemyQueue.Count != 0 && enemyQueue[0] != null)
        {
            if(currentTowerType.GetType().Name == "Pike")
            {
                timerValue = 0f;

                if (FindObjectOfType<TowerActions>().ReturnCurrentUpgradeLevel(currentTowerType) >= 2)
                {
                    enemyQueue[0].GetComponent<EnemyDebuff>().applyDebuff(2, this.gameObject);
                }

                enemyQueue[0].GetComponent<EnemyHealth>().TakeDamage(damageValue, this.gameObject);

                if (OptionsScript.muteSFX == false)
                {
                    AttackSound();
                    DamageSFX();
                }
            }
            else if (currentTowerType.GetType().Name == "Flintlock")
            {
                timerValue = 0f;
                enemyQueue[0].GetComponent<EnemyHealth>().TakeDamage(damageValue, this.gameObject);

                if (OptionsScript.muteSFX == false)
                {
                    AttackSound();
                    DamageSFX();
                }
            }
            else if (currentTowerType.GetType().Name == "Musket")
            {
                timerValue = 0f;
                enemyQueue[0].GetComponent<EnemyHealth>().TakeDamage(damageValue, this.gameObject);

                if (OptionsScript.muteSFX == false)
                {
                    AttackSound();
                    DamageSFX();
                }
            }
            else if (currentTowerType.GetType().Name == "Mortar")
            {
                timerValue = 0f;

                mortarExplosionPrefab = FindObjectOfType<TowerSpawner>().mortarExplosionPrefab;
                temporaryExplosion = Instantiate(mortarExplosionPrefab, new Vector3(enemyQueue[0].transform.position.x, 0, enemyQueue[0].transform.position.z), Quaternion.identity);
                temporaryExplosion.transform.parent = this.transform;
                temporaryExplosion.GetComponent<MortarBlast>().ExplodeEnemies(damageValue, this.gameObject);

                if (FindObjectOfType<TowerActions>().ReturnCurrentUpgradeLevel(currentTowerType) >= 3)
                {
                    enemyQueue[0].GetComponent<EnemyDebuff>().applyDebuff(3, this.gameObject);
                }

                if (OptionsScript.muteSFX == false)
                {
                    AttackSound();
                    DamageSFX();
                }

                Destroy(temporaryExplosion, 0.5f);
            }
            else if (currentTowerType.GetType().Name == "Halberdier")
            {
                timerValue = 0f;
                FindObjectOfType<HalberdierSupport>().SupportNearbyTowers();
            }
            else if (currentTowerType.GetType().Name == "Cannon")
            {
                timerValue = 0f;
                var lookAt = enemyQueue[0].transform.position;
                lookAt.y = 0;
                var rotation = Quaternion.LookRotation(lookAt);
                cannonShotPrefab = FindObjectOfType<TowerSpawner>().cannonShotPrefab;
                temporaryExplosion = Instantiate(cannonShotPrefab, lookAt, rotation);
                temporaryExplosion.transform.parent = this.transform;
                temporaryExplosion.GetComponent<CannonShot>().BeamAttack(damageValue, this.gameObject);

                if (OptionsScript.muteSFX == false)
                {
                    AttackSound();
                    DamageSFX();
                }

                Destroy(temporaryExplosion, stayingPower);
            }
        }

        // Remove already defeated enemies
        if (enemyQueue.Count != 0 && enemyQueue[0] == null)
        {
            DeleteEnemyFromList(null);
        }
    }

    public void AddEnemyToList(GameObject Enemy)
    {
        enemyQueue.Add(Enemy);
    }

    public void DeleteEnemyFromList(GameObject enemyObject)
    {
        for (int enemyIndex = 0; enemyIndex < enemyQueue.Count; enemyIndex++)
        {
            if (enemyQueue[enemyIndex] == enemyObject)
            {
                enemyQueue.RemoveAt(enemyIndex);
            }
        }
    }

    public void AttackSound()
    {
        int AttackSound = Random.Range(1, 3);
        switch (AttackSound)
        {
            case 1:
                AttackSFX.Play();
                break;

            case 2:
                None.Play();
                break;
        }
    }

    public void DamageSFX()
    {
        int DamageSound = Random.Range(1, 4);
        switch (DamageSound)
        {
            case 1:
                Damage1.Play();
                    break;

            case 2:
                Damage2.Play(0);
                break;

            case 3:
                Damage3.Play(0);
                break;

            case 4:
                Damage4.Play(0);
                break;
        }

    }
}
