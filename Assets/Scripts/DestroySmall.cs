using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySmall : MonoBehaviour
{
    private EnemyController smallStats;
    public GameObject splat;

    void Start()
    {
        smallStats = GetComponent<EnemyController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(splat, transform.position, transform.rotation);
            AddScore();
            Destroy(gameObject);
        }
    }

    void AddScore()
    {
        GameObject thePlayer = GameObject.Find("Player");
        PlayerController playerScore = thePlayer.GetComponent<PlayerController>();
        playerScore.score += smallStats.KillScore;
    }
}
