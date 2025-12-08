using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skrip dasar untuk semua senjata: atur cooldown dan cara menyerang

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData; // Data senjata (damage, speed, cooldown, dll)
    float currentCooldown;

    protected PlayerMovement pm;
    [Header("Attack Input Settings")]
    public bool useManualAttack = true; // Jika true, serang hanya saat klik kiri

    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        if (weaponData == null)
        {
            enabled = false;
            return;
        }
        currentCooldown = weaponData.CooldownDuration;
    }
    
    protected virtual void Update()
    {
        // Hitung mundur cooldown
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }

        // Mode manual: klik kiri memicu serangan jika cooldown sudah selesai
        if (useManualAttack)
        {
            if (currentCooldown <= 0f && Input.GetMouseButton(0))
            {
                Attack();
            }
        }
        else
        {
            // Mode otomatis: serang setiap cooldown selesai
            if (currentCooldown <= 0f)
            {
                Attack();
            }
        }
    }

    protected virtual void Attack(){
        currentCooldown = weaponData.CooldownDuration;
    }
}
