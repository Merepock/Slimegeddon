﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private AudioSource SFX;
    public AudioClip[] effects;
    public Text ScoreText, HealthText;
    public GameObject GameOverScreen, projectile;
    public int score, health, currScore;
    public float playerSpeed, shootVolume;
    public float fireRate = 0.18f;
    private float nextFire = 0.0f;
    private Rigidbody2D rb2d;
    private bool CanTakeDamage = true;
    public float delayTimer = 2.0f;

    private IEnumerator tempImmune()
    {
        CanTakeDamage = false; //Player will stop taking damage.
        this.GetComponent<SpriteRenderer>().color = new Color(0, 132, 1, 0.5F); //Dark shade of green, turns transparent
        yield return new WaitForSeconds(delayTimer); //Player will be immune to damage for 2 seconds.
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); //Default sprite colours, maximum opacity
        this.GetComponent<Collider2D>().enabled = true;
        CanTakeDamage = true; //Player can take damage again..
        yield break;
    }


    void Start()
    {
        SFX = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        health = 5;
        HealthText.text = "HP: " + health.ToString ();
        score = 0;
    }

    void FixedUpdate()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector2(Horizontal, Vertical) * playerSpeed;
    }

    void Update()
    {
        lookAtMouse();
        if(currScore != score)
        {
            addScoreSound();
            currScore = score;
        }
        ScoreText.text = "Score: " + score.ToString();
        HealthText.text = "HP: " + health.ToString();

        if (health > 0)
        {
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
            {
                shoot(transform.rotation);
            }
        }      

        checkOutOfBounds();
    }

    void checkOutOfBounds()
    {
        Vector2 currentPosition = transform.position;
        if (currentPosition.x < -10)
        {
            Vector2 leftToRight = new Vector2(currentPosition.x + 20, currentPosition.y);
            transform.position = leftToRight;
        }
        if (currentPosition.x > 10)
        {
            Vector2 rightToLeft = new Vector2(currentPosition.x - 20, currentPosition.y);
            transform.position = rightToLeft;
        }
        if (currentPosition.y < -6)
        {
            Vector2 bottomToTop = new Vector2(currentPosition.x, currentPosition.y + 12);
            transform.position = bottomToTop;
        }
        if (currentPosition.y > 6)
        {
            Vector2 topToBottom = new Vector2(currentPosition.x, currentPosition.y - 12);
            transform.position = topToBottom;
        }
    }

    void lookAtMouse()
    {
        Vector3 MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);

        Vector2 direction = new Vector2(MousePos.x - transform.position.x, MousePos.y - transform.position.y);
        transform.up = direction;
    }

    void shoot(Quaternion angle)
    {
        shootSound();
        nextFire = Time.time + fireRate;
        Instantiate(projectile, transform.position, angle);
    }

    void playerHurtSound()
    {
        SFX.clip = effects[0];
        SFX.PlayOneShot(SFX.clip);
    }

    void addScoreSound()
    {
        SFX.clip = effects[1];
        SFX.PlayOneShot(SFX.clip);
    }

    void shootSound()
    {
        SFX.clip = effects[2];
        SFX.volume = shootVolume;
        SFX.PlayOneShot(SFX.clip);
    }

    void healSound()
    {
        SFX.clip = effects[3];
        SFX.PlayOneShot(SFX.clip);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {            
            if (CanTakeDamage == true)
            {
                if (health > 0)
                {
                    health -= 1;
                    playerHurtSound();
                    Destroy(collision.gameObject);
                    this.GetComponent<Collider2D>().enabled = false;
                    StartCoroutine(tempImmune());
                }
                
                if (health == 0)
                {
                    GameOverScreen.SetActive(true); //Display the game over screen.
                    gameObject.GetComponent<SpriteRenderer>().enabled = false; //Makes the player invisible
                }                
            }                      
        } 
        
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            healSound();
            Destroy(collision.gameObject);
            if (health < 5)
            {               
                health += 1;                
            }   
        }
    }
}
