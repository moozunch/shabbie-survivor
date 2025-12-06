using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    // Current stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    // Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    // class for defining level ranges and experience cap increases
    [System.Serializable]
    public class LevelRange
  {
    public int startLevel;
    public int endLevel;
    public int experienceCapIncrease;
  }

    // i - frame
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invicibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    void Awake()
    {
        // Initialize stats from ScriptableObject
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.RecoveredHealth;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set initial experience cap based on level ranges
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        // Handle invincibility timer
        if (invicibilityTimer > 0)
        {
            invicibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
    }
    

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelChecker();
    }

    void LevelChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;

            // Update experience cap based on new level
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    // Update is called once per frame
    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            currentHealth -= dmg;

            invicibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }

        }

    }

    public void Kill()
    {
        Debug.Log("Player Is DEAD");
    }

    public void RestoreHealth(float amount)
    {
        // only heal the player if their current health is less than max health
        if (currentHealth + amount < characterData.MaxHealth)
        {
            currentHealth += amount;

            // make sure current health does not exceed max health
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
        
    }
}
