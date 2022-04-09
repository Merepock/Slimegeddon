using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float duration;
    public GameObject player;
    public PlayerController playerController;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            /*
            If theres already a powerup active
            Get it from the player controller
            Stop its coroutine
            And call its deactivate method
            */
            Activate();
        }
    }
    
    protected void disableGraphics() 
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    protected virtual IEnumerator onPickup() 
    {
        yield return new WaitForSeconds(duration);
    } 

    protected virtual void Activate(){}
    protected virtual void Deactivate(){}
}
