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

    EnemySpawner es; // Variabel Global

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        // Cache referensi di awal game biar ringan
        es = FindObjectOfType<EnemySpawner>();
        player = FindObjectOfType<PlayerStats>().transform;
    }

    void Update()
    {
        // Cek jarak untuk respawn (looping map)
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
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
            playerStats.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        // FIX ERROR: Cek null dulu. Kalau game stop, es mungkin sudah hilang duluan.
        if (es != null)
        {
            es.OnEnemyKilled();
        }
    }

    void ReturnEnemy()
    {
        // SAFETY CHECK: Pastikan es tidak null sebelum mengakses list spawn points
        if (es == null) return;

        // LOGIC FIX: Gunakan .localPosition, bukan .position
        // Karena kita menambahkan offset relatif terhadap posisi player
        Transform randomPoint = es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)];
        transform.position = player.position + randomPoint.localPosition;
    }
}