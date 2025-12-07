using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];

    void Awake()
    {
        // Initialize lists with null values
        for (int i = 0; i < 6; i++)
        {
            weaponSlots.Add(null);
            passiveItemSlots.Add(null);
        }
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = 1;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem item)
    {
        passiveItemSlots[slotIndex] = item;
        passiveItemLevels[slotIndex] = 1;
        Debug.Log($"Passive item added to slot {slotIndex}");
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
