using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumAndLarge : EnemyController
{
    public GameObject childSlime;

    protected override void destroy()
    {
        Instantiate(childSlime, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Instantiate(childSlime, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        GameObject.Find("Player").GetComponent<PlayerController>().score += this.killScore;
        Instantiate(splat, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
