using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BERDIRI SENDIRI, tidak inherit ProjectileWeaponBehaviour
public class OrbWeaponBehaviour : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponScriptableObject weaponData; // Pastikan diisi di Inspector Prefab

    protected PlayerStats player;

    // Current stats (Sama seperti referensi kamu)
    protected float currentDamage;
    protected float currentSpeed; // Ini nanti dipakai untuk kecepatan putar
    protected float currentCoolDownDuration;
    protected float currentKnockback; // Tambahan biar bisa dorong musuh

    protected virtual void Awake()
    {
        // Load data dari Scriptable Object saat game mulai
        if (weaponData != null)
        {
            currentDamage = weaponData.Damage;
            currentSpeed = weaponData.Speed;
            currentCoolDownDuration = weaponData.CooldownDuration;
            // Kita abaikan Pierce karena Orb biasanya infinite pierce
        }
    }

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        // PENTING: Kita HAPUS baris 'Destroy(gameObject)' 
        // karena Orb harus abadi selamanya.
    }

    public float GetCurrentDamage()
    {
        // Rumus damage mengikuti contoh kamu
        return currentDamage * player.currentMight;
    }

    // Getter untuk kecepatan (supaya bisa diambil oleh Controller)
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}