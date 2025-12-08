using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        // Tambah kekuatan serangan (might) pemain berdasarkan multiplier dari SO
        playerStats.currentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}
