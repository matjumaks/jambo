using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	[SerializeField]
	string hoverOverSound = "ButtonHover";

	[SerializeField]
	string pressButtonSound = "ButtonPress";

	public int Char = 1;
	[SerializeField]
	public GameObject myObject;

	AudioManager audioManager;

	void Start ()
	{
		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("No audiomanager found!");
		}
	}

	public void SetCharactertoOne(){
		Char = 1;
	}

	public void SetCharacter(){
		Char = 2;
	}

	public void SetCharacter2(){
		Char = 3;
	}

	public void StartGame ()
	{
		audioManager.PlaySound(pressButtonSound);
		if(Char == 1){
			myObject.gameObject.SetActive (true);
		}else if( Char == 2){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}else if( Char == 3){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
		}
	}


	public void QuitGame()
	{
		audioManager.PlaySound(pressButtonSound);

		Debug.Log("WE QUIT THE GAME!");
		Application.Quit();
	}

	public void OnMouseOver ()
	{
		audioManager.PlaySound(hoverOverSound);
	}
}
