using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public GameObject childSlime, splat;
    private EnemyController enemyStats;

    void Start()
    {
        enemyStats = GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if(enemyStats.HitPoints == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(childSlime, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                }
                Instantiate(splat, transform.position, transform.rotation);
                enemyStats.AddScore();
                Destroy(gameObject);
            }
        }
    }
}
