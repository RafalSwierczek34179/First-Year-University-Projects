using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rigid2D : MonoBehaviour
{
    protected Rigidbody2D rigid2D;
    protected virtual void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
}
