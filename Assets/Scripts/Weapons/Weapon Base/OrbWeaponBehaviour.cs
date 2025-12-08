using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BERDIRI SENDIRI, tidak inherit ProjectileWeaponBehaviour
public class OrbWeaponBehaviour : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponScriptableObject weaponData; // Pastikan diisi di Inspector Prefab

    protected PlayerStats player;

    // Statistik saat ini (diambil dari weaponData)
    protected float currentDamage;
    protected float currentSpeed; // Dipakai untuk kecepatan putar
    protected float currentCoolDownDuration;
    protected float currentKnockback; // Besaran dorongan musuh (knockback)

    protected virtual void Awake()
    {
        // Muat data dari ScriptableObject saat game mulai
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
        // Orb bersifat permanen, tidak dihancurkan otomatis
    }

    public float GetCurrentDamage()
    {
        // Damage mengikuti pengganda kekuatan (might) dari pemain
        return currentDamage * player.currentMight;
    }

    // Ambil nilai kecepatan untuk dipakai Controller
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}