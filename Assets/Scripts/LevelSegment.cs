﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour {

	public float scrollSpeed;
	public bool levelReady = true;
	public Camera runnerCam;
	private bool scrollScreen = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (scrollScreen) {
			scroll ();
		}
	}

	private void scroll() {
		transform.position = new Vector2 (transform.position.x - (scrollSpeed * Time.deltaTime), transform.position.y);
	}

	public void stopScrolling() {
		scrollScreen = false;
	}

	public void startScrolling() {
		scrollScreen = true;
	}

	public void resetLevel() {
		levelReady = false;
		foreach (GameObject trigger in GameObject.FindGameObjectsWithTag ("ActionTrigger")) {
			trigger.GetComponent<BoxCollider2D> ().enabled = true;
		}
		stopScrolling ();
		StartCoroutine ("ScrollToStart");
	}

	IEnumerator ScrollToStart() {
		while (!Mathf.Approximately(transform.position.x, 0.0f)) {
			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (0f, transform.position.y), 30 * Time.deltaTime);
			yield return null;
		}
		levelReady = true;
	}
}
