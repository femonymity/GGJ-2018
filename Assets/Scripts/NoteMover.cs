using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour {

	public float noteSpeed;
	public bool inZone;
	public bool scrolling;
	public string inputName;
	public float startX;
	public bool isActive = true;

	void Start() {
		startX = transform.position.x;
		scrolling = true;
	}

	public bool isInZone () {
		return inZone;
	}

	public void startNote() {
		scrolling = true;
	}

	public void stopNote() {
		scrolling = false;
	}

	public void resetNote() {
		GetComponent<SpriteRenderer> ().enabled = true;
		transform.position = new Vector2 (startX, transform.position.y);
		inZone = false;
		isActive = true;
	}

	void OnTriggerEnter2D () {
		inZone = true;
	}

	void OnTriggerExit2D () {
		inZone = false;
	}

	// Update is called once per frame
	void Update () {
		if (scrolling) {
			transform.position = new Vector2 (transform.position.x - (noteSpeed * Time.deltaTime), transform.position.y);
		}
	}
}
