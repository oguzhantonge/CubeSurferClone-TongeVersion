using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelHolder", menuName = "ScriptableObjects/LevelHolder", order = 1)]
public class LevelHolderSO : ScriptableObject
{
    public GameObject[] levels;
}
