using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerup : MonoBehaviour
{
    public float spawnTimer = 20.0f;
    public GameObject powerUp, heartCapsule, ghostPowerup, wipePowerup;
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
                int chance = Random.Range(0, 20);
                if (chance % 4 == 0)
                {
                    powerUp = Instantiate(ghostPowerup, new Vector2(Random.Range(-9, 9), Random.Range(-5, 5)), transform.rotation);
                }
                else if (chance % 3 == 0)
                {
                    powerUp = Instantiate(wipePowerup, new Vector2(Random.Range(-9, 9), Random.Range(-5, 5)), transform.rotation);
                }
                else
                {
                    powerUp = Instantiate(heartCapsule, new Vector2(Random.Range(-9, 9), Random.Range(-5, 5)), transform.rotation);
                }
                spawnTimer += 20.0f;
            }
            break;
        }

    }
}
