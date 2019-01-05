using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState{SPAWNING, WAITING, COUNTING};
	int k = 2;
	public string victory = "Victory";

	[SerializeField]
	AudioManager audioManager;

	[SerializeField]
	private GameObject gameWon;

	[System.Serializable]
	public class Wave{
		public string name;
		public Transform enemy, enemy2, enemy3, boss1;
		public int count;
		public float rate;
	}
	public Transform[] spawnPoints;
	public float searchCountdown = 1f;
	public Wave[] waves;
	public int nextWave = 0;
	public int NextWave{
		get{ return nextWave + 1; }
	}
	private SpawnState state =  SpawnState.COUNTING;
	public SpawnState State{
		get { return state; }
	}
	public float timeBetweenWaves = 5f;
	public float waveCountdown;
	public float WaveCountdown{
		get { return waveCountdown; }
	}

	void Start(){
		waveCountdown = timeBetweenWaves;
		audioManager = AudioManager.instance;
	}

	void Update()
	{
		
		if (state == SpawnState.WAITING) {
			if (!EnemyIsAlive()) {
				WaveCompleted();
			} else {
				return;
			}
		}

		if (waveCountdown <= 0) {
			if (state != SpawnState.SPAWNING) {
				StartCoroutine(SpawnWave (waves [nextWave]));
			}
		}
			else 
			{
				waveCountdown -= Time.deltaTime;
			}
		}

	void WaveCompleted(){
		
		Debug.Log ("Wave com");

		state = SpawnState.COUNTING;

		waveCountdown = timeBetweenWaves;
		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 5;
			state = SpawnState.WAITING;
			WonGame ();
		} else {
			nextWave++;
		}
	}

	public void WonGame (){
		if (gameWon.activeInHierarchy == false) {
			audioManager.PlaySound (victory);
			gameWon.SetActive (true);
		}
	}

		bool EnemyIsAlive(){
			searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0) {
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag ("Enemy") == null) {
				return false;
			}
		}
			return true;
		}

		IEnumerator SpawnWave(Wave _wave)
		{
			k++;
			state = SpawnState.SPAWNING;
		for (int i = 0, j = 3; i < _wave.count; i++, j++) {
			if (k > 5) {
				SpawnEnemy3 (_wave.boss1);
			} else {
					
				SpawnEnemy (_wave.enemy, _wave.enemy2);
				if (j < 4) {
					SpawnEnemy2 (_wave.enemy3);
				}

			}
		
			yield return new WaitForSeconds(1f/_wave.rate);
		}
		state = SpawnState.WAITING;

		yield break;
		} 
		
	void SpawnEnemy(Transform _enemy, Transform _enemy2)
		{
		Transform _sp = spawnPoints[Random.Range(0, 3)];
		Instantiate(_enemy, _sp.position,_sp.rotation);
		Transform _sp2 = spawnPoints[Random.Range(3, 5)];
		Instantiate(_enemy2, _sp2.position,_sp2.rotation);
		}
	void SpawnEnemy2(Transform _enemy3)
		{
			Transform _sp3 = spawnPoints[Random.Range(5, 6)];
			Instantiate(_enemy3, _sp3.position,_sp3.rotation);
		}
	void SpawnEnemy3(Transform _boss1)
		{
			Transform _sp4 = spawnPoints [Random.Range (6, 7)];
			Instantiate (_boss1, _sp4.position, _sp4.rotation);
		}
	}

