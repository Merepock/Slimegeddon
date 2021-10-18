using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private int WaveSize, sizes;
    public int ticks;
    public GameObject small, medium, large, player;
    public GameObject[] ToSpawn;   
    public List<Vector2> SpawnPoints;
    public float IntervalTimer = 2.5f;
    private float CurrTimer = 2.5f; //This is added to the interval timer each time it ticks and will change during the game
    public PlayerController playerHP;
    


    void Start()
    {
        WaveSize = 3; //initial size of enemy waves
        ticks = 0; //counts how many times the interval timer has hit 0
        sizes = 0; //Restricts to small enemies spawning only at the start of the game.
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
                    SpawnPoints.Add(new Vector2(10.0f, (Random.Range(3.75f, -3.75f))));  //Right border of the screen
                    SpawnPoints.Add(new Vector2(-10.0f, (Random.Range(3.75f, -3.75f))));  //Left border of the screen
                    SpawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), 6.0f));  //Top border of the screen
                    SpawnPoints.Add(new Vector2((Random.Range(7.5f, -7.5f)), -6.0f)); //Bottom border of the screen

                    int n = Random.Range(0, SpawnPoints.Count);
                    Vector2 SpawnPos = SpawnPoints[n];
                    float UBAngle = 0.0f, LBAngle = 0.0f;
                    switch (n)
                    {
                        case 0:
                            LBAngle = 60.0f;
                            UBAngle = 120.0f;
                            break;
                        case 1:
                            LBAngle = 240.0f;
                            UBAngle = 300.0f;
                            break;
                        case 2:
                            LBAngle = 150.0f;
                            UBAngle = 210.0f;
                            break;
                        case 3:
                            LBAngle = 330.0f;
                            UBAngle = 390.0f;
                            break;
                    }
                    Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(LBAngle, UBAngle));
                    GameObject Enemy = Instantiate(ToSpawn[UnityEngine.Random.Range(0, sizes)], SpawnPos, rotation);
                    Physics2D.IgnoreCollision(GameObject.FindWithTag("Wall").GetComponent<Collider2D>(), Enemy.GetComponent<Collider2D>(), true);
                    SpawnPoints.Clear();
                }
                ticks += 1;
                switch (ticks) //Select statement which will increase the upper bound of the range of the ToSpawn index
                {
                    case 8:
                        sizes = 2; //Medium enemies will be allowed to spawn
                        break;
                    case 30:
                        sizes = 3; //Large enemies will be allowed to spawn
                        CurrTimer += 1.0f; //Interval increases when large enemies spawn.
                        break;
                }
                if (ticks % 30 == 0)
                {
                    WaveSize += 1; //The size of the enemy waves increases with every multiple of 30
                    CurrTimer += 1.0f; //Interval timer increases with the wave size
                }
                IntervalTimer = CurrTimer; //Reset the timer for another interval
            }
            break;
        }
    }
}


