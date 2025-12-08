using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    [HideInInspector]
    public Vector2 moveDir; // Arah gerak saat ini (input WASD), dinormalisasi
    [HideInInspector]
    public float lastHorizontalVector; // Jejak arah horizontal terakhir (kiri/kanan)
    [HideInInspector]
    public float lastVerticalVector; // Jejak arah vertikal terakhir (atas/bawah)
    [HideInInspector]
    public Vector2 lastMovedVector; // Kombinasi arah terakhir, dipakai untuk arah serangan (contoh: Knife)

    // Referensi komponen yang dibutuhkan
    Rigidbody2D rb;
    public CharacterScriptableObject characterData; // Data karakter (kecepatan, dll)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); // Default menghadap ke kanan saat mulai
    }

    void Update()
    {
        InputManagement(); // Baca input dan simpan arah
    }

    void FixedUpdate()
    {
        Move(); // Terapkan gerak di physics step
    }

    void InputManagement()
    {
        // Ambil input mentah (tanpa smoothing) dari WASD/panah
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Buat vektor gerak dan normalisasi agar kecepatan konsisten diagonal
        moveDir = new Vector2(moveX, moveY).normalized;

        // Simpan arah horizontal terakhir jika bergerak kiri/kanan
        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);  // Arah serangan mengikuti sumbu X
        }

        // Simpan arah vertikal terakhir jika bergerak atas/bawah
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);  // Arah serangan mengikuti sumbu Y
        }

        // Jika bergerak diagonal, gabungkan kedua jejak arah
        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        // Terapkan kecepatan ke Rigidbody berdasarkan arah input dan kecepatan dari data karakter
        rb.velocity = new Vector2(moveDir.x * characterData.MoveSpeed, moveDir.y * characterData.MoveSpeed);
    }
}
