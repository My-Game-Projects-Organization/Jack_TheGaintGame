using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//To move the player
	//Public to see it in Inspector Panel and private no
	public float speed = 8f, maxVelocity = 4f;

	//[SerializeField] //To visible Private function in Inspector Panel 
	private Rigidbody2D myBody;//Physic
	private Animator anim;//Animation

	//Awake function
	void Awake(){
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	/* Update is called every frame per second, if we have 60 FPS, it'll call
	 60 times PS, but FixedUpdate call every couple */
	void FixedUpdate () {
		PlayerMoveKeyboard ();
	}

	//Create function to move player
	void PlayerMoveKeyboard(){
		//force in the X axe
		float forceX = 0f;
		//Returns the absolute value of f and return x velecity in Rigidbody
		float vel = Mathf.Abs (myBody.velocity.x);

		//press left arrow key, right arrow key or A,D key
		float h = Input.GetAxisRaw ("Horizontal");

		//moving right when we press D key or right arrow key
		if (h > 0) {
		
			if (vel < maxVelocity)//it still move
				forceX = speed;

			/*Give references scale in Player Inspector, present is 1.3f*/
			Vector3 temp = transform.localScale;
			/*Set scale x exact 1.3f b/c he moving right*/
			temp.x = 1.3f;

			transform.localScale = temp;

			/*Animation Player b/c in Player Animator Window we set 
			 parametter Walk is bool. It set TRUE b/c we need to transition
			 from Idle to Walk and if we move, we see him move*/ 
			anim.SetBool ("Walk", true);

		} else if (h < 0) {
			if (vel < maxVelocity)
				forceX = -speed;//left side is negative


			Vector3 temp = transform.localScale;
			temp.x = -1.3f;//b/c he facing the left side
			transform.localScale = temp;

			anim.SetBool ("Walk", true);
			 
			/* else meaning that if h not larger or less than 0, we set 
			 it FALSE to not Animation and if we stop, wee see him stand, not
			 Animation*/
		} else {
			anim.SetBool ("Walk", false);
		}

		//Apply a force to the rigidbody, 
		myBody.AddForce (new Vector2 (forceX,0));
	}
}
