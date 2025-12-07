using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats playerStats;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        // Method ini akan di-override oleh child classes
    }

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }
}
