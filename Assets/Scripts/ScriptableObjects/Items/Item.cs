using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject {

    [SerializeField]
    public string itemName;
    [SerializeField]
    public string description;
    [SerializeField]
    public Sprite icon;
    [SerializeField]
    public int value;

}
