using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerup : MonoBehaviour
{
    public float spawnTimer = 20.0f;
    public GameObject heartCapsule, player;
    private PlayerController playerStats;

    void Start()
    {
        playerStats = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        while (playerStats.health > 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                GameObject powerUp = Instantiate(heartCapsule, new Vector2(Random.Range(-9, 9), Random.Range(-5, 5)), transform.rotation);
                spawnTimer += 20.0f;
            }
            break;
        }

    }
}