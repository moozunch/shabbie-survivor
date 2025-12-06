using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, ICollectible
{
    public int healthRestored;
    
    public void Collect()
    {
         PlayerStats player = FindObjectOfType<PlayerStats>();
         player.RestoreHealth(healthRestored);
         Destroy(gameObject);
    }

}
