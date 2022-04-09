using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMode : Powerup
{
    protected override void Activate()
    {
        if(playerController.temporaryImmunity != null) 
        {
            StopCoroutine(playerController.temporaryImmunity);
        }

        playerController.playSound(4);

        playerController.CanTakeDamage = false;
        player.GetComponent<Collider2D>().isTrigger = true;
        player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5F);

        StartCoroutine(onPickup());
    }

    protected override void Deactivate()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        player.GetComponent<Collider2D>().isTrigger = false;
        playerController.CanTakeDamage = true;
    }

    protected override IEnumerator onPickup()
    {
        disableGraphics();
        yield return new WaitForSeconds(duration);
        Deactivate();
        Destroy(gameObject);
    }
}
