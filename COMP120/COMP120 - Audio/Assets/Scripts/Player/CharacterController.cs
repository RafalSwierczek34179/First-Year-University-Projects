using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Rigidbody3DObject : MonoBehaviour
{
    [HideInInspector]
    public new Rigidbody rigidbody;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
}
public class CharacterController : Rigidbody3DObject
{

    [SerializeField]
    float playerSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(new Vector3(0, 0f, playerSpeed), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddRelativeForce(new Vector3(-playerSpeed, 0f, 0f), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddRelativeForce(new Vector3(0f, 0f, -playerSpeed), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddRelativeForce(new Vector3(playerSpeed, 0f,0f), ForceMode.Impulse);
        }
    }
}
