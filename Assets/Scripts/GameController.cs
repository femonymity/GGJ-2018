using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private MusicController musicCon;
	public LevelSegment level;
	public PlayerController player;
	public bool resettingLevel;
	private GameObject noteParent;

	// Use this for initialization
	void Start () {
		musicCon = GameObject.FindGameObjectWithTag ("MusicController").GetComponent<MusicController> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		level = GameObject.FindGameObjectWithTag ("Terrain").GetComponent<LevelSegment> ();
		noteParent = GameObject.FindGameObjectWithTag ("NoteParent");
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
		GameObject.FindGameObjectWithTag ("Parallax").GetComponent<BackgroundController> ().resetParallax();
		foreach (Transform child in noteParent.transform) {
			child.GetComponent<NoteMover> ().resetNote ();
		}
		level.resetLevel ();
	}

	public void stopAllNotes() {
		foreach (Transform child in noteParent.transform) {
			child.GetComponent<NoteMover> ().stopNote ();
		}
	}

	public void startPlay() {
		musicCon.restartMusic ();
		level.startScrolling ();
	}
}
