using UnityEngine;
using System.Collections;

public class TrailBullet : MonoBehaviour {

	public int speedT = 230;

	[SerializeField]
	public Player player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.right * Time.deltaTime * speedT);

		//Destroy (this.gameObject, 5f);
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.name == "Player" || coll.collider.name == "Player(Clone)" || coll.collider.name == "Player2" || 
			coll.collider.name == "Player2(Clone)" ) {
			Debug.LogError (coll.collider.name);
			player.DamagePlayer (20);
			Destroy (this.gameObject);

		} else {
			Destroy (this.gameObject);
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		}
	}
}
