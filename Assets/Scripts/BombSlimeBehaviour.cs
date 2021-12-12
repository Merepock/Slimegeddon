using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSlimeBehaviour : EnemyController
{
    public GameObject explosion;
    protected override void destroy()
    {
        GameObject blast = Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0));
        blast.GetComponent<Explosion>().setStats(false, 8.0f);
        GameObject.Find("Player").GetComponent<PlayerController>().score += this.killScore;
        Destroy(gameObject);
    }
}
