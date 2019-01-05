using UnityEngine;
using System.Collections;

public class QuitButtonWon : MonoBehaviour {

	public void Quit(){
		Application.Quit ();
		Debug.LogError ("quit");
	}
}
