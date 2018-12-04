using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour {
	

	public void QuitGame() {
		Application.Quit();
	}
	public void TitleScreen() {
		SceneManager.LoadScene("Title");
	}
	public void RestartLvl() {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void NextLvl() {
		
	}
}
