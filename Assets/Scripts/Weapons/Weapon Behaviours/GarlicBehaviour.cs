using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;

    [Header("Cycle Settings")]
    public float maxSize = 3f;        // Ukuran maksimal
    public float growSpeed = 5f;      // Kecepatan membesar
    public float shrinkSpeed = 5f;    // Kecepatan mengecil
    
    public float cooldownDuration = 1.5f; // Jeda waktu saat hilang (sesuai request: 1 detik)

    protected override void Start()
    {
        // BYPASS: Jangan panggil base.Start() biar gak hancur.
        player = FindObjectOfType<PlayerStats>();
        markedEnemies = new List<GameObject>();

        // Mulai siklus hidup Garlic
        StartCoroutine(GarlicRoutine());
    }

    // Coroutine: Cara gampang mengatur urutan kejadian (Story)
    IEnumerator GarlicRoutine()
    {
        while (true) // Loop selamanya (selama game jalan)
        {
            // 1. RESET & MUNCUL (Membesar)
            markedEnemies.Clear(); // Hapus daftar musuh lama biar bisa kena damage lagi
            transform.localScale = Vector3.zero; // Mulai dari 0

            // Loop untuk animasi membesar (0 -> Max)
            while (transform.localScale.x < maxSize)
            {
                transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
                yield return null; // Tunggu frame berikutnya
            }
            transform.localScale = Vector3.one * maxSize; // Pastikan pas di angka max

            

            // 3. MENGHILANG (Mengecil)
            // Loop untuk animasi mengecil (Max -> 0)
            while (transform.localScale.x > 0.1f)
            {
                transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
                yield return null;
            }
            transform.localScale = Vector3.zero; // Pastikan benar-benar 0 (hilang)

            // 4. ISTIRAHAT (Cooldown)
            // Sesuai request: Hilang dulu, tunggu 1 detik, baru ulang
            yield return new WaitForSeconds(cooldownDuration);
        }
    }

    // Logika Tabrakan (Masih sama, cuma nambah cek currentPierce kalau perlu)
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        // Saat ukuran 0 (sedang cooldown), jangan ada damage masuk walau tidak sengaja kesenggol
        if (transform.localScale.x <= 0.1f) return;

        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(GetCurrentDamage());
                markedEnemies.Add(col.gameObject);
            }
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(col.gameObject))
            {
                breakable.TakeDamage(GetCurrentDamage());
                markedEnemies.Add(col.gameObject);
            }
        }
    }
}