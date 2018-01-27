﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Queue<PlayerInput> inputs;
	private PlayerInput currentInput;
	private GameController gameCon;
	private int maxInputs { get; set; }
	private float timeToBeat;
	private float jumpTime;
	private float airTime;

	private bool hanging = false;
	private bool jumping = false;
	private bool falling = false;

	public float beatInterval;
	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		inputs = new Queue<PlayerInput> ();
		gameCon = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		maxInputs = 8;
		timeToBeat = beatInterval;
	}

	// Update is called once per frame
	void Update () {
		timeToBeat -= Time.deltaTime;
		getPlayerInput ();

		if (timeToBeat <= 0) {
			if (inputs.Count > 0) {
				currentInput = inputs.Dequeue ();
				Invoke (currentInput.inputName, 0.0f);
			}

			float diff = timeToBeat * -1;
			timeToBeat = beatInterval + diff;
		}

		if (jumping) {
			jumpTime -= Time.deltaTime;
			if (jumpTime <= 0.0f) {
				jumping = false;
				hanging = true;
				rb.velocity = new Vector2 (rb.velocity.x, 0.0f);
			}
		} else if (hanging) {
			airTime -= Time.deltaTime;
			if (airTime <= 0.0f) {
				hanging = false;
				falling = true;
				rb.velocity = new Vector2 (rb.velocity.x, -8.0f);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Obstacle") {
			killPlayer ();
		} else if (other.gameObject.tag == "Terrain" && falling) {
			rb.velocity = new Vector2 (rb.velocity.x, 0.0f);
			falling = false;
		}
	}

	private void getPlayerInput() {
		if (!(inputs.Count >= maxInputs)) {
			if (Input.GetButtonDown ("jump")) {
				inputs.Enqueue (new PlayerInput ("jump"));
			} else if (Input.GetButtonDown ("duck")) {
				inputs.Enqueue (new PlayerInput ("duck"));
			} else if (Input.GetButtonDown ("longjump")) {
				inputs.Enqueue (new PlayerInput ("longjump"));
			} else if (Input.GetButtonDown ("highjump")) {
				inputs.Enqueue (new PlayerInput ("highjump"));
			}
		}
	}

	private void jump() {
		//TODO play animation

		jumping = true;
		jumpTime = 0.25f;
		airTime = 0.1f;
		rb.velocity = new Vector2 (rb.velocity.x, 12.0f);
	}

	private void duck() {
		//TODO play animation

		//TODO reduce hitbox height
	}

	private void longjump() {
		//TODO play animation

		jumping = true;
		jumpTime = 0.15f;
		airTime = 0.3f;
	}

	private void highjump() {
		//TODO play animation

		jumping = true;
		jumpTime = 0.35f;
		airTime = 0.05f;
	}

	public void spawnPlayer() {
		//TODO

	}

	private void killPlayer() {
		GameObject.FindGameObjectWithTag ("MusicController").GetComponent<MusicController>().pauseMusic();
		//TODO play player death animation
		//animation should call endOfPlayerDeath on last frame

	}

	public void endOfPlayerDeath() {
		gameCon.resetLevel ();
	}

	public void disablePlayer() {
		gameObject.SetActive (false);
	}

	public void enablePlayer() {
		gameObject.SetActive (true);
	}
}
