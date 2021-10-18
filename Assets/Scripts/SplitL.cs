using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitL : MonoBehaviour
{
    public GameObject medium, splat;
    private EnemyController mediumStats;
    private EnemyController largeStats;

    void Start()
    {
        mediumStats = medium.GetComponent<EnemyController>();
        largeStats = GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if(largeStats.HitPoints == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(medium, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                }
                Instantiate(splat, transform.position, transform.rotation);
                AddScore();
                Destroy(gameObject);
            }
        }
    }

    void AddScore()
    {
        GameObject thePlayer = GameObject.Find("Player");
        PlayerController playerScore = thePlayer.GetComponent<PlayerController>();
        playerScore.score += largeStats.KillScore;
    }
}
