using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	KeyboardInput keyIn;

	public float speed = 3f;
	public float jumpSpeed = 8f;
	public float gravSpeed = 10f;
	public bool grounded = false;

	void Start() {
		keyIn = GetComponent<KeyboardInput> ();
	}

	void Update() {

		if (Physics.Raycast (transform.position, new Vector3 (0, -1), .8f)) {
			grounded = true;
		} else {
			grounded = false;
		}

		if (grounded == false) {
			transform.Translate(new Vector3(0, -gravSpeed * Time.deltaTime));
		}

		if (keyIn.XAxis == 1 && !Physics.Raycast(transform.position, new Vector3(1, 0), .5f)) {
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0));
		} 
		else if (keyIn.XAxis == -1 && !Physics.Raycast(transform.position, new Vector3(-1, 0), .5f)) {
			transform.Translate (new Vector3 (-speed * Time.deltaTime, 0));
		}

		if (keyIn.JumpButtonPressedThisFrame && grounded == true) {
			transform.Translate(new Vector3(0, jumpSpeed));
		}
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.name == "Enemy Parent") {
			Application.LoadLevel(Application.loadedLevel);
		}

		if(collision.gameObject.name == "Moving Platform Parent" && grounded == true) {
			this.transform.parent = collision.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider collision) {
		if (collision.gameObject.name == "Moving Platform Parent") {
			this.transform.parent = null;
		}
	}

    /*
     * TO DO:
     *      
     * 
     * 
     * 
     * Variables you might want:
     *      References to the CharacterController and Keyboard input classes
     *      Speed values for moving, falling, and jumping
     *      A value to keep track of the player's movement speed and direction
     *      You will probably need to use the Update function as well as create functions for moving platforms and enemies
     */

}