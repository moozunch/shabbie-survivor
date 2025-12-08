using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    // Spawn sekali saja saat game mulai (karena Garlic nempel terus)
    protected override void Start()
    {
        base.Start();
        useManualAttack = false;
        SpawnGarlic();
    }

    protected override void Attack()
    {
        base.Attack();
        
    }

    void SpawnGarlic()
    {
        // Spawn Prefab
        if (weaponData == null)
        {
            return;
        }
        if (weaponData.Prefab == null)
        {
            return;
        }
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity, transform);

        // Pastikan posisi nempel di player (Parenting)
        spawnedGarlic.transform.parent = transform;

        // Reset posisi lokal ke 0,0,0 biar pas di tengah player
        spawnedGarlic.transform.localPosition = Vector3.zero;

        // Ensure it has a trigger collider to register overlaps
        var col2d = spawnedGarlic.GetComponent<Collider2D>();
        if (col2d == null)
        {
            col2d = spawnedGarlic.AddComponent<CircleCollider2D>();
        }
        col2d.isTrigger = true;
    }
}