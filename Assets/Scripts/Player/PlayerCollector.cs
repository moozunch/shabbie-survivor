using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;                 // Referensi ke stats pemain
    CircleCollider2D playerCollector;   // Area magnet untuk menarik item
    public float pullSpeed;             // Kecepatan tarikan item menuju pemain

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>(); // Pastikan collider di-set IsTrigger
    }

    void Update()
    {
        playerCollector.radius = player.currentMagnet; // Sesuaikan radius dengan stat magnet pemain
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.TryGetComponent(out ICollectible collectible))
       {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forcedDirection = (transform.position - col.transform.position).normalized;
                rb.AddForce(forcedDirection * pullSpeed); // Dorong item ke arah pemain

            collectible.Collect();
       }
    }
}
