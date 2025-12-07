using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    // Current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 20f;
    Transform player;
    EnemySpawner es; 

    // --- TAMBAHAN UNTUK SHADER ---
    private SpriteRenderer sr; 

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        es = FindObjectOfType<EnemySpawner>();
        // Fix pencarian Player (jika player mati, ini bisa null, jadi hati-hati)
        PlayerStats pStats = FindObjectOfType<PlayerStats>();
        if(pStats != null) player = pStats.transform;

        // Ambil komponen Sprite Renderer agar bisa akses Material
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return; // Safety check

        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        // --- TRIGGER ANIMASI SHADER DISINI ---
        // Panggil efek kedap-kedip setiap kali kena damage
        if (sr != null)
        {
            StartCoroutine(FlashRoutine());
        }

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    // --- LOGIKA ANIMASI SHADER ---
    IEnumerator FlashRoutine()
    {
        // 1. Ubah parameter shader '_FlashAmount' jadi 1 (Merah Full)
        // Kita akses .material untuk membuat instance unik buat musuh ini saja
        sr.material.SetFloat("_FlashAmount", 1f);

        // 2. Tunggu sebentar (0.1 detik)
        yield return new WaitForSeconds(0.1f);

        // 3. Kembalikan ke 0 (Warna Normal)
        sr.material.SetFloat("_FlashAmount", 0f);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = col.gameObject.GetComponent<PlayerStats>();
            if(playerStats != null) playerStats.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        if (es != null)
        {
            es.OnEnemyKilled();
        }
    }

    void ReturnEnemy()
    {
        if (es == null) return;
        Transform randomPoint = es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)];
        transform.position = player.position + randomPoint.localPosition;
    }
}