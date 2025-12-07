using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);

    void Awake()
    {
        // Initialize lists with null values
        for (int i = 0; i < 6; i++)
        {
            weaponSlots.Add(null);
            passiveItemSlots.Add(null);
        }
        
        // Auto-find and assign UI slots from Canvas BEFORE Start() is called
        FindAndAssignUISlots();
        
        // Disable all empty UI slots at start
        foreach (Image img in weaponUISlots)
        {
            if (img != null)
            {
                img.enabled = false;
            }
        }
        
        foreach (Image img in passiveItemUISlots)
        {
            if (img != null)
            {
                img.enabled = false;
            }
        }
    }

    void FindAndAssignUISlots()
    {
        // Clear existing lists
        weaponUISlots.Clear();
        passiveItemUISlots.Clear();
        
        // Find all Image components in Canvas
        Image[] allImages = FindObjectsOfType<Image>();
        
        // Auto-assign weapon slots (look for "Slot Weapon" in name)
        for (int i = 1; i <= 6; i++)
        {
            Image foundSlot = System.Array.Find(allImages, img => img.gameObject.name == "Slot Weapon " + i);
            weaponUISlots.Add(foundSlot);
            
            if (foundSlot != null)
            {
                Debug.Log($"Found weapon UI slot {i - 1}: {foundSlot.gameObject.name}");
            }
            else
            {
                Debug.LogWarning($"Weapon UI slot {i} not found! Make sure GameObject is named 'Slot Weapon {i}'");
            }
        }
        
        // Auto-assign passive item slots (look for "Slot Passive" in name)
        for (int i = 1; i <= 6; i++)
        {
            Image foundSlot = System.Array.Find(allImages, img => img.gameObject.name == "Slot Passive Weapon " + i);
            passiveItemUISlots.Add(foundSlot);
            
            if (foundSlot != null)
            {
                Debug.Log($"Found passive item UI slot {i - 1}: {foundSlot.gameObject.name}");
            }
            else
            {
                Debug.LogWarning($"Passive item UI slot {i} not found! Make sure GameObject is named 'Slot Passive Weapon {i}'");
            }
        }
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        
        // Set level, if already exists increment, otherwise start at 1
        if (weaponLevels[slotIndex] == 0)
        {
            weaponLevels[slotIndex] = 1;
        }
        else
        {
            weaponLevels[slotIndex]++;
        }
        
        // Update UI
        if (weaponUISlots.Count > slotIndex && weaponUISlots[slotIndex] != null)
        {
            if (weapon.weaponData.Icon != null)
            {
                weaponUISlots[slotIndex].enabled = true;
                weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;
                Debug.Log($"Weapon UI updated: slot {slotIndex}, icon: {weapon.weaponData.Icon.name}");
            }
            else
            {
                Debug.LogWarning($"Weapon in slot {slotIndex} has no Icon in ScriptableObject!");
            }
        }
        else
        {
            Debug.LogWarning($"Weapon UI slot {slotIndex} is not assigned in InventoryManager!");
        }
    }

    public void AddPassiveItem(int slotIndex, PassiveItem item)
    {
        passiveItemSlots[slotIndex] = item;
        
        // Set level, if already exists increment, otherwise start at 1
        if (passiveItemLevels[slotIndex] == 0)
        {
            passiveItemLevels[slotIndex] = 1;
        }
        else
        {
            passiveItemLevels[slotIndex]++;
        }
        
        // Update UI
        if (passiveItemUISlots.Count > slotIndex && passiveItemUISlots[slotIndex] != null)
        {
            if (item.passiveItemData.Icon != null)
            {
                passiveItemUISlots[slotIndex].enabled = true;
                passiveItemUISlots[slotIndex].sprite = item.passiveItemData.Icon;
                Debug.Log($"Passive item UI updated: slot {slotIndex}, icon: {item.passiveItemData.Icon.name}");
            }
            else
            {
                Debug.LogWarning($"Passive item in slot {slotIndex} has no Icon in ScriptableObject!");
            }
        }
        else
        {
            Debug.LogWarning($"Passive item UI slot {slotIndex} is not assigned in InventoryManager!");
        }
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if (weaponSlots[slotIndex] != null)
        {
            WeaponController weapon = weaponSlots[slotIndex];
            
            // Check if there's a next level prefab
            if (weapon.weaponData.NextLevelPrefab != null)
            {
                GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, weapon.transform.position, Quaternion.identity);
                upgradedWeapon.transform.SetParent(weapon.transform.parent);
                
                // Add upgraded weapon to same slot
                WeaponController weaponController = upgradedWeapon.GetComponent<WeaponController>();
                AddWeapon(slotIndex, weaponController);
                
                // Destroy old weapon
                Destroy(weapon.gameObject);
                
                Debug.Log($"Weapon in slot {slotIndex} leveled up to level {weaponLevels[slotIndex]}");
            }
            else
            {
                Debug.LogWarning($"Weapon in slot {slotIndex} has no next level prefab!");
            }
        }
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        if (passiveItemSlots[slotIndex] != null)
        {
            PassiveItem item = passiveItemSlots[slotIndex];
            
            // Check if there's a next level prefab
            if (item.passiveItemData.NextLevelPrefab != null)
            {
                GameObject upgradedItem = Instantiate(item.passiveItemData.NextLevelPrefab, item.transform.position, Quaternion.identity);
                upgradedItem.transform.SetParent(item.transform.parent);
                
                // Add upgraded item to same slot
                PassiveItem passiveItemController = upgradedItem.GetComponent<PassiveItem>();
                AddPassiveItem(slotIndex, passiveItemController);
                
                // Destroy old item
                Destroy(item.gameObject);
                
                Debug.Log($"Passive item in slot {slotIndex} leveled up to level {passiveItemLevels[slotIndex]}");
            }
            else
            {
                Debug.LogWarning($"Passive item in slot {slotIndex} has no next level prefab!");
            }
        }
    }
}
