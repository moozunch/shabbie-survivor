using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; 
        public int waveQuota; 
        public float spawnInterval; 
        public int spawnCount; 
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; 
        public int spawnCount; // UBAH: float ke int biar konsisten
        public GameObject enemyPrefabs; 
    }

    public List<Wave> waves; // List ini akan diisi otomatis oleh script
    public int currentWaveCount; 

    [Header("Procedural Settings")]
    public GameObject enemyPrefab; // DRAG PREFAB BAT KE SINI
    public int totalWaves = 10; // Total wave yang diinginkan
    public int baseEnemyCount = 10; // Jumlah musuh di wave 1
    public float enemyMultiplier = 1.2f; // Tiap wave musuh dikali 1.2
    public float baseSpawnInterval = 1f; // Jeda antar spawn musuh

    [Header("Spawner Attributes")]
    float spawnTimer; 
    public int enemiesAlive; 
    public int maxEnemiesAllowed = 100; 
    public bool maxEnemiesReached = false;
    public float waveInterval = 3f; // Jeda antar wave
    bool isWaveTransitioning = false; // Flag biar gak error double call next wave

    [Header("Spawn Position")]
    public List<Transform> relativeSpawnPoints; 
    Transform player;

    void Start()
    {
        // 1. Cari Player
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            player = playerStats.transform;
        }
        else
        {
            Debug.LogError("Player tidak ditemukan! Pastikan ada PlayerStats di scene.");
            return; 
        }

        // 2. Cek Spawn Points (Biar gak error kalau kosong)
        if (relativeSpawnPoints == null || relativeSpawnPoints.Count == 0)
        {
            GenerateAutoSpawnPoints();
        }

        // 3. Generate Waves secara Prosedural
        GenerateProceduralWaves();

        // 4. Hitung Quota Wave 1
        CalculateWaveQuota();
    }

    // Fungsi baru untuk membuat data wave secara otomatis
    void GenerateProceduralWaves()
    {
        waves = new List<Wave>(); // Reset list

        for (int i = 0; i < totalWaves; i++)
        {
            Wave newWave = new Wave();
            newWave.waveName = "Wave " + (i + 1);
            newWave.spawnInterval = baseSpawnInterval;
            newWave.spawnCount = 0;

            // Buat grup musuh (Bat)
            EnemyGroup newGroup = new EnemyGroup();
            newGroup.enemyName = enemyPrefab.name;
            newGroup.enemyPrefabs = enemyPrefab;
            newGroup.spawnCount = 0;

            // Rumus kenaikan jumlah musuh: Base * (Multiplier pangkat Wave)
            // Contoh: Wave 0 = 10, Wave 1 = 12, Wave 2 = 14, dst.
            newGroup.enemyCount = Mathf.RoundToInt(baseEnemyCount * Mathf.Pow(enemyMultiplier, i));

            newWave.enemyGroups = new List<EnemyGroup>();
            newWave.enemyGroups.Add(newGroup);

            waves.Add(newWave);
        }
    }

    // Fungsi biar gak error kalau lupa naruh SpawnPoint di Inspector
    void GenerateAutoSpawnPoints()
    {
        relativeSpawnPoints = new List<Transform>();
        GameObject spawnParent = new GameObject("AutoSpawnPoints");
        spawnParent.transform.parent = transform;

        // Bikin 8 titik melingkar di sekitar player
        int points = 8;
        float radius = 10f; // Jarak dari player
        for (int i = 0; i < points; i++)
        {
            float angle = i * Mathf.PI * 2 / points;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            
            GameObject p = new GameObject("Point " + i);
            p.transform.parent = spawnParent.transform;
            p.transform.localPosition = pos;
            relativeSpawnPoints.Add(p.transform);
        }
    }

    void Update()
    {
        // Cek apakah wave selesai (Spawn sudah penuh DAN Quota terpenuhi)
        // Dan pastikan tidak sedang transisi ke wave berikutnya
        if (currentWaveCount < waves.Count 
            && waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota 
            && !isWaveTransitioning)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemeies();
        }
    }

    IEnumerator BeginNextWave()
    {
        isWaveTransitioning = true; // Kunci biar gak kepanggil berkali-kali

        yield return new WaitForSeconds(waveInterval); 

        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++; 
            CalculateWaveQuota(); 
            Debug.Log("Starting Wave: " + (currentWaveCount + 1));
        }
        
        isWaveTransitioning = false; // Buka kunci
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0; 
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.Log("Wave " + (currentWaveCount+1) + " Quota: " + currentWaveQuota);
    }

    void SpawnEnemeies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return; 
                    }
                    else
                    {
                        maxEnemiesReached = false;
                    }

                    // Pilih posisi random dari list spawn point
                    Transform randomPoint = relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)];
                    
                    // Spawn Musuh
                    Instantiate(enemyGroup.enemyPrefabs, player.position + randomPoint.localPosition, Quaternion.identity);

                    enemyGroup.spawnCount++; 
                    waves[currentWaveCount].spawnCount++; 
                    enemiesAlive++; 
                    
                    // Break biar gak spawn semua tipe sekaligus dalam 1 frame (opsional)
                    break; 
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--; 
    }
}