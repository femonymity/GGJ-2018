using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	public Transform startPos;
	public float parallaxScrollSpeed;

	// Use this for initialization
	void Start () {
		startPos = transform;
	}
	
	// Update is called once per frame
	void Update () {
		parallaxScroll ();
	}

	void parallaxScroll() {
		transform.position = new Vector2 (transform.position.x - (parallaxScrollSpeed * Time.deltaTime), transform.position.y);
	}

	public void resetParallax() {
		transform.position = startPos.position;
	}
}
