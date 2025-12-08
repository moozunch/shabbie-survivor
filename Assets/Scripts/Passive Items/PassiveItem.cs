using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats playerStats;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        // Method ini di-override oleh turunan untuk terapkan efek (contoh: speed, damage)
    }

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>(); // Ambil referensi ke pemain
    }

    void Start()
    {
        ApplyModifier(); // Terapkan efek saat item aktif
    }
}
