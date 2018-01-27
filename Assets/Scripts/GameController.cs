using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Queue<LevelSegment> levelSegments;
	public LevelSegment newestSegment;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (newestSegment.segmentEdgeToScreenEdge () < 100) {
			newestSegment = levelSegments.Dequeue ();
			newestSegment.gameObject.SetActive (true);
		}
		*/
	}
}
