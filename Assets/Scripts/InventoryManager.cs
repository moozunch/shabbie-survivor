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
        // Logic untuk level up weapon akan diimplementasikan nanti
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        // Logic untuk level up passive item akan diimplementasikan nanti
    }
}
