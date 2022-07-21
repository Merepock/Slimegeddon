using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private int waveSize, sizes;
    public int ticks;
    public GameObject small, medium, large, danger, bomb, player;
    public GameObject[] toSpawn;   
    public List<Vector2> spawnPoints, corners;
    public float intervalTimer = 2.5f, specialTimer;
    private float currentTimer = 2.5f; //This is added to the interval timer each time it ticks and will change during the game
    public PlayerController playerController;
    


    void Start()
    {
        corners.Add(new Vector2(10.0f, 6.0f)); //Top right
        corners.Add(new Vector2(10.0f, -6.0f)); //Bottom Right
        corners.Add(new Vector2(-10.0f, 6.0f)); //Top Left
        corners.Add(new Vector2(-10.0f, -6.0f)); //Bottom Left

        waveSize = 3;
        ticks = 0;
        sizes = 0;
        toSpawn[0] = small;
        toSpawn[1] = medium;
        toSpawn[2] = large;
        
        playerController = player.GetComponent<PlayerController>();

        specialTimer = Random.Range(20f, 30f);
    }

    void Update()
    {
        while (playerController.health > 0)
        {
            intervalTimer -= Time.deltaTime;
            specialTimer -= Time.deltaTime;

            if (intervalTimer <= 0)
            {
                for (int i = 0; i < waveSize; i++)
                {
                    spawnPoints.Add(new Vector2(10.0f, (Random.Range(3.75f, -3.75f))));
                    spawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), 6.0f));
                    spawnPoints.Add(new Vector2(-10.0f, (Random.Range(3.75f, -3.75f))));
                    spawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), -6.0f));

                    int n = Random.Range(0, spawnPoints.Count);
                    Vector2 SpawnPos = spawnPoints[n];
                    
                    Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(60 + (n * 90), 120 + (n * 90)));
                    GameObject Enemy = Instantiate(toSpawn[UnityEngine.Random.Range(0, sizes)], SpawnPos, rotation);
                    setIgnoreWalls(Enemy);
                    spawnPoints.Clear();
                }

                ticks += 1;

                switch (ticks)
                {
                    case 8:
                        sizes = 2;
                        break;
                    case 30:
                        sizes = 3;
                        currentTimer += 0.5f;
                        break;
                }

                if (ticks % 25 == 0)
                {
                    waveSize += 1;
                    currentTimer += 0.5f;
                }

                intervalTimer += currentTimer;
            }

            if(specialTimer <= 0)
            {
                int specialSpawnChance = Random.Range(0, 31);

                GameObject specialEnemy;

                if (specialSpawnChance < 10)
                {
                    specialEnemy = SpawnDangerSlime();
                    setIgnoreWalls(specialEnemy);
                }

                if (specialSpawnChance < 15)
                {
                    specialEnemy = SpawnBombSlime();
                    setIgnoreWalls(specialEnemy);
                }

                specialTimer += Random.Range(15f, 20f);
            }

            break;
        }
    }

    private GameObject SpawnDangerSlime()
    {
        int idx;

        Vector2 currentPosition = player.transform.position;

        if(currentPosition.x >= 0)
        {
            if(currentPosition.y >= 0)
            {
                idx = 0;
            }
            else 
            {
                idx = 1;
            }
        }
        else
        {
            if(currentPosition.y >= 0)
            {
                idx = 2;
            }
            else
            {
                idx = 3;
            }
        }

        return Instantiate(danger, corners[idx], transform.rotation);
    }

    private GameObject SpawnBombSlime()
    {
        spawnPoints.Add(new Vector2(10.0f, (Random.Range(3.75f, -3.75f))));
        spawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), 6.0f));
        spawnPoints.Add(new Vector2(-10.0f, (Random.Range(3.75f, -3.75f))));
        spawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), -6.0f));

        int n = Random.Range(0, spawnPoints.Count);
        Vector2 SpawnPos = spawnPoints[n];
        spawnPoints.Clear();
                    
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(60 + (n * 90), 120 + (n * 90)));
        return  Instantiate(bomb, SpawnPos, rotation);              
    }

    private void setIgnoreWalls (GameObject o) 
    {
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Wall").GetComponent<Collider2D>(), o.GetComponent<Collider2D>(), true);
    }
}


