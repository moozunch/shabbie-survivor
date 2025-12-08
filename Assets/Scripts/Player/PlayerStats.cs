using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData; // Data karakter terpilih (stat dasar)

    // Statistik saat ini (turunan dari CharacterScriptableObject)
    public float currentHealth;            // HP saat ini
    public float currentRecovery;          // Kecepatan pemulihan HP per detik
    public float currentMoveSpeed;         // Kecepatan gerak
    public float currentMight;             // Pengganda damage
    public float currentProjectileSpeed;   // Kecepatan proyektil
    public float currentMagnet;            // Jangkauan magnet pickup

    // Inventaris (senjata dan item pasif)
    public InventoryManager inventoryManager; // Pengelola slot dan UI
    public int weaponIndex;                   // Index slot senjata berikutnya
    public int passiveItemIndex;              // Index slot item pasif berikutnya

    // Pengalaman dan level pemain
    [Header("Experience/Level")]
    public int experience = 0;   // Total EXP saat ini
    public int level = 1;        // Level saat ini
    public int experienceCap;    // Batas EXP untuk naik level

        // Kelas untuk mendefinisikan rentang level dan peningkatan cap EXP
    [System.Serializable]
    public class LevelRange
  {
    public int startLevel;
    public int endLevel;
    public int experienceCapIncrease;
  }

    // I-Frames (invincibility sementara setelah kena hit)
    [Header("I-Frames")]
    public float invincibilityDuration; // Durasi kebal sementara
    float invicibilityTimer;            // Timer kebal yang aktif
    bool isInvincible;                  // Status kebal

    public List<LevelRange> levelRanges;

    void Awake()
    {
        characterData = CharacterSelector.GetData();         // Ambil data karakter dari selector
        CharacterSelector.instance.DestroySingleton();       // Hapus selector agar tidak dobel antar scene

        inventoryManager = GetComponent<InventoryManager>(); // Ambil pengelola inventory

        // Inisialisasi statistik dari ScriptableObject karakter
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.RecoveredHealth;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;

        // Pastikan might minimal 1 agar damage tidak nol
        if (currentMight == 0)
        {
            currentMight = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set EXP cap awal berdasarkan rentang level
        experienceCap = levelRanges[0].experienceCapIncrease;
        
        // Spawn senjata awal setelah InventoryManager siap
        SpawnedWeapon(characterData.StartingWeapon);
        
        // Registrasi otomatis item pasif yang sudah menjadi child Player
        RegisterExistingPassiveItems();
    }

    void Update()
    {
        // Kelola timer invincibility (kebal sementara)
        if (invicibilityTimer > 0)
        {
            invicibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }

        Recover(); // Pemulihan HP pasif
    }
    

    public void IncreaseExperience(int amount)
    {
        experience += amount; // Tambah EXP
        LevelChecker();       // Cek apakah naik level
    }

    void LevelChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;

            // Update batas EXP berdasarkan level baru
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            currentHealth -= dmg;

            invicibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }

        }

    }

    public void Kill()
    {
        Debug.Log("Player Is DEAD");

        // Beritahu GameManager untuk menghentikan permainan
        GameManager.instance.TriggerGameOver();

        // (Opsional) Hancurkan player agar visual hilang
        Destroy(gameObject); 
    }

    public void RestoreHealth(float amount)
    {
        // Pulihkan HP hanya jika belum mencapai maksimum
        if (currentHealth + amount < characterData.MaxHealth)
        {
            currentHealth += amount;

            // Pastikan HP tidak melebihi maksimum
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
        
    }

    void Recover()
    { 
      // Pulihkan HP seiring waktu
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            // Pastikan HP tidak melebihi maksimum
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnedWeapon(GameObject weapon)
    {
        // Cek apakah slot senjata penuh
        if (weaponIndex >= inventoryManager.weaponSlots.Count)
        {
            Debug.LogError("Weapon inventory is full!");
            return;
        }

        // Spawn senjata
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        
        // Tambahkan ke inventory dan UI
        WeaponController weaponController = spawnedWeapon.GetComponent<WeaponController>();
        inventoryManager.AddWeapon(weaponIndex, weaponController);
        
        // Naikkan index untuk senjata berikutnya
        weaponIndex++;
    }

    public void SpawnedPassiveItem(GameObject passiveItem)
    {
        // Cek apakah slot item pasif penuh
        if (passiveItemIndex >= inventoryManager.passiveItemSlots.Count)
        {
            Debug.LogError("Passive item inventory is full!");
            return;
        }

        // Spawn item pasif
        GameObject spawnedItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedItem.transform.SetParent(transform);
        
        // Tambahkan ke inventory dan UI
        PassiveItem passiveItemComponent = spawnedItem.GetComponent<PassiveItem>();
        inventoryManager.AddPassiveItem(passiveItemIndex, passiveItemComponent);
        
        // Naikkan index untuk item berikutnya
        passiveItemIndex++;
    }

    void RegisterExistingPassiveItems()
    {
        // Cari semua item pasif yang sudah menjadi child dari Player
        PassiveItem[] existingItems = GetComponentsInChildren<PassiveItem>();
        
        foreach (PassiveItem item in existingItems)
        {
            if (passiveItemIndex >= inventoryManager.passiveItemSlots.Count)
            {
                Debug.LogError("Passive item inventory is full!");
                break;
            }
            
            // Tambahkan item pasif yang sudah ada ke inventory
            inventoryManager.AddPassiveItem(passiveItemIndex, item);
            passiveItemIndex++;
        }
        
        Debug.Log($"Registered {existingItems.Length} existing passive items to inventory");
    }

    
}