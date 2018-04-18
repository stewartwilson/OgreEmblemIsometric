using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileSnap : MonoBehaviour {

    public float xSnap;
    public float ySnap;

    public bool snapSortOrder;

	// Update is called once per frame
	void Update () {
        if (xSnap != 0 && ySnap != 0) {
            foreach (Transform child in transform)
            {
                float xPos = child.position.x;
                float yPos = child.position.y;

                float xMod = xPos % xSnap;
                if (xMod != 0)
                {
                    int rounded = Mathf.RoundToInt(xPos / xSnap);
                    child.position = new Vector2(rounded * xSnap, yPos);
                }
                float yMod = yPos % ySnap;
                if (yMod != 0)
                {
                    int rounded = Mathf.RoundToInt(yPos / ySnap);
                    child.position = new Vector2(xPos, rounded * ySnap);
                }
                if (snapSortOrder) { 
                    int layer = Mathf.RoundToInt(yPos / ySnap);
                    child.GetComponent<SpriteRenderer>().sortingOrder = -layer;
                }
            }
        }
    }
}
