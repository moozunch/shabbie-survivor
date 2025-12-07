using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; // A list og froup of enemies to spawn in this wave
        public int waveQuota; //the total number of enemies to spawn in this wave
        public float spawnInterval; //the interval at which to spawn enemies
        public int spawnCount; //the number of enemies spawned so far in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //number of each enemy type in this group
        public float spawnCount;
        public GameObject enemyPrefabs; //list of enemy prefabs in this group
    }

    public List<Wave> waves; //list of waves in the game
    public int currentWaveCount; //index of the current wave, iingat selalu mulati dari 0

    Transform player;

    [Header("Spawner Attributes")]
    float spawnTimer; //timer to track spawn intervals, when to spawn next enemy
    public int enemiesAlive; //to check if there are enemies alive
    public int maxEnemiesAllowed; //max number of enemies allowed to be alive at once
    public bool maxEnemiesReached = false;
    public float waveInterval; //interval between waves


    [Header("Spawn Position")]
    public List<Transform> relativeSpawnPoints; //a list to store all the relative spawn points to the player


    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) //check if the current wave is completedn and next wave should start
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        //check if it's time to spawn enemies based on the spawn interval
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f; //reset the timer after spawning enemies
            SpawnEnemeies();
        }
    }

    //couroutine to begin the next wave after a delay, gunanya asynchronous
    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval); //wait for the specified interval before starting the next wave

        //If there are more waves to spawn after the current wave, move on to the next wave
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++; //increment the wave count to move to the next wave
            CalculateWaveQuota(); //recalculate the wave quota for the new wave
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0; //reset to 0
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }


    //this method will stop spawning enemies when the max number of enemies alive is reached
    //the method will only spawn enemies in a particular wave until it is time for the next wave's enemies to spawn
    void SpawnEnemeies()
    {
        //check if the minmum number of enemies for the wave has been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //checked if the minimum number of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return; //exit the function if the max number of enemies is reached
                    }
                    else
                    {
                        maxEnemiesReached = false;
                    }

                    //spawn the enemy at a random spawn point but relative close to player
                    Instantiate(enemyGroup.enemyPrefabs, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++; //increment the spawn count for this enemy type to keep track emang udah bertambah
                    waves[currentWaveCount].spawnCount++; //increment the total spawn count for the wave
                    enemiesAlive++; //increment the total number of enemies alive in the scene
                }
            }
        }

        //reset maxEnemiesReached flag if the number of enemies alive is below the maximum limit
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    //call this  method when an enemy is killed to decrement the number of enemies alive
    public void OnEnemyKilled()
    {
        enemiesAlive--; //decrement the number of enemies alive when an enemy is killed
    }

}
