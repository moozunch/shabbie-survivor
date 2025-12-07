using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    // Spawn sekali saja saat game mulai (karena Garlic nempel terus)
    protected override void Start()
    {
        base.Start();
        SpawnGarlic();
    }

    protected override void Attack()
    {
        base.Attack();
        // KOSONGKAN BAGIAN INI.
        // Jangan taruh Instantiate di sini. 
        // Kalau ditaruh sini, nanti setiap cooldown selesai dia bakal spawn garlic baru 
        // sampai menumpuk ratusan object.
    }

    void SpawnGarlic()
    {
        // Spawn Prefab
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity, transform);

        // Pastikan posisi nempel di player (Parenting)
        spawnedGarlic.transform.parent = transform;

        // Reset posisi lokal ke 0,0,0 biar pas di tengah player
        spawnedGarlic.transform.localPosition = Vector3.zero;
    }
}