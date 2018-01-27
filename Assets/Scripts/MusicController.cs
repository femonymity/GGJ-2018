using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	private AudioSource source;

	public AudioClip music;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		source.clip = music;
	}
	
	public void startMusic() {
		source.Play ();
	}

	public void restartMusic() {
		source.time = 0.0f;
		source.Play ();
	}

	public void pauseMusic() {
		source.Pause ();
	}
}
