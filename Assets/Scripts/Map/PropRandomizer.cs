using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints; // Titik-titik tempat prop akan di-spawn
    public List<GameObject> propPrefabs;     // Kumpulan prefab prop yang dipilih secara acak

    void Start()
    {
        SpawnProps(); // Spawn seluruh prop saat mulai
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform; // Jadikan anak dari spawn point agar rapi di hierarchy
        }
    }
}
