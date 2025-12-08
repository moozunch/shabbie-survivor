using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    Sprite icon; // Ikon untuk UI inventory
    public Sprite Icon { get => icon; private set => icon = value;}

    [SerializeField]
    int level; // Level senjata saat ini
    public int Level { get => level; private set => level = value;}

    [SerializeField]
    GameObject nextLevelPrefab; // Prefab senjata untuk level berikutnya
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value;}

    [SerializeField]
    public GameObject prefab; // Prefab proyektil/behaviour yang di-spawn saat menyerang
    public GameObject Prefab { get => prefab; private set => prefab = value;}

    [SerializeField]
    float damage; // Besaran damage dasar
    public float Damage { get => damage; private set => damage = value;}

    [SerializeField]
    float speed; // Kecepatan proyektil atau animasi serangan
    public float Speed { get => speed; private set => speed = value;}

    [SerializeField]
    float cooldownDuration; // Jeda waktu antar serangan
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value;}  

    [SerializeField]
    int pierce; // Jumlah tembus (berapa musuh yang bisa dilewati proyektil)
    public int Pierce { get => pierce; private set => pierce = value;}
}
