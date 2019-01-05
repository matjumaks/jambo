using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomShow : MonoBehaviour {

	//Spawn this object
	public GameObject cloud;
	public Text cloudtext;

	int cloudR = 0;
	public float maxTime = 5;
	public float minTime = 2;

	//current time
	private float time;

	//The time to spawn the object
	private float spawnTime;

	void Start(){
		

		SetRandomTime();
		time = minTime;
	}

	void FixedUpdate(){
		
		cloudtext = cloud.gameObject.GetComponentInChildren<Text>();
		//Check if its the right time to spawn the object
		if(time >= spawnTime){
			SpawnObject();
			SetRandomTime();
			cloudR = Random.Range (1, 4);
		}

		if (cloudR == 1) {
			cloudtext.text = "Fuck yeah!";
		}

		if (cloudR == 2) {
			cloudtext.text = "Leczopeczo!";
		}

		if (cloudR == 3) {
			cloudtext.text = "Triss! Yen!";
		}
		//Counts up
		time += Time.deltaTime;


	}


	//Spawns the object and resets the time
	void SpawnObject(){
		time = 0;
		//Debug.LogError ("cloud test");
		cloud.gameObject.SetActive (true);
		Invoke("Turnoff", 2);
	}

	void Turnoff(){
		cloud.gameObject.SetActive (false);
	}

	//Sets the random time between minTime and maxTime
	void SetRandomTime(){
		spawnTime = Random.Range(minTime, maxTime);
	}

}