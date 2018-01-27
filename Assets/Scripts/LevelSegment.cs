using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour {

	public float scrollSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		scroll ();
	}

	private void scroll() {
		transform.position = new Vector2 (transform.position.x - scrollSpeed, transform.position.y);
	}
}
