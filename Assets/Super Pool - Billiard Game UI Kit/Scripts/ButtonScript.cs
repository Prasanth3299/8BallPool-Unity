using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour 
{

	public void PlayButton(string GameSceneExample)
	{
		SceneManager.LoadScene("Game Scene Example");
	}
		
	public void MenuButton(string MainScreenExample)
	{
		SceneManager.LoadScene("Main Screen Scene");
	}

	public void CueShop(string CueShopExample)
	{
		SceneManager.LoadScene("Cue Shop Scene");
	}

	void Update() {
		if (Input.GetKey("escape"))
			Application.Quit();

	}
}