using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        // Tambah kecepatan gerak pemain berdasarkan multiplier dari SO
        playerStats.currentMoveSpeed *= 1 + passiveItemData.Multiplier / 100f;
    }
}
