using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSlimeBehaviour : EnemyController
{
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        Vector2 direction = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);
        transform.up = direction;
        enemyRb.velocity = transform.up * MoveSpeed;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().health = 0;
            Destroy(gameObject);
        }
    }
}
