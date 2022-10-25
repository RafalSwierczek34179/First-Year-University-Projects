using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] int damageAmount = 20;
    [SerializeField] GameObject mapPrefab;
    NavMeshAgent enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        mapPrefab = GameObject.Find("Pendennis Castle").gameObject;
        enemyAI = GetComponent<NavMeshAgent>();
        enemyAI.SetDestination(mapPrefab.transform.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Castle")
        {
            mapPrefab.GetComponent<CastleHealthManager>().TakeDamage(damageAmount);
            // Stops the tween animating the enemy HB from trying to animate it when the object gets destroyed
            this.GetComponentInChildren<EnemyHealthBar>().KillTween();
            GameObject.Destroy(this.gameObject);
        }
    }
}
