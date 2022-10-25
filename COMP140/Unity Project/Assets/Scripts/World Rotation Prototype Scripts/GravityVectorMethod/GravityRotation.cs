using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRotation : MonoBehaviour
{
    private const float gravity = 9.81f;

    private void FixedUpdate()
    {

        Physics2D.gravity = new Vector3(HorizontalComponentOfGravity(gravity), VerticalComponentOFGravity(gravity), 0f);
    }
    private float HorizontalComponentOfGravity(float gravity)
    {
        float angleInRadians = ConversionToRadians(transform.rotation.eulerAngles.z);
        return gravity * Mathf.Sin(angleInRadians);
    }

    private float VerticalComponentOFGravity(float gravity)
    {
        float angleInRadians = ConversionToRadians(transform.rotation.eulerAngles.z);
        return -gravity * Mathf.Cos(angleInRadians);
    }

    private float ConversionToRadians(float angleInDegrees)
    {
        return angleInDegrees * (Mathf.PI / 180f);
    }
}
