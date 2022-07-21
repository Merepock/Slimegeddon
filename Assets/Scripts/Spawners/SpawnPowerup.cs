using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerup : MonoBehaviour
{
    public float spawnTimer = 20.0f;
    public GameObject powerUp, heartCapsule, ghostPowerup, wipePowerup, missileRestock, spreadBulletsPowerup;
    private PlayerController playerStats;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        while (playerStats.health > 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                int chance = Random.Range(0, 101);
                
                if (chance % 2 == 0)
                {
                    Instantiate(missileRestock, new Vector2(Random.Range(-9, 9), Random.Range(-5, 5)), transform.rotation);
                }

                if (chance <= 100 && chance > 90)
                {
                    powerUp = wipePowerup;
                }
                else if (chance <= 90 && chance > 60)
                {
                    powerUp = ghostPowerup;
                }
                else if (chance <= 60 && chance > 50)
                {
                    powerUp = spreadBulletsPowerup;
                }
                else
                {
                    powerUp = heartCapsule;
                }

                Instantiate(powerUp, new Vector2(Random.Range(-9, 9), Random.Range(-5, 5)), transform.rotation);

                spawnTimer += 20.0f;
            }
            break;
        }

    }
}
