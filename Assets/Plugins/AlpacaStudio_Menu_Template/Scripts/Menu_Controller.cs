using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour {

	public string sceneToLoadOnPlay = "Level";
	public GameObject mainMenu;
	public GameObject credits;
	
	public void PlayGame () {
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoadOnPlay);
	}
	
	public void QuitGame () {
		Application.Quit();
	}

	public void MainMenu()
	{
		mainMenu.SetActive(true);
		credits.SetActive(false);
	}
	
	public void Credits()
	{
		mainMenu.SetActive(false);
		credits.SetActive(true);
	}
}
