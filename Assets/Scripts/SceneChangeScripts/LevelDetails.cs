using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDetails
{
    public string name;

    [Range(0.1f, 10f)]
    public float difficultyModifier;
    public string music_name;

}
