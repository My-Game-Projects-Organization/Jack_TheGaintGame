using UnityEngine;
using System.Collections;

/*BGScaler to fix the witdh in backgrounds, b/c background game is not full and we get a blue gap*/
public class BGScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*Use SpriteRenderer to render in the scenes*/
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		/*because we need to alter each position, resize background to fit camera of witdh*/
		Vector3 tempScale = transform.localScale;
		/*Giving the witdh size of our sprite*/
		float width = sr.sprite.bounds.size.x; 

		/*The height is represent of camera*/
		float worldHeight = Camera.main.orthographicSize * 2f;
		float worldWidth = worldHeight / Screen.height * Screen.width;

		tempScale.x = worldWidth / width;

		transform.localScale = tempScale;

	}
	
}
