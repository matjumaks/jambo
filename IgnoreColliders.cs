using UnityEngine;
using System.Collections;

public class IgnoreColliders : MonoBehaviour {

	public Enemy enemy;
	public Transform deathParticles;
	[SerializeField]
	public AudioManager audioManager;

	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		Physics2D.IgnoreLayerCollision(14, 13);
		Physics2D.IgnoreLayerCollision (14, 14);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag != ""){
			Particles ();
			audioManager.PlaySound("Explosion");
			Destroy (gameObject);
			//enemy.DamageEnemy(9999999);
		}
}
	void Particles(){
		Transform _clone = Instantiate(deathParticles, new Vector3(transform.position.x,transform.position.y,-4) , Quaternion.identity) as Transform;
		Destroy(_clone.gameObject, 5f);
		Debug.Log ("dziala raz");
	}

}
