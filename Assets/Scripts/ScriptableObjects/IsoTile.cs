﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IsoTile : ScriptableObject {
    [SerializeField]
    public GroundType groundType;
    [SerializeField]
    public int elevation;
    [SerializeField]
    public int x;
    [SerializeField]
    public int y;
    [SerializeField]
    bool safeToStand;

    public void createTileFromArray(int[] data)
    {
        if(data.Length == 5)
        {
            groundType = (GroundType)data[0];
            x = data[1];
            y = data[2];
            elevation = data[3];
            if (data[4] == 0)
                safeToStand = false;
            else
                safeToStand = true;
        }
    }

}
