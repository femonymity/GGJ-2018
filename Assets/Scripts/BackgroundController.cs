using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	public float startX;
	public float parallaxScrollSpeed;
	public bool scrolling;

	// Use this for initialization
	void Start () {
		startX = transform.position.x;
		scrolling = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (scrolling) {
			parallaxScroll ();
		}
	}

	void parallaxScroll() {
		transform.position = new Vector2 (transform.position.x - (parallaxScrollSpeed * Time.deltaTime), transform.position.y);
	}

	public void startScrolling() {
		scrolling = true;
	}

	public void stopScrolling() {
		scrolling = false;
	}

	public void resetParallax() {
		transform.position = new Vector2 (startX, transform.position.y);
	}
}
