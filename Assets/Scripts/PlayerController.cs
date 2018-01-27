using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private List<PlayerInput> inputs;
	private int maxInputs { get; set; }
	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		inputs = new List<PlayerInput> ();
		maxInputs = 8;
	}
	
	// Update is called once per frame
	void Update () {
		getPlayerInput ();
	}

	private void getPlayerInput() {
		if (!(inputs.Count >= maxInputs)) {
			if (Input.GetButtonDown ("jump")) {
				inputs.Add (new PlayerInput ("jump"));
				jump ();
			} else if (Input.GetButtonDown ("duck")) {
				inputs.Add (new PlayerInput ("duck"));
			} else if (Input.GetButtonDown ("longjump")) {
				inputs.Add (new PlayerInput ("longjump"));
			} else if (Input.GetButtonDown ("highjump")) {
				inputs.Add (new PlayerInput ("highjump"));
			}
		}
	}

	private void jump() {
		//play jump animation

		rb.AddForce (new Vector2(0.0f, 200.0f));
	}

	private void duck() {

	}

	private void longJump() {
		rb.AddForce (new Vector2(0.0f, 300.0f));
	}

	private void highJump() {

	}
}
