using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Powerup
{
    protected override IEnumerator onPickup()
    {
        playerController.playSound(3);
        if(playerController.health < 5)
        {
            playerController.health++;
        }
        disableGraphics();
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
