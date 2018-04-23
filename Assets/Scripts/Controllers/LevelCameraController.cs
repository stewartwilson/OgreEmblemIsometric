using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCameraController : MonoBehaviour {

    public float xSpeed = 1;
    public float ySpeed = 1;
	
	// Update is called once per frame
	void Update () {
        float vertical = Input.GetAxis("Camera Y");
        float horizontal = Input.GetAxis("Camera X");

        transform.Translate(new Vector2(horizontal * xSpeed * Time.deltaTime, vertical * ySpeed * Time.deltaTime));
    }
}
