using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField, Range(0f, 360f)]private float rotationPerSecond = 90f;

    private void Update()
    {
        // Rotation angle based on players input
        float rotationAngle = Input.GetAxisRaw("Horizontal") * rotationPerSecond * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.forward, rotationAngle);
    }
}
