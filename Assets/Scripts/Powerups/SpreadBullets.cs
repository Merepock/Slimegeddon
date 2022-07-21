using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullets : Powerup
{
    void Awake()
    {
        this.duration = 30f;
    }

    protected override void Activate()
    {
        playerController.playSound(9);
        playerController.spreadBullets = true;
        playerController.activePowerup = player.AddComponent<SpreadBullets>();
        Destroy(GetComponent<DestroyAfterTimer>());
        playerController.powerupDuration = StartCoroutine(onPickup());
    }

    protected override void Deactivate()
    {
        playerController.spreadBullets = false;
        playerController.playSound(7);
        Destroy(player.GetComponent<SpreadBullets>());
        if(this.gameObject != player)
        {
            Destroy(gameObject);
        }
    }

    protected override IEnumerator onPickup()
    {
        disableGraphics();
        yield return new WaitForSeconds(duration);
        Deactivate();
    }
}
