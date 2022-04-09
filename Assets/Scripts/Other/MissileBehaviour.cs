using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public GameObject explosion;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.up * moveSpeed);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject blast = Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0));
            blast.GetComponent<Explosion>().setStats(true, 4.0f);
            Destroy(gameObject);
        }
    }

}
