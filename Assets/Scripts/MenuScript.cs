using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public string mainScene;
	public string controlScene;

	void Update(){
		if(Input.GetKey(KeyCode.Escape)){
			QuitGame();
		}
	}

	public void StartGame() {
		SceneManager.LoadScene(mainScene);
	}

	public void ShowControls(){
		SceneManager.LoadScene(controlScene);
	}
	
	public void QuitGame() {
		Application.Quit();
	}
}
