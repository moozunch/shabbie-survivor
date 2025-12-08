using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : WeaponController
{
    // List untuk menyimpan bola yang sudah dimunculkan
    private List<GameObject> spawnedOrbs = new List<GameObject>();

    // Variable pengaturan orbit
    private float currentAngle = 0f;
    public float rotationSpeed = 150f; // Kecepatan mengelilingi player
    public float orbitRadius = 2.5f;

    // --- TAMBAHAN BARU ---
    [Header("Spin Settings")]
    public float selfRotationSpeed = 360f; // Kecepatan putar kipas (derajat per detik)

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
            // 1. Update Sudut Orbit (Translasi Melingkar)
            currentAngle += rotationSpeed * Time.deltaTime;
            if (currentAngle >= 360f) currentAngle -= 360f;

            // 2. Update Posisi & Rotasi Bola
            UpdateOrbPositions();
        }
    }

    void SpawnOrbs()
    {
        foreach (var orb in spawnedOrbs) { if (orb) Destroy(orb); }
        spawnedOrbs.Clear();

        // Spawn weapon
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
                // --- A. LOGIKA ORBIT (TRANSLASI) ---
                float tempAngle = currentAngle + (angleStep * i);
                float rad = tempAngle * Mathf.Deg2Rad;

                float x = transform.position.x + (orbitRadius * Mathf.Cos(rad));
                float y = transform.position.y + (orbitRadius * Mathf.Sin(rad));

                spawnedOrbs[i].transform.position = new Vector3(x, y, 0);

                // --- B. LOGIKA KIPAS ANGIN (ROTASI MANUAL) ---
                // Vector3.forward artinya sumbu Z (depan), 
                // karena di 2D kita memutar gambar pada sumbu Z.
                spawnedOrbs[i].transform.Rotate(Vector3.forward * selfRotationSpeed * Time.deltaTime);
            }
        }
    }
}