using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class Damage : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damageHB = 20;
    public int addHealth;

    public bool isSpawned = false;

    public HealthBar healthBar;

    public GameObject spawner;
    public GameObject healthObj;
    private GameObject newInstance;

    public AudioSource heartSpawn;


    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        heartSpawn = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Seagull")
        {
            TakeDamage(damageHB);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Health")
        {
            currentHealth = currentHealth + addHealth;            
            healthBar.SetHealth(currentHealth);
            isSpawned = false;
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {        
        if (currentHealth <= 0)
        {
            GameObject.Find("GameOver").gameObject.GetComponent<GameOver_script>().TurnOnGameOverScript();
        }
        if (currentHealth <= 40)
        {
            if (isSpawned == false)
            {
                isSpawned = true;
                CreatePrefab();
                GetComponent<AudioSource>().Play();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void CreatePrefab()
    {
        float instX = spawner.transform.position.x;
        float instY = spawner.transform.position.y;
        float instZ = spawner.transform.position.z;       

        Debug.Log("X: " + instX);
        Debug.Log("Y: " + instY);

        newInstance = Instantiate(healthObj, new Vector3(instX, instY, instZ), Quaternion.identity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
