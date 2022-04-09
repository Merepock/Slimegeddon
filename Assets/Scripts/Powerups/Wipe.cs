using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wipe : Powerup
{
    protected override IEnumerator onPickup()
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
        disableGraphics();
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
