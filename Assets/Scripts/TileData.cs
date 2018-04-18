using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    public GridPosition position;
    public Sprite sprite;
    public bool safeToStand;

    private void Awake()
    {
        if(sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

}
