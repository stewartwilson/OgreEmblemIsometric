using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    public GameObject objectInTile;

    void OnTriggerEnter2D(Collider2D other)
    {
        objectInTile = other.gameObject;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        objectInTile = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        objectInTile = null;
    }
}
