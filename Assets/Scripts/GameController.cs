using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private MusicController musicCon;
	public LevelSegment level;
	public PlayerController player;
	public bool resettingLevel;

	// Use this for initialization
	void Start () {
		musicCon = GameObject.FindGameObjectWithTag ("MusicController").GetComponent<MusicController> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		level = GameObject.FindGameObjectWithTag ("Terrain").GetComponent<LevelSegment> ();
		resettingLevel = false;
		musicCon.startMusic ();
	}
	
	// Update is called once per frame
	void Update () {
		if (resettingLevel && level.levelReady) {
			resettingLevel = false;
			player.spawnPlayer ();
			Invoke ("startPlay", 2.0f);
		}
	}

	public void resetLevel() {
		resettingLevel = true;
		level.resetLevel ();
	}

	public void startPlay() {
		musicCon.restartMusic ();
		level.startScrolling ();
		player.resetTimer ();
	}
}
