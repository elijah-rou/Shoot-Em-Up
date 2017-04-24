using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScript : MonoBehaviour {

	public string menu;
	void Update(){
		if(Input.GetKey(KeyCode.Escape)){
			returnMenu();
		}
	}

	public void returnMenu() {
		SceneManager.LoadScene(menu);
	}
	
	public void QuitGame() {
		Application.Quit();
	}
}
