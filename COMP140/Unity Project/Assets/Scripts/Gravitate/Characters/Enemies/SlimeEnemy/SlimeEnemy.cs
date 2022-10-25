using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlimeEnemy : GenericEnemy
{
    private AdjustSize adjustSize;
    private CircleCollider2D circleCollider2D;
    private AIPath aiPath;
    [SerializeField] private GameObject slimeEnemyPrefab;
    private int size = 3;
    public int Size
    {
        set { if (1 <= value && value <= 3) { size = value; } else { size = 3; } }
    }
    protected override void Awake()
    {
        base.Awake();
        aiPath = GetComponent<AIPath>();
        adjustSize = GetComponentInChildren<AdjustSize>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {   
        switch (size)
        {
            case 2:
                base.Init(15, 15);
                aiPath.maxSpeed = 2;
                adjustSize.AdjustSlimeGFXSize(size);
                circleCollider2D.radius = 0.4f;
                aiPath.radius = 0.4f;
                rigid2D.mass = 20;
                break;
            case 1:
                base.Init(10, 7);
                aiPath.maxSpeed = 3;
                adjustSize.AdjustSlimeGFXSize(size);
                circleCollider2D.radius = 0.2f;
                aiPath.radius = 0.2f;
                rigid2D.mass = 10;
                break;
            default:
                base.Init(30, 25);
                aiPath.maxSpeed = 1;
                adjustSize.AdjustSlimeGFXSize(size);
                circleCollider2D.radius = 0.9f;
                aiPath.radius = 0.9f;
                rigid2D.mass = 40;
                break;
        }
    }
    protected override void OnDeath()
    {
        if (size != 1)
        {
            for (int slimeChildCount = 0; slimeChildCount < 2; slimeChildCount++)
            {
                GameObject slimeChild = Instantiate(slimeEnemyPrefab);
                // If size of dying slime was 3, spawn in 2 size 2 slimes, else if it was 2 then spawn in 2 size 1 slimes
                slimeChild.GetComponent<SlimeEnemy>().Size = (size == 2 ? 1 : 2);
                slimeChild.transform.position = this.transform.position + new Vector3(slimeChildCount, 0f, 0f);
            }
        }
        base.OnDeath();
    }   
}

