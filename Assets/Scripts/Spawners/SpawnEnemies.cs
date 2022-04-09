using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private int WaveSize, sizes;
    public int ticks;
    public GameObject small, medium, large, danger, bomb, player;
    public GameObject[] ToSpawn;   
    public List<Vector2> SpawnPoints, Corners;
    public float IntervalTimer = 2.5f;
    private float CurrTimer = 2.5f; //This is added to the interval timer each time it ticks and will change during the game
    public PlayerController playerHP;
    


    void Start()
    {
        Corners.Add(new Vector2(10.0f, 6.0f));
        Corners.Add(new Vector2(10.0f, 6.0f));
        Corners.Add(new Vector2(-10.0f, 6.0f));
        Corners.Add(new Vector2(-10.0f, -6.0f));

        WaveSize = 3;
        ticks = 0;
        sizes = 0;
        ToSpawn[0] = small;
        ToSpawn[1] = medium;
        ToSpawn[2] = large;
        
        playerHP = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        while (playerHP.health > 0)
        {
            IntervalTimer -= Time.deltaTime;
            if (IntervalTimer <= 0)
            {
                for (int i = 0; i < WaveSize; i++)
                {
                    SpawnPoints.Add(new Vector2(10.0f, (Random.Range(3.75f, -3.75f))));
                    SpawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), 6.0f));
                    SpawnPoints.Add(new Vector2(-10.0f, (Random.Range(3.75f, -3.75f))));
                    SpawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), -6.0f));

                    int n = Random.Range(0, SpawnPoints.Count);
                    Vector2 SpawnPos = SpawnPoints[n];
                    
                    Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(60 + (n * 90), 120 + (n * 90)));
                    GameObject Enemy = Instantiate(ToSpawn[UnityEngine.Random.Range(0, sizes)], SpawnPos, rotation);
                    setIgnoreWalls(Enemy);
                    SpawnPoints.Clear();
                }

                ticks += 1;

                switch (ticks)
                {
                    case 8:
                        sizes = 2;
                        break;
                    case 30:
                        sizes = 3;
                        CurrTimer += 0.5f;
                        break;
                }

                if (ticks % 25 == 0)
                {
                    WaveSize += 1;
                    CurrTimer += 0.5f;
                }

                if (ticks > 20)
                {
                    GameObject specialEnemy;
                    int specialSpawnChance = Random.Range(0, 6);

                    if (specialSpawnChance == 1)
                    {
                        specialEnemy = SpawnDangerSlime();
                        setIgnoreWalls(specialEnemy);
                    }
                    if (specialSpawnChance % 6 == 0)
                    {
                        specialEnemy = SpawnBombSlime();
                        setIgnoreWalls(specialEnemy);
                    }
                }

                IntervalTimer = CurrTimer;
            }
            break;
        }
    }

    private GameObject SpawnDangerSlime()
    {
        return Instantiate(danger, Corners[Random.Range(0, Corners.Count)], transform.rotation);
    }

    private GameObject SpawnBombSlime()
    {
        SpawnPoints.Add(new Vector2(10.0f, (Random.Range(3.75f, -3.75f))));
        SpawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), 6.0f));
        SpawnPoints.Add(new Vector2(-10.0f, (Random.Range(3.75f, -3.75f))));
        SpawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), -6.0f));

        int n = Random.Range(0, SpawnPoints.Count);
        Vector2 SpawnPos = SpawnPoints[n];
        SpawnPoints.Clear();
                    
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(60 + (n * 90), 120 + (n * 90)));
        return  Instantiate(bomb, SpawnPos, rotation);              
    }

    private void setIgnoreWalls (GameObject o) 
    {
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Wall").GetComponent<Collider2D>(), o.GetComponent<Collider2D>(), true);
    }
}


