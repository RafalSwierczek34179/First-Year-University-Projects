using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private const float gravity = 9.81f;
    private int gameOver = 1;
    public int GameOver
    {
        set { gameOver = 0; }
    }

    private void FixedUpdate()
    {
        Physics2D.gravity = new Vector3(HorizontalComponentOfGravity(gravity * gameOver), VerticalComponentOFGravity(gravity * gameOver), 0f);
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
