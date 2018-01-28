using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Queue<PlayerInput> correctInputs;
	private PlayerInput currentInput;
	private GameController gameCon;
	private Animator anim;
	private float timeToBeat;
	private float jumpTime;
	private float airTime;

	private bool hanging = false;
	private bool jumping = false;
	private bool falling = false;

	public Vector2 startLocation;
	public float inputDelay = 2.0f;
	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		correctInputs = new Queue<PlayerInput> ();
		gameCon = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		anim = GetComponent<Animator> ();

		startLocation = new Vector2 (-9.5f, -1.2f);
	}

	// Update is called once per frame
	void Update () {
		getPlayerInput ();

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

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Obstacle") {
			killPlayer ();
		} else if (other.gameObject.tag == "Terrain" && falling) {
			rb.velocity = new Vector2 (rb.velocity.x, 0.0f);
			falling = false;
		} else if (other.gameObject.tag == "ActionTrigger") {
			Debug.Log ("trigger action");
			Debug.Log (other.gameObject.name);
			PlayerInput nextInput = correctInputs.Dequeue ();
			Invoke (nextInput.inputName , 0.0f);
		}
	}

	private void getPlayerInput() {
		bool correctInput = false;
		string inputName = "";
		if (Input.GetButtonDown ("jump")) {
			inputName = "jump";
			correctInput = addCorrectInput (inputName);
		} else if (Input.GetButtonDown ("duck")) {
			inputName = "duck";
			correctInput = addCorrectInput (inputName);
		} else if (Input.GetButtonDown ("longjump")) {
			inputName = "longjump";
			correctInput = addCorrectInput (inputName);
		} else if (Input.GetButtonDown ("highjump")) {
			inputName = "highjump";
			correctInput = addCorrectInput (inputName);
		}

		if ((inputName != string.Empty) && !correctInput) {
			Debug.Log ("Wrong");
			Invoke (inputName, inputDelay);
		}
	}

	private bool addCorrectInput(string inputName) {
		bool success = false;
		GameObject[] notes = GameObject.FindGameObjectsWithTag ("Note");
		foreach (GameObject note in notes) {
			if (note.GetComponent<NoteMover> ().isInZone ()) {
				correctInputs.Enqueue (new PlayerInput (inputName));
				success = true;
				Destroy (note);
				Debug.Log ("Right");
			}
		}
		return success;
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
		jumpTime = 0.2f;
		airTime = 0.7f;
		rb.velocity = new Vector2 (rb.velocity.x, 12.0f);
	}

	private void highjump() {
		//TODO play animation

		jumping = true;
		jumpTime = 0.35f;
		airTime = 0.05f;
		rb.velocity = new Vector2 (rb.velocity.x, 12.0f);
	}

	public void spawnPlayer() {
		//TODO
		rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
		transform.position = startLocation;
		enablePlayer();
		GetComponent<SpriteRenderer> ().enabled = true;
		Debug.Log ("player spawned");
	}

	private void killPlayer() {
		rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
		MusicController musicCon = GameObject.FindGameObjectWithTag ("MusicController").GetComponent<MusicController> ();
		musicCon.pauseMusic();
		musicCon.playDeathSound ();

		anim.SetTrigger ("Death");

		correctInputs.Clear ();
		CancelInvoke ();
	}

	public void endOfPlayerDeath() {
		disablePlayer ();
		gameCon.resetLevel ();
	}

	public void disablePlayer() {
		gameObject.SetActive (false);
	}

	public void enablePlayer() {
		gameObject.SetActive (true);
	}
}
