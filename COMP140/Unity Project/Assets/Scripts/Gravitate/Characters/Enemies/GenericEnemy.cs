using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GenericEnemy : Character
{
    [SerializeField] protected GameObject player;
    protected AIDestinationSetter aiDestinationSetter;
    protected bool playerInRange = false;
    protected override void Awake()
    {
        base.Awake();
        Init();
        player = GameObject.FindGameObjectWithTag("Player");
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }
    protected override void Update()
    {
        base.Update();
        PlayerDetection();
    }
    protected virtual bool PlayerDetection()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) <= 8.5f)
        {
            aiDestinationSetter.target = player.transform;
            playerInRange = true;
        }
        else
        {
            aiDestinationSetter.target = this.transform;
            playerInRange = false;
        }
        return playerInRange;
    }

}
