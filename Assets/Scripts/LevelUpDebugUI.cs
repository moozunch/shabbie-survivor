using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpDebugUI : MonoBehaviour
{
    InventoryManager inventoryManager;
    PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        inventoryManager = GetComponent<InventoryManager>();
    }

    void Update()
    {
        // Press 1-6 to level up weapons in slots 0-5
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Attempting to level up weapon in slot 0");
            inventoryManager.LevelUpWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Attempting to level up weapon in slot 1");
            inventoryManager.LevelUpWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Attempting to level up weapon in slot 2");
            inventoryManager.LevelUpWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Attempting to level up weapon in slot 3");
            inventoryManager.LevelUpWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Attempting to level up weapon in slot 4");
            inventoryManager.LevelUpWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Attempting to level up weapon in slot 5");
            inventoryManager.LevelUpWeapon(5);
        }

        // Press Q-Y to level up passive items in slots 0-5
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Attempting to level up passive item in slot 0");
            inventoryManager.LevelUpPassiveItem(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Attempting to level up passive item in slot 1");
            inventoryManager.LevelUpPassiveItem(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Attempting to level up passive item in slot 2");
            inventoryManager.LevelUpPassiveItem(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Attempting to level up passive item in slot 3");
            inventoryManager.LevelUpPassiveItem(3);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Attempting to level up passive item in slot 4");
            inventoryManager.LevelUpPassiveItem(4);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Attempting to level up passive item in slot 5");
            inventoryManager.LevelUpPassiveItem(5);
        }
    }
}
