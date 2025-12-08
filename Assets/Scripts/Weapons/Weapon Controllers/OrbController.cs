using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : WeaponController
{
    // List untuk menyimpan orb yang sudah dimunculkan
    private List<GameObject> spawnedOrbs = new List<GameObject>();

    // Pengaturan orbit
    private float currentAngle = 0f;
    public float rotationSpeed = 150f; // Kecepatan orb mengelilingi pemain
    public float orbitRadius = 2.5f;

    // Putaran visual orb
    [Header("Spin Settings")]
    public float selfRotationSpeed = 360f; // Kecepatan putar visual orb (derajat per detik)

    protected override void Start()
    {
        base.Start();
        useManualAttack = false;
        SpawnOrbs();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    void Update()
    {
        if (spawnedOrbs.Count > 0)
        {
            // 1. Update sudut orbit (gerak melingkar)
            currentAngle += rotationSpeed * Time.deltaTime;
            if (currentAngle >= 360f) currentAngle -= 360f;

            // 2. Update posisi & rotasi orb
            UpdateOrbPositions();
        }
    }

    void SpawnOrbs()
    {
        foreach (var orb in spawnedOrbs) { if (orb) Destroy(orb); }
        spawnedOrbs.Clear();

        // Spawn satu orb dan simpan referensinya
        GameObject spawnedOrb = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity, transform);
        spawnedOrbs.Add(spawnedOrb);
    }

    void UpdateOrbPositions()
    {
        float angleStep = 360f / spawnedOrbs.Count;

        for (int i = 0; i < spawnedOrbs.Count; i++)
        {
            if (spawnedOrbs[i] != null)
            {
                // --- A. Logika orbit (translasi) ---
                float tempAngle = currentAngle + (angleStep * i);
                float rad = tempAngle * Mathf.Deg2Rad;

                float x = transform.position.x + (orbitRadius * Mathf.Cos(rad));
                float y = transform.position.y + (orbitRadius * Mathf.Sin(rad));

                spawnedOrbs[i].transform.position = new Vector3(x, y, 0);

                // --- B. Rotasi visual orb (manual) ---
                // Vector3.forward adalah sumbu Z yang kita pakai di 2D untuk memutar sprite
                spawnedOrbs[i].transform.Rotate(Vector3.forward * selfRotationSpeed * Time.deltaTime);
            }
        }
    }
}