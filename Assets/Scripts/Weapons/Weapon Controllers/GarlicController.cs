using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    // Spawn sekali saja saat game mulai (garlic menempel di pemain)
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
        // Spawn prefab garlic dari ScriptableObject
        if (weaponData == null)
        {
            return;
        }
        if (weaponData.Prefab == null)
        {
            return;
        }
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity, transform);

        // Pastikan posisi menempel di player (parenting)
        spawnedGarlic.transform.parent = transform;

        // Reset posisi lokal ke 0,0,0 agar berada di tengah pemain
        spawnedGarlic.transform.localPosition = Vector3.zero;

        // Pastikan memiliki Collider2D trigger agar tabrakan terdeteksi
        var col2d = spawnedGarlic.GetComponent<Collider2D>();
        if (col2d == null)
        {
            col2d = spawnedGarlic.AddComponent<CircleCollider2D>();
        }
        col2d.isTrigger = true;
    }
}