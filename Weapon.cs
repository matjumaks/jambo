using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float effectSpawnRate = 0;
	public int Damage = 10;
	public LayerMask whatToHit;
	float timeToSpawnEffect = 0;

	public string weaponShootSound = "DefaultShot";

	public Transform BulletTrailPrefab;
	public Transform MuzzleFlashPrefab;
	public Transform HitPrefab;

	// Caching
	AudioManager audioManager;

	public float camShakeAmt = 0.1f;
	CameraShake camShake;
	
	float timeToFire = 0;
	Transform firepoint;

	// Use this for initialization
	void Awake () {
		firepoint = transform.FindChild ("Fire");
	
	}

	void Start(){
		camShake = GameMaster.gm.GetComponent<CameraShake> ();

		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("FREAK OUT! No audiomanager found in scene.");
		}
	}

	// Update is called once per frame
	void Update () {
		if (fireRate == null) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		} else {

			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
					Shoot();
			}
		}
	}


	void Shoot(){
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firepoint.position.x, firepoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
		

		

		if (hit.collider != null)  {
		


			Enemy enemy = hit.collider.GetComponent<Enemy> ();
			if (enemy != null) {
				enemy.DamageEnemy (Damage);
				Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
			}
		}

		if(Time.time>=timeToSpawnEffect){
			Vector3 hitPos;
			Vector3 hitNormal;

			if (hit.collider == null) {
				hitPos = (mousePosition - firePointPosition) * 30;
				hitNormal = new Vector3 (9999, 9999, 9999);
			} else {
				hitPos = hit.point;
				hitNormal = hit.normal;
			}
			Effect(hitPos , hitNormal);
			timeToSpawnEffect = Time.time +1/effectSpawnRate;
		}

	}
	void Effect(Vector3 hitPos, Vector3 hitNormal){
		//Transform trail = Instantiate (BulletTrailPrefab,firepoint.position,firepoint.rotation) as Transform;
		/* LineRenderer lr = trail.GetComponent<LineRenderer> ();
		if (lr != null) {
			lr.SetPosition (0, firepoint.position);
			lr.SetPosition (1, hitPos);
		} */

		//Destroy (trail.gameObject,0.01f);

		if (hitNormal != new Vector3(9999, 9999, 9999))
		{
			Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation (Vector3.up, hitNormal)) as Transform;
			Destroy(hitParticle.gameObject, 1f);
		}

		Transform clone = Instantiate (MuzzleFlashPrefab,firepoint.position,firepoint.rotation) as Transform;
		clone.parent = firepoint;
		float size = Random.Range (0.6f,0.9f);
		clone.localScale = new Vector3 (size,size,size);
		Destroy (clone.gameObject,0.1f);

		camShake.Shake (camShakeAmt, 0.2f);

		//Play shoot sound
		audioManager.PlaySound(weaponShootSound);
	}
}
