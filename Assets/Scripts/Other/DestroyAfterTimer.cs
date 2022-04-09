using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
    public float stayTimer;
    private SpriteRenderer sprite;

    void Update()
    {
        stayTimer -= Time.deltaTime;
        if (stayTimer <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
