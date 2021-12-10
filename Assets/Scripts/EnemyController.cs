using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int HitPoints, KillScore;
    public float MoveSpeed;
    public Rigidbody2D enemyRb;
    private LayerMask wall;
    public AudioSource enemyHit;
    Vector2 prevVelocity;

    
    private IEnumerator travelThroughBorder()
    {  
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Wall").GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), false);
        yield break;
    }

    private IEnumerator stuckToWall()
    {
        yield return new WaitForSeconds(0.5f);
        if (enemyRb.IsTouchingLayers(wall) == true)
        {
            Destroy(gameObject);
        }
    }
    

    protected virtual void Start()
    {
        StartCoroutine(travelThroughBorder());
        enemyHit = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody2D>();
        wall = LayerMask.GetMask("Ground");
        enemyRb.velocity = transform.up * MoveSpeed; 
    }

    protected virtual void FixedUpdate()
    {
        prevVelocity = enemyRb.velocity;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if (HitPoints >= 1)
            {
                HitPoints -= 1;
                enemyHit.Play();
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            enemyRb.velocity = Vector2.Reflect(prevVelocity, other.contacts[0].normal);
            transform.up = enemyRb.velocity;
            StartCoroutine(stuckToWall());
        }
    }

    public void AddScore()
    {
        GameObject thePlayer = GameObject.Find("Player");
        PlayerController playerScore = thePlayer.GetComponent<PlayerController>();
        playerScore.score += this.KillScore;
    }
}
