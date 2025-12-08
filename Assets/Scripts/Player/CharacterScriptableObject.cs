using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]

public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
   public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

   [SerializeField]
   float maxHealth; // HP maksimum karakter
   public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

   [SerializeField]
   float recovery; // Jumlah HP yang dipulihkan per detik
   public float RecoveredHealth { get => recovery; private set => recovery = value; }

   [SerializeField]
   float moveSpeed; // Kecepatan gerak dasar karakter
   public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

   [SerializeField]
   float might; // Pengganda kekuatan serangan (damage multiplier)
   public float Might { get => might; private set => might = value; }

    [SerializeField]
    float projectileSpeed; // Kecepatan proyektil (untuk senjata jarak jauh)
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField]
    float magnet; // Jangkauan pickup (magnet) untuk menarik item
    public float Magnet { get => magnet; private set => magnet = value; }

}