using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6); // Slot senjata (maks 6)
    public int[] weaponLevels = new int[6];                                     // Level tiap senjata
    public List<Image> weaponUISlots = new List<Image>(6);                       // UI ikon senjata
    
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);       // Slot item pasif
    public int[] passiveItemLevels = new int[6];                                 // Level item pasif
    public List<Image> passiveItemUISlots = new List<Image>(6);                  // UI ikon item pasif

    void Awake()
    {
        // Inisialisasi list dengan nilai null
        for (int i = 0; i < 6; i++)
        {
            weaponSlots.Add(null);
            passiveItemSlots.Add(null);
        }
        
        // Cari dan assign UI slot dari Canvas sebelum Start() dipanggil
        FindAndAssignUISlots();
        
        // Nonaktifkan semua slot UI yang masih kosong
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
        // Bersihkan list yang ada
        weaponUISlots.Clear();
        passiveItemUISlots.Clear();
        
        // Cari semua komponen Image di Canvas
        Image[] allImages = FindObjectsOfType<Image>();
        
        // Auto-assign slot senjata (cari nama "Slot Weapon X")
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
        
        // Auto-assign slot item pasif (cari nama "Slot Passive Weapon X")
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
        weaponSlots[slotIndex] = weapon; // Simpan referensi senjata ke slot
        
        // Set level: jika belum ada mulai dari 1, jika sudah naikkan
        if (weaponLevels[slotIndex] == 0)
        {
            weaponLevels[slotIndex] = 1;
        }
        else
        {
            weaponLevels[slotIndex]++;
        }
        
        // Update UI ikon senjata
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
        passiveItemSlots[slotIndex] = item; // Simpan referensi item pasif ke slot
        
        // Set level: jika belum ada mulai dari 1, jika sudah naikkan
        if (passiveItemLevels[slotIndex] == 0)
        {
            passiveItemLevels[slotIndex] = 1;
        }
        else
        {
            passiveItemLevels[slotIndex]++;
        }
        
        // Update UI ikon item pasif
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
            
            // Cek apakah ada prefab level berikutnya
            if (weapon.weaponData.NextLevelPrefab != null)
            {
                GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, weapon.transform.position, Quaternion.identity);
                upgradedWeapon.transform.SetParent(weapon.transform.parent);
                
                // Tambahkan senjata yang di-upgrade ke slot yang sama
                WeaponController weaponController = upgradedWeapon.GetComponent<WeaponController>();
                AddWeapon(slotIndex, weaponController);
                
                // Hancurkan senjata lama
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
            
            // Cek apakah ada prefab level berikutnya
            if (item.passiveItemData.NextLevelPrefab != null)
            {
                GameObject upgradedItem = Instantiate(item.passiveItemData.NextLevelPrefab, item.transform.position, Quaternion.identity);
                upgradedItem.transform.SetParent(item.transform.parent);
                
                // Tambahkan item yang di-upgrade ke slot yang sama
                PassiveItem passiveItemController = upgradedItem.GetComponent<PassiveItem>();
                AddPassiveItem(slotIndex, passiveItemController);
                
                // Hancurkan item lama
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
