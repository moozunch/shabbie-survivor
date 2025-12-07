using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    // Current stats
    public float currentHealth;
    public float currentRecovery;
    public float currentMoveSpeed;
    public float currentMight;
    public float currentProjectileSpeed;
    public float currentMagnet;

    // Inventory
    public InventoryManager inventoryManager;
    public int weaponIndex;
    public int passiveItemIndex;

    // Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    // class for defining level ranges and experience cap increases
    [System.Serializable]
    public class LevelRange
  {
    public int startLevel;
    public int endLevel;
    public int experienceCapIncrease;
  }

    // i - frame
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invicibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventoryManager = GetComponent<InventoryManager>();

        // Initialize stats from ScriptableObject
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.RecoveredHealth;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;

        // Ensure currentMight is at least 1
        if (currentMight == 0)
        {
            currentMight = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set initial experience cap based on level ranges
        experienceCap = levelRanges[0].experienceCapIncrease;
        
        // Spawn starting weapon after InventoryManager is initialized
        SpawnedWeapon(characterData.StartingWeapon);
        
        // Auto-register existing passive items that are already children of Player
        RegisterExistingPassiveItems();
    }

    void Update()
    {
        // Handle invincibility timer
        if (invicibilityTimer > 0)
        {
            invicibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }

        Recover();
    }
    

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelChecker();
    }

    void LevelChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;

            // Update experience cap based on new level
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

    // Update is called once per frame
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
    }

    public void RestoreHealth(float amount)
    {
        // only heal the player if their current health is less than max health
        if (currentHealth + amount < characterData.MaxHealth)
        {
            currentHealth += amount;

            // make sure current health does not exceed max health
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
        
    }

    void Recover()
    { 
      // recover health over time
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            // make sure current health does not exceed max health
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnedWeapon(GameObject weapon)
    {
        // Check if weapon slots are full
        if (weaponIndex >= inventoryManager.weaponSlots.Count)
        {
            Debug.LogError("Weapon inventory is full!");
            return;
        }

        // Spawn weapon
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        
        // Add to inventory
        WeaponController weaponController = spawnedWeapon.GetComponent<WeaponController>();
        inventoryManager.AddWeapon(weaponIndex, weaponController);
        
        // Increment weapon index for next weapon
        weaponIndex++;
    }

    public void SpawnedPassiveItem(GameObject passiveItem)
    {
        // Check if passive item slots are full
        if (passiveItemIndex >= inventoryManager.passiveItemSlots.Count)
        {
            Debug.LogError("Passive item inventory is full!");
            return;
        }

        // Spawn passive item
        GameObject spawnedItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedItem.transform.SetParent(transform);
        
        // Add to inventory
        PassiveItem passiveItemComponent = spawnedItem.GetComponent<PassiveItem>();
        inventoryManager.AddPassiveItem(passiveItemIndex, passiveItemComponent);
        
        // Increment passive item index for next item
        passiveItemIndex++;
    }

    void RegisterExistingPassiveItems()
    {
        // Find all passive items that are children of Player
        PassiveItem[] existingItems = GetComponentsInChildren<PassiveItem>();
        
        foreach (PassiveItem item in existingItems)
        {
            if (passiveItemIndex >= inventoryManager.passiveItemSlots.Count)
            {
                Debug.LogError("Passive item inventory is full!");
                break;
            }
            
            // Add existing passive item to inventory
            inventoryManager.AddPassiveItem(passiveItemIndex, item);
            passiveItemIndex++;
        }
        
        Debug.Log($"Registered {existingItems.Length} existing passive items to inventory");
    }

 }
