using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;        // Akses statistik musuh (kecepatan, HP, damage)
    Transform player;       // Target posisi pemain untuk dikejar

    void Start()
    {
        // Ambil komponen EnemyStats di GameObject ini
        enemy = GetComponent<EnemyStats>();
        // Cari Player di scene lalu ambil Transform-nya untuk dijadikan target
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        // Gerakkan musuh mendekati pemain dengan kecepatan saat ini
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.transform.position,
            enemy.currentMoveSpeed * Time.deltaTime
        );
    }
}
