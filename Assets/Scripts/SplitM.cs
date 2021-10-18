using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitM : MonoBehaviour
{
    public GameObject small, splat;
    private EnemyController smallStats;
    private EnemyController mediumStats;
    void Start()
    {
        smallStats = small.GetComponent<EnemyController>();
        mediumStats = GetComponent<EnemyController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if(mediumStats.HitPoints == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(small, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
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
        playerScore.score += mediumStats.KillScore;
    }
}
