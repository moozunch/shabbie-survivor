using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData; // Data senjata dari ScriptableObject
    protected PlayerStats player;             // Referensi ke pemain untuk ambil nilai might

    public float destroyAfterSeconds;         // Durasi sebelum objek senjata dihapus

    // Statistik saat ini (turunan dari weaponData)
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        // Inisialisasi nilai dari ScriptableObject
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    public float GetCurrentDamage()
    {
        // Damage mengikuti nilai might dari pemain (pengganda kekuatan)
        return currentDamage * player.currentMight;
    }

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        Destroy(gameObject, destroyAfterSeconds); // Hapus objek melee sesuai durasi
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        // Serang musuh saat bersentuhan (trigger)
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
        }
        // Hancurkan properti yang bisa pecah
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
            }
        }
    }
}
