using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    int level;
    public int Level { get => level; }

    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; }

    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; }
}
