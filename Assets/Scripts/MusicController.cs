using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource deathSource;

	// Use this for initialization
	void Start () {

	}
	
	public void startMusic() {
		musicSource.Play ();
	}

	public void restartMusic() {
		musicSource.time = 0.0f;
		musicSource.Play ();
	}

	public void pauseMusic() {
		musicSource.Pause ();
	}

	public void playDeathSound() {
		deathSource.time = 0.0f;
		deathSource.Play ();
	}
}
