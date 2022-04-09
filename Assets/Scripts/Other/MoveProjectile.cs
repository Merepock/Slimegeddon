using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public float ProjectileSpeed;
    void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * ProjectileSpeed));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

