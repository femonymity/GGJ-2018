using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Camera runnerCam;
	private MusicController musicCon;
	public LevelSegment level;
	public PlayerController player;
	public bool resettingLevel;
	private GameObject noteParent;
	private BackgroundController parallax;

	// Use this for initialization
	void Start () {
		musicCon = GameObject.FindGameObjectWithTag ("MusicController").GetComponent<MusicController> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		level = GameObject.FindGameObjectWithTag ("LevelParent").GetComponent<LevelSegment> ();
		noteParent = GameObject.FindGameObjectWithTag ("NoteParent");
		parallax = GameObject.FindGameObjectWithTag ("Parallax").GetComponent<BackgroundController> ();
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
		parallax.stopScrolling ();
		parallax.resetParallax ();
		level.resetLevel ();
	}

	public void stopAllNotes() {
		foreach (Transform child in noteParent.transform) {
			child.GetComponent<NoteMover> ().stopNote ();
		}
	}

	public void startAllNotes() {
		foreach (Transform child in noteParent.transform) {
			child.GetComponent<NoteMover> ().startNote ();
		}
	}

	public void stopParallax() {
		parallax.stopScrolling ();
	}

	public void startPlay() {
		musicCon.restartMusic ();
		level.startScrolling ();
		startAllNotes ();
		parallax.startScrolling ();
	}

	public void winGame() {
		parallax.stopScrolling ();
		level.stopScrolling ();
		player.rb.velocity = new Vector2 (7.0f, 0.0f);

		StartCoroutine ("loadOutro");
	}

	IEnumerator loadOutro() {
		//yield return WaitForSeconds (2.0f);
		AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync ("Outro");
		while(!aSyncLoad.isDone) {
			yield return null;
		}
	}
}
