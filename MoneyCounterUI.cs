using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour {

	private Text moneytext;

	// Use this for initialization
	void Awake() {
		moneytext = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		moneytext.text = "MONEY: " + GameMaster.Money.ToString ();
	}
}

