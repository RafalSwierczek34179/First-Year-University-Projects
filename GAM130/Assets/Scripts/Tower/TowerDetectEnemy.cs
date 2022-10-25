using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetectEnemy : MonoBehaviour
{
    [HideInInspector]
    float Radius;
    LayerMask Default;
    Vector3 Area;
    public Collider[] hitColliders;
    public void DetectEnemiesOnSpawn()
    {
        //detects any enemies that are already inside the range of the tower when it is spawned in and adds them to the list of enemies
        if (this.GetComponentInParent<TowerAttackScript>().isRailgunTower == true)
        {
            Area = this.GetComponentInParent<BoxCollider>().bounds.size * (transform.localScale.x + transform.localScale.z);
            hitColliders = Physics.OverlapBox(transform.position, Area / 2, Quaternion.identity, Default);
        }

        else
        {
            Radius = this.GetComponentInParent<CapsuleCollider>().radius + 4;
            hitColliders = Physics.OverlapSphere(transform.position, Radius, 1 << 3);
        }

        foreach (var hitCollider in hitColliders)
        {
            this.GetComponentInParent<TowerAttackScript>().AddEnemyToList(hitCollider.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            this.GetComponentInParent<TowerAttackScript>().AddEnemyToList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            this.GetComponentInParent<TowerAttackScript>().DeleteEnemyFromList(other.gameObject);
        }
    }
}
