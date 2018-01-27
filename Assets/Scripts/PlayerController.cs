using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Queue<PlayerInput> inputs;
	private PlayerInput currentInput;
	private int maxInputs { get; set; }
	private float timeToBeat;

	public float beatInterval;
	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		inputs = new Queue<PlayerInput> ();
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

		rb.AddForce (new Vector2(0.0f, 200.0f));
	}

	private void duck() {
		//TODO play animation

		//TODO reduce hitbox height
	}

	private void longjump() {
		//TODO play animation

		rb.AddForce (new Vector2(0.0f, 300.0f));
	}

	private void highjump() {
		//TODO play animation

		rb.AddForce (new Vector2(0.0f, 300.0f));
	}
}
