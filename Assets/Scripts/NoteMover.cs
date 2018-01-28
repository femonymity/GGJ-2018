using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour {

	public float noteSpeed;
	public bool inZone;

	public bool isInZone () {
		return inZone;
	}

	void OnTriggerEnter2D () {
		Debug.Log ("enter");
		inZone = true;
	}

	void OnTriggerExit2D () {
		inZone = false;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x - (noteSpeed * Time.deltaTime), transform.position.y);
	}
}
