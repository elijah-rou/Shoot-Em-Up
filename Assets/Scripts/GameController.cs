using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Player{
	public int life;
	public GameObject player;
	public GameObject[] cores;
	public GameObject win;
}
public class GameController : MonoBehaviour {

	public Player player1;
	public Player player2;
	public GameObject gameMenu;
	public bool menuOn;
	public AudioSource hurtSound;
	public string menuScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// win condition
		if(player1.life <= 0){
			player1.player.SetActive(false);
			player2.win.SetActive(true);
		}
		if(player2.life <= 0){
			player2.player.SetActive(false);
			player1.win.SetActive(true);
		}

		// win screen
		if(player1.win.activeSelf || player2.win.activeSelf){
			if(Input.GetKeyDown(KeyCode.R)){
				restart();
			}
			if(Input.GetKeyDown(KeyCode.M)){
				returnMenu();
			}
			if(Input.GetKeyDown(KeyCode.Q)){
				QuitGame();
			}
		}
		// either pause or plaing
		else{
			// paused
			if(menuOn){
				if(Input.GetKeyDown(KeyCode.Escape)){
					resume();
				}
				if(Input.GetKeyDown(KeyCode.R)){
					restart();
				}
				if(Input.GetKeyDown(KeyCode.M)){
					returnMenu();
				}
				if(Input.GetKeyDown(KeyCode.Q)){
					QuitGame();
				}
			}
			// playing
			else{
				if(Input.GetKeyDown(KeyCode.Escape)){
					pause();
				}
			}
		}
	}

	public void HurtP1(){
		player1.life -= 1;
		player1.cores[player1.life].SetActive(false);
		hurtSound.Play();
	}

	public void HurtP2(){
		player2.life -= 1;
		player2.cores[player2.life].SetActive(false);
		hurtSound.Play();
	}

	public void pause(){
		gameMenu.SetActive(true);
		menuOn = true;
	}
	public void resume(){
		gameMenu.SetActive(false);
		menuOn = false;
	}
	public void restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void returnMenu() {
		SceneManager.LoadScene(menuScene);
	}
	
	public void QuitGame() {
		Application.Quit();
	} 
		
}
