using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Pickup, ICollectible
{
    public int healthRestored;
    
    public void Collect()
    {
         // Saat diambil, pulihkan HP pemain sejumlah yang ditentukan
         PlayerStats player = FindObjectOfType<PlayerStats>();
         player.RestoreHealth(healthRestored);
    }
}
