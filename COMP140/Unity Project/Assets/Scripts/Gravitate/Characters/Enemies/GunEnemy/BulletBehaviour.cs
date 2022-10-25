using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : Rigid2D
{
    [SerializeField] private int bulletDamage = 7;

    private void FixedUpdate()
    {
        // MovePosition works on global coords, the code below moves the bullet 5 squares pers second using local coords, this is so that rotation doesn't affect trajectory, and converts those to global using TransformPoint
        Vector3 globalPosition = transform.TransformPoint(new Vector3(-5f * Time.deltaTime, 0f, 0f));
        rigid2D.MovePosition(globalPosition);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().TakeDamage(bulletDamage);
        }
        Destroy(this.transform.parent.gameObject);
    }

}
