using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMode : Powerup
{
    void Awake()
    {
        this.duration = 12f;
    }

    protected override void Activate()
    {
        if(playerController.temporaryImmunity != null) 
        {
            StopCoroutine(playerController.temporaryImmunity);
        }

        playerController.playSound(4);

        playerController.canTakeDamage = false;
        player.GetComponent<Collider2D>().isTrigger = true;
        player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5F);

        playerController.activePowerup = player.AddComponent<GhostMode>();
        Destroy(GetComponent<DestroyAfterTimer>());
        playerController.powerupDuration = StartCoroutine(onPickup());
    }

    protected override void Deactivate()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        player.GetComponent<Collider2D>().isTrigger = false;
        playerController.canTakeDamage = true;
        playerController.playSound(7);
        Destroy(player.GetComponent<GhostMode>());
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
