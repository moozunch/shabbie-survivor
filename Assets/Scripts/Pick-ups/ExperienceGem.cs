using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup, ICollectible
{
   public int experienceGranted;

   public void Collect()
   {
       // Saat diambil, berikan EXP ke pemain
       PlayerStats player = FindObjectOfType<PlayerStats>();
       player.IncreaseExperience(experienceGranted);
   }
}
