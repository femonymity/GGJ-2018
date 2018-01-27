using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour {

	public float scrollSpeed;
	public Camera runnerCam;

	private Collider2D coll;
	private Plane[] planes;

	// Use this for initialization
	void Start () {
		planes = GeometryUtility.CalculateFrustumPlanes (runnerCam);
		coll = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		scroll ();

		if (GeometryUtility.TestPlanesAABB (planes, coll.bounds)) {

		} else {
			Destroy (gameObject);
		}
	}

	private void scroll() {
		transform.position = new Vector2 (transform.position.x - (scrollSpeed * Time.deltaTime), transform.position.y);
	}
}
