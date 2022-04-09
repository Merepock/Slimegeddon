using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restock : Powerup
{
    protected override IEnumerator onPickup()
    {
        playerController.playSound(3);
        playerController.missileCount += Random.Range(3, 6);
        disableGraphics();
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
