using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LivesCounterUI : MonoBehaviour {

	private Text livestext;

	// Use this for initialization
	void Awake() {
		livestext = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		livestext.text = "LIVES: " + GameMaster.Remaninglives.ToString ();
	}
}
