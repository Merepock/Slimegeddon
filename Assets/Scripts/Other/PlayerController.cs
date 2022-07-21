using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private AudioSource SFX;
    public AudioClip[] effects;
    public Text scoreText, healthText, missileNumber;
    public GameObject GameOverScreen, projectile, missile, heart;
    public int score, health, currScore, currHealth, missileCount;
    public float playerSpeed, shootVolume, explosionVolume;
    private Rigidbody2D rb2d;
    public bool canTakeDamage, spreadBullets;
    public Coroutine temporaryImmunity = null;
    private Animator anim;
    public Powerup activePowerup = null;

    public IEnumerator tempImmune(float timer)
    {
        canTakeDamage = false;
        this.GetComponent<SpriteRenderer>().color = new Color(0, 132, 1, 0.5F);
        yield return new WaitForSeconds(timer);
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        canTakeDamage = true;
        yield break;
    }


    void Start()
    {
        anim = GetComponent<Animator>();
        SFX = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        health = 5;
        missileCount = 5;
        healthText.text = "Lives: ";
        score = 0;
        canTakeDamage = true;
        spreadBullets = false;
    }

    void FixedUpdate()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector2(Horizontal, Vertical) * playerSpeed;
        if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0) 
        {
            anim.SetBool("isMoving", true);
        }
        else 
        {
            anim.SetBool("isMoving", false);
        }
    }

    void Update()
    {
        lookAtMouse();
        if(currScore != score)
        {
            playSound(1);
            currScore = score;
        }

        if(currHealth != health)
        {
            updateHealthBar();
            currHealth = health;
        }

        scoreText.text = "Score: " + score.ToString();
        missileNumber.text = "X" + missileCount.ToString();

        if (health > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shootBullet(transform.rotation);
            }
            if (Input.GetMouseButtonDown(1) && missileCount > 0)
            {
                shootMissile(transform.rotation);
                missileCount--;
            }
        }

        if (health == 0)
        {
            GameOverScreen.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }       

        checkOutOfBounds();
    }

    void updateHealthBar() 
    {
        foreach (Transform child in healthText.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for(int i = 0; i < health; i++)
        {
            GameObject hitPoint = Instantiate(heart, healthText.transform);
        }
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

    void shootBullet(Quaternion quat)
    {
        if (spreadBullets)
        {
            playSound(8);
            Vector3 currRotation = quat.eulerAngles;
            currRotation -= new Vector3(0, 0, 40);
            for (int i = 0;  i < 5; i++)
            {
                Instantiate(projectile, transform.position, Quaternion.Euler(currRotation));
                currRotation += new Vector3(0, 0, 20);
            }
        }
        else
        {
            playSound(2);
            Instantiate(projectile, transform.position, quat);
        }
        
    }

    void shootMissile(Quaternion quat)
    {
        playSound(6);
        Instantiate(missile, transform.position, quat);
    }

    public void takeDamage()
    {
        health -= 1;
        playSound(0);
        canTakeDamage = false;
        temporaryImmunity = StartCoroutine(tempImmune(2.0f));
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {            
            if (canTakeDamage == true)
            {
                if (health > 0)
                {
                    takeDamage();
                    Destroy(collision.gameObject);
                }              
            }                      
        } 
    }

    public void playSound(int i)
    {
        SFX.clip = effects[i];
        SFX.PlayOneShot(SFX.clip);
    }
    /*
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

    void ghostSound()
    {
        SFX.clip = effects[4];
        SFX.PlayOneShot(SFX.clip);
    }

    void deathSound()
    {
        SFX.clip = effects[5];
        SFX.volume = explosionVolume;
        SFX.PlayOneShot(SFX.clip);
    }

    void missileSound()
    {
        SFX.clip = effects[6];
        SFX.volume = shootVolume;
        SFX.PlayOneShot(SFX.clip);
    }
    */
}

