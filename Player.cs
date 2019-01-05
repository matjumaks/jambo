using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour {

	public int fallBoundary = -20;
	public string deathSoundName = "DeathVoice";
	public string damageSoundName = "Ground";
	private AudioManager audioManager;
	[SerializeField]
	private StatusIndicator statusIndicator;

	private PlayerStats stats;

	[SerializeField]
	private GameObject[] weapons;

	void Start()
	{	
		StartCoroutine(ExecuteAfterTime(3));
		Physics2D.IgnoreLayerCollision (14, 8);

		stats = PlayerStats.instance;

		Physics2D.IgnoreLayerCollision(8, 13);

		stats.curHealth = stats.maxHealth;

		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
		}
		else
		{
			statusIndicator.setHealth(stats.curHealth, stats.maxHealth);
		}

		GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("PANIC! No audiomanager in scene.");
		}
		InvokeRepeating("RegenHealth", 1f/stats.healthRegenRate, 1f/stats.healthRegenRate);
	}

	void RegenHealth ()
	{
		stats.curHealth += 1;
		statusIndicator.setHealth(stats.curHealth, stats.maxHealth);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			weapons [1].gameObject.SetActive (false);
			weapons [0].gameObject.SetActive (true);
		}  

		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			weapons [1].gameObject.SetActive (true);
			weapons [0].gameObject.SetActive (false);
		}

		if (transform.position.y <= fallBoundary)
			DamagePlayer (9999999);
	}

	void OnUpgradeMenuToggle  (bool active)
	{
		GetComponent<Platformer2DUserControl>().enabled = !active;
		Weapon _weapon = GetComponentInChildren<Weapon>();
		if (_weapon != null)
			_weapon.enabled = !active;
	}

	public void DamagePlayer (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0)
		{
			//play death sound
			audioManager.PlaySound(deathSoundName);
			GameMaster.KillPlayer(this);
		} else
	{
		//play damage sound
		audioManager.PlaySound(damageSoundName);
	}

		statusIndicator.setHealth(stats.curHealth, stats.maxHealth);
	}

	void OnDestroy ()
	{
		GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		Debug.LogError ("works");
		Physics2D.IgnoreLayerCollision (14, 8,false);

		// Code to execute after the delay
	}

}
