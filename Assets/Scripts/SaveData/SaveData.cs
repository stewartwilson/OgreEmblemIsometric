using UnityEngine;
using System.Collections;

[System.Serializable]
public class SaveData
{
    public static SaveData current;
    [SerializeField]
    public int testValue;

    public SaveData()
    {
    }
}

