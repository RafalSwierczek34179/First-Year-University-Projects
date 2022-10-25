using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GunEnemy : GenericEnemy
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private float shotsPerSecond = 1.5f;
    private float timeBeforeNextShot = 0f;
    protected override void Awake()
    {
        base.Awake();
        Init(20, 0);
        // aiDestinationSetter can only take in transforms, therefore spawnLocation is first a prefab of an empty object which is then replaced by its instantiated self thats in the scene
        spawnLocation = Instantiate(spawnLocation);
        spawnLocation.transform.position = transform.position;
    }
    protected override void Update()
    {
        base.Update();
        Shoot();
    }
    protected override bool PlayerDetection()
    {
        base.PlayerDetection();
        // Same as original PlayerDetection except that now it chooses to target its spawn location rather than stand still if the player is out of range
        aiDestinationSetter.target = (!playerInRange ? spawnLocation.transform : player.transform);
        return playerInRange;
    }
    private void Shoot()
    {
        timeBeforeNextShot -= 1 * Time.deltaTime;
        if (!playerInRange)
        {
            timeBeforeNextShot = 0f;
        }
        else if(timeBeforeNextShot <= 0f)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z - 90f);
            Instantiate(bullet, transform.TransformPoint(new Vector3(0f, 0.7f, 0f)), rotation);
            timeBeforeNextShot = shotsPerSecond;
        }
    }
}
