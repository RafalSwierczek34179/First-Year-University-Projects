using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullAI : MonoBehaviour
{
    // Bird Speed
    private int speed;
    // Player
    // Player and bird Distance
    private float aiToPlayerDistance;
    private GameObject player;

    
    public int maxbirdSpeed;
    public int minBirdSpeed;

    /// <summary>
    /// Sets the max speed for the random range of bird speeds
    /// </summary>
    /// <param name="s"></param>
    public void SetMaxSpeed (int maxSpeed)
    {
        maxbirdSpeed = maxSpeed;
    }

    /// <summary>
    ///  Sets the min speed for the random range of bird speeds
    /// </summary>
    /// <param name="minSpeed"></param>
    public void SetMinSpeed (int minSpeed)
    {
        minBirdSpeed = minSpeed;
    }


    private void Start()
    {
        player = GameObject.Find("PlayerArmature");
        speed = Random.Range(minBirdSpeed, maxbirdSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        FindMovementDirection();
        AiMove();
        // Bird Speed        
    }

    /// <summary>
    /// Faces in the direction of the player
    /// </summary>
    void FindMovementDirection()
    {
        Collider playerCollider = player.GetComponent<Collider>();
        Vector3 vectorDirection = playerCollider.bounds.center;
        transform.LookAt(vectorDirection);
    }

    /// <summary>
    /// Moves ai towards player, stops at a distance of 2
    /// </summary>
    void AiMove()
    {
        aiToPlayerDistance = Vector3.Distance(transform.position, player.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
