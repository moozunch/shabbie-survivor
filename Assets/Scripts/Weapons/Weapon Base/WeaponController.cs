using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base script for all weapons

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;

    protected PlayerMovement pm;
    [Header("Attack Input Settings")]
    public bool useManualAttack = true; // When true, attack only on input

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
        // Cooldown tick
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }

        // Manual input: Left mouse click triggers attack if cooldown ready
        if (useManualAttack)
        {
            if (currentCooldown <= 0f && Input.GetMouseButton(0))
            {
                Attack();
            }
        }
        else
        {
            // Legacy auto-attack behavior
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
