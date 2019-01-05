using UnityEngine;
using System.Collections;
//using System.Collections.IEnumerable;

public class EnemyWeapon2 : MonoBehaviour {
	//public Transform BulletTrailPrefab;
	//public Transform MuzzleFlashPrefab;
	//public Transform HitPrefab;
	public Transform misslebang;
	public string weaponShootSound = "DefaultShot";

	AudioManager audioManager;

	[SerializeField]
	public Player player;

	public LayerMask whatToHit;

	Transform firepoint;
	[SerializeField]
	Transform target;

	void Awake () {
		firepoint = transform.FindChild ("EnemyFire");
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}


	void Start(){
		InvokeRepeating("Effect", 2.0f, 1.0f);

		audioManager = AudioManager.instance;
	}

	/*

	void ShootAtPlayer()
	{
				//Get the mouse position on the screen and send a raycast into the game world from that position.
				//Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
				
		StartCoroutine (ExecuteAfterTime(2.0f));
				

	}


	IEnumerator ExecuteAfterTime(float time)
	{
		/*target = GameObject.FindGameObjectWithTag ("Player").transform;
		Vector2 playerPosition = new Vector2 (target.transform.position.x,target.transform.position.y);
		yield return new WaitForSeconds(time);

		Vector2 firePointPosition = new Vector2 (firepoint.position.x, firepoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, playerPosition - firePointPosition, 140, whatToHit);

		//If something was hit, the RaycastHit2D.collider will not be null.
		if (hit.collider != null)
		{
			if (hit.collider.name == "Player" || hit.collider.name == "Player(Clone)" || hit.collider.name == "Player2" && player != null) {
				player.DamagePlayer (20);

			} else {
				player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
				Debug.Log (hit.collider.name);

			}
		}

			Vector3 hitPos;
			Vector3 hitNormal;

			if (hit.collider == null) {
				hitPos = (playerPosition - firePointPosition) * 30;
				hitNormal = new Vector3 (9999, 9999, 9999);
			} else {
				hitPos = hit.point;
				hitNormal = hit.normal;
			}
			Effect(hitPos , hitNormal);
		Effect ();
			


		yield break;
		// Code to execute after the delay
	}

	*/
	void Effect(){
		Vector3 temp = new Vector3(0.3f,0,0);
		Vector3 temp2= new Vector3(0.9f,0,0);
		//Debug.LogError (firepoint.position);
		for(int i = 0; i < 3; i ++){
		firepoint.position += temp;
		Transform missle = Instantiate (misslebang,firepoint.position,firepoint.rotation) as Transform;
		
		Destroy (missle.gameObject,3f);
		
		
		}
		audioManager.PlaySound(weaponShootSound);
		firepoint.position -= temp2;
		/*
		if (hitNormal != new Vector3(9999, 9999, 9999))
		{
			Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation (Vector3.up, hitNormal)) as Transform;
			Destroy(hitParticle.gameObject, 1f);
		}

		Transform clone = Instantiate (MuzzleFlashPrefab,firepoint.position,firepoint.rotation) as Transform;
		clone.parent = firepoint;
		float size = Random.Range (0.6f,0.9f);
		clone.localScale = new Vector3 (size,size,size);
		Destroy (clone.gameObject,0.1f);*/


		//Play shoot sound

	}

}
