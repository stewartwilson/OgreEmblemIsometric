using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTile {
    [SerializeField]
    public GroundType groundType;
    [SerializeField]
    public GridPosition position;
    [SerializeField]
    public bool safeToStand;

    public MapTile(int[] data)
    {
        if(data.Length == 5)
        {
            groundType = (GroundType)data[0];
            position.x = data[1];
            position.y = data[2];
            position.elevation = data[3];
            if (data[4] == 0)
                safeToStand = false;
            else
                safeToStand = true;
        }
    }

}
