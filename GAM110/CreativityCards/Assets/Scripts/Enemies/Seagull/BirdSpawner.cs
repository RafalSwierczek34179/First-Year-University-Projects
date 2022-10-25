using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject bird;

    public int maxBirdSpeed;
    public int minBirdSpeed;

    public int maxSpawned;
    private int numberOfSpawnedBirds = 0;

    // Minimium distance the bird will spawn ahead/behind the player
    public int BirdSpawnBuffer;
    // The range after the buffer inwhich the bird can spawn
    public int spawnRange;
    // Side of the street the bird will spawn on, "far" is furthest from camera, close is the camera side
    private string streetSide = "null";
    private float birdSpawnX;
    private float birdSpawnY;
    private float birdSpawnZ;
    // The amount of time it takes between each bird spawning
    public float spawnTime = 5f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerArmature");        
        StartCoroutine(Spawnbird());
    }


    IEnumerator Spawnbird()
    {
        while (numberOfSpawnedBirds+1 <= maxSpawned)
        {
            float playerX = player.transform.position.x;            
            // Decides whether to spawn the bird infront or behind the player
            int birdSpawnRegionX = Random.Range(0, 2);
            // Decides which side of the road to spawn the player
            int birdSpawnRegionY = Random.Range(0, 2);


            // Decides where on the street the seagulls should spawn
            if (birdSpawnRegionY == 0)
            {
                streetSide = "close";
            }
            else
            {
                streetSide = "far";
            }

            if (birdSpawnRegionX == 0)
            {
                // Spawns AHEAD of the player
                birdSpawnX = Random.Range(playerX - BirdSpawnBuffer, playerX - BirdSpawnBuffer - spawnRange);
            }
            else
            {
                // Spawn BEHIND the player
                birdSpawnX = Random.Range(playerX + BirdSpawnBuffer, playerX + BirdSpawnBuffer + spawnRange);
            }

            if (streetSide == "far")
            {
                SetBirdCoordsFar(birdSpawnX);
            }
            else
            {
                SetBirdCoordsClose(birdSpawnX);
            }

            // Set position of the bird and then instantiate the bird
            Vector3 pos = new Vector3(birdSpawnX, birdSpawnY, birdSpawnZ); 
            GameObject newBird = Instantiate(bird,pos, Quaternion.identity) as GameObject;

            newBird.GetComponent<SeagullAI>().SetMaxSpeed(maxBirdSpeed);
            newBird.GetComponent<SeagullAI>().SetMinSpeed(minBirdSpeed);

            numberOfSpawnedBirds += 1;
            yield return new WaitForSeconds(spawnTime);
        }
    }

    /// <summary>
    /// Sets the coordinates of the bird if they're spawn is on the far side of the street
    /// </summary>
    /// <param name="relitiveBirdSpawnX">The X coordinate for the bird to spawn on </param>
    private void SetBirdCoordsFar(float relitiveBirdSpawnX)
    {
        birdSpawnX = relitiveBirdSpawnX;
        birdSpawnZ = 6;
        birdSpawnY = 6;
    }

    /// <summary>
    /// Sets the coordinates of the bird if they're spawn is on the close side of the street
    /// </summary>
    /// <param name="relitiveBirdSpawnX"></param>
    private void SetBirdCoordsClose(float relitiveBirdSpawnX)
    {
        birdSpawnX = relitiveBirdSpawnX;
        birdSpawnZ = -6;
        birdSpawnY = Random.Range(0, 6);
    }
}
