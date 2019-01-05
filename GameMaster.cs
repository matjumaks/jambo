using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	[SerializeField]
	private int maxLives = 3;

	public static GameMaster gm;
	private static int _remaninglives;
	public static int Remaninglives
	{
		get{ return _remaninglives; }
	}

	void Awake () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
	}

	public delegate void UpgradeMenuCallback(bool active);
	public UpgradeMenuCallback onToggleUpgradeMenu;

	[SerializeField]
	private int startingMoney;
	public static int Money;

	public string respawnCountdownSoundName = "RespawnCountdown";
	public string spawnSoundName = "Spawn";
	public string gameOverSoundName = "GameOver";

	[SerializeField]
	private GameObject upgradeMenu;
	[SerializeField]
	private WaveSpawner waveSpawner;



	public Transform playerPrefab;
	public Transform playerPrefab2;
	public Transform spawnPoint;
	public float spawnDelay = 2;
	public CameraShake cameraShake;
	//public Scene scene = SceneManager.GetActiveScene();
	[SerializeField]
	private GameObject gameOverUI;

	//cache
	private AudioManager audioManager;

	void Start(){

		_remaninglives = maxLives;

		Money = startingMoney;
		//caching
		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
		}




	}
	public void EndGame (){

		audioManager.PlaySound(gameOverSoundName);
		gameOverUI.SetActive (true);
	}



	void Update ()
	{
		

		if (Input.GetKeyDown(KeyCode.U))
		{
			ToggleUpgradeMenu();
		}
	}

	private void ToggleUpgradeMenu ()
	{
		upgradeMenu.SetActive( !upgradeMenu.activeSelf );
		onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
		waveSpawner.enabled = !upgradeMenu.activeSelf;
	}

	public IEnumerator RespawnPlayer(){
		audioManager.PlaySound(respawnCountdownSoundName);
		yield return new WaitForSeconds (spawnDelay);
		audioManager.PlaySound(spawnSoundName);
		Scene scene = SceneManager.GetActiveScene();
		Debug.Log("Active scene is '" + scene.buildIndex + "'.");
		if (scene.buildIndex == 1) {
			Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		} else {
			Instantiate (playerPrefab2, spawnPoint.position, spawnPoint.rotation);
		}
		//add spawn particles
	}

	public static void KillPlayer (Player player) {
		Destroy(player.gameObject);
		_remaninglives -= 1;
		if (_remaninglives <= 0) {
			gm.EndGame ();
		} else {
			gm.StartCoroutine (gm.RespawnPlayer());
		}
	}

	public static void KillEnemy (Enemy enemy) {
		gm._KillEnemy(enemy);
	}

	public void _KillEnemy(Enemy _enemy)
	{
		// Gain some money
		Money += _enemy.moneyDrop;
		audioManager.PlaySound("Money");

		Transform _clone = Instantiate(_enemy.deathParticles, new Vector3( _enemy.transform.position.x, _enemy.transform.position.y,-4) , Quaternion.identity) as Transform;
		Transform _clone2 = Instantiate(_enemy.deathParticles2, new Vector3( _enemy.transform.position.x, _enemy.transform.position.y,-4) , Quaternion.identity) as Transform;
		Destroy(_clone.gameObject, 5f); 
		Destroy(_clone2.gameObject, 5f); 
		//zniszczyc czasteczki
		cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
		Destroy(_enemy.gameObject);
		// Let's play some sound
		audioManager.PlaySound(_enemy.deathSoundName);
	}


}
