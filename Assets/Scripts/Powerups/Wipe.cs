using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wipe : Powerup
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Activate();
        }
    }

    protected override void Activate()
    {
        playerController.playSound(5);
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (activeEnemies.Length != 0)
        {
            foreach (GameObject enemy in activeEnemies)
            {
                Destroy(enemy.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
