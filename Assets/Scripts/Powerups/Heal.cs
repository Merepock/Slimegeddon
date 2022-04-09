using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Powerup
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Activate();
    }

    protected override void Activate()
    {
        playerController.playSound(3);
        if(playerController.health < 5)
        {
            playerController.health++;
        }
        Destroy(gameObject);
    }
}
