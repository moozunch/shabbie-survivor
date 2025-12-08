using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;
    
    // Variabel untuk flip sprite (jika pakai scale untuk hadap kiri/kanan)
    private SpriteRenderer sr;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        
        // Cari player (pastikan script PlayerStats ada di player)
        PlayerStats pStats = FindObjectOfType<PlayerStats>();
        if(pStats != null) player = pStats.transform;
        
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        // --- 1. TRANSLASI MANUAL (Pergerakan) ---
        // Rumus: Arah = (Tujuan - Asal).normalized
        Vector3 direction = (player.position - transform.position).normalized;

        // Rumus: Posisi Baru = Posisi Lama + (Arah * Kecepatan * Waktu)
        transform.position += direction * enemy.currentMoveSpeed * Time.deltaTime;


        // --- 2. ROTASI/SKALA MANUAL (Hadap-hadapan) ---

        if (direction.x > 0)
        {
            // Jalan ke Kanan -> Hadap Kanan
            transform.localScale = new Vector3(1, 1, 1); 
        }
        else if (direction.x < 0)
        {
            // Jalan ke Kiri -> Hadap Kiri (Flip X)
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
}