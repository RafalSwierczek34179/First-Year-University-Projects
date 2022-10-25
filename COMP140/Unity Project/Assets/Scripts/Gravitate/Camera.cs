using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 zOffset = new Vector3(0f, 0f, -10f);
    private void Update()
    {
        transform.position = player.GetComponent<Transform>().position + zOffset;
        transform.rotation = player.GetComponent<Transform>().rotation;
    }
}
