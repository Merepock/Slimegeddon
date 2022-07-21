using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool friendly;
    private PlayerController p;
    public Vector3 radius;
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void setStats(bool isFriendly, float blastRadius)
    {
        this.friendly = isFriendly;
        this.radius = new Vector3(blastRadius, blastRadius, 0);
    }

    void Update()
    {
        this.transform.localScale = this.radius;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().hitPoints = 0;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (!friendly && p.canTakeDamage)
            {
                p.takeDamage();
            }
        }
    }
}
