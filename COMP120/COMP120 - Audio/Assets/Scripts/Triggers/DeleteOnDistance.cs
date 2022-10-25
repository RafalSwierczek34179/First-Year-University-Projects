using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnDistance : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float distance = 5f;
    
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) <= distance)
        {
            Destroy(gameObject);
        }
    }
}
