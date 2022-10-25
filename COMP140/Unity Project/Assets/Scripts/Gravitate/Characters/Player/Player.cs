using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [SerializeField, Range(0f, 360f)] private float rotationPerSecond = 90f;
    [SerializeField] private float velocityRequiredToDealDamage = 6f;
    protected override void Awake()
    {
        base.Awake();
        Init(100);
    }
    protected override void Update()
    {
        base.Update();
        PlayerRotation(HandleRotationInput());
        PlayerSpeed();
    }
    protected override void OnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private float HandleRotationInput()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Mouse0))
        {
            input = Input.mousePosition.x > (Screen.width/2f) ? 1 : -1;
        }
        return input * rotationPerSecond * Time.deltaTime;
    }
    private void PlayerRotation(float rotationAngle)
    {
        transform.RotateAround(transform.position, Vector3.forward, rotationAngle);
    }
    private void PlayerSpeed()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            // Turns graity for the player on and off depending on which button the player has pressed(up turns it off and down turns it on)
            rigid2D.gravityScale = Input.GetAxisRaw("Vertical") > 0f ? 1 : 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            float xVelocity = rigid2D.velocity.x;
            float yVelocity = rigid2D.velocity.y;
            float magnitudeOfVelocity = Mathf.Sqrt((xVelocity * xVelocity) + (yVelocity * yVelocity));
            if (magnitudeOfVelocity >= velocityRequiredToDealDamage)
            {
                collision.transform.GetComponent<GenericEnemy>().TakeDamage(damageOutput);
            }
            else
            {
                TakeDamage(collision.transform.GetComponent<GenericEnemy>().DamageOutput);
            }
        }
    }
}
