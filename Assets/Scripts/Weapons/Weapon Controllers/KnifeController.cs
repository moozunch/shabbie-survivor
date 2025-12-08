using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{

    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Attack()
    {
        base.Attack();
        if (weaponData == null)
        {
            return;
        }
        if (weaponData.Prefab == null)
        {
            return;
        }
        // Spawn prefab pisau di posisi pemain
        GameObject spawnedKnife = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity);
        var kb = spawnedKnife.GetComponent<KnifeBehaviour>();
        if (kb == null)
        {
            return;
        }
        // Pastikan proyektil memiliki Collider2D trigger agar tabrakan terdeteksi
        var col2d = spawnedKnife.GetComponent<Collider2D>();
        if (col2d == null)
        {
            col2d = spawnedKnife.AddComponent<BoxCollider2D>();
        }
        col2d.isTrigger = true;
        // Arahkan pisau mengikuti arah gerak terakhir pemain
        kb.DirectionChecker(pm.lastMovedVector);

    }
}
