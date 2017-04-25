using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Player{
	public int life;
	public GameObject player;
	public PlayerController plyrCtrl;
	public GameObject[] cores;
	public GameObject boostCore;
	public GameObject rateCore;
	public GameObject invulnerabilityCore;
	public GameObject win;
}

[System.Serializable]
public struct Creep{
	public int life;
	public GameObject creep;
	public int maxCreeps;
	public int spawnTime;
	public Transform[] spawnPoints;
}
[System.Serializable]
public struct LivingCreep{
	public int life;
	public string type;
	public GameObject creep;

}


public class GameController : MonoBehaviour {

	public Player player1;
	public Player player2;
	public Creep s_creep;
	public Creep med_creep;
	public Creep big_creep;
	public List<LivingCreep> smallCreeps;
	public List<LivingCreep> mediumCreeps;
	public List<LivingCreep> bigCreeps;
	public GameObject gameMenu;
	public bool menuOn;
	public AudioSource hurtSound;
	public string menuScene;
	
	// core powerups


	// Use this for initialization
	void Start () {
		foreach (PlayerController k  in FindObjectsOfType<PlayerController>()){
			if(k.tag == "Player1"){
				player1.plyrCtrl = k;
			}
			else if(k.tag == "Player2"){
				player2.plyrCtrl = k;
			}
		}
		InvokeRepeating ("SpawnSmall", s_creep.spawnTime, s_creep.spawnTime);
		InvokeRepeating ("SpawnMedium", med_creep.spawnTime, med_creep.spawnTime);
		InvokeRepeating ("SpawnBig", big_creep.spawnTime, big_creep.spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
		// check boost core
		if(player1.plyrCtrl.boostIsCooldown){
			player1.boostCore.SetActive(false);
		}
		else{
			player1.boostCore.SetActive(true);
		}
		if(player2.plyrCtrl.boostIsCooldown){
			player2.boostCore.SetActive(false);
		}
		else{
			player2.boostCore.SetActive(true);
		}
		// win condition
		if(player1.life <= 0 && !player1.win.activeSelf){
			player1.player.SetActive(false);
			player2.win.SetActive(true);
		}
		else if(player2.life <= 0 && !player2.win.activeSelf){
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

	public void SpawnSmall(){
		// Find a random index between zero and one less than the number of spawn points
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		if(smallCreeps.Count < s_creep.maxCreeps){
			Debug.Log("SPAWN SMALL CREEP");
			int spawnPointIndex = Random.Range (0, s_creep.spawnPoints.Length);
			GameObject crp = (GameObject)Instantiate (s_creep.creep, s_creep.spawnPoints[spawnPointIndex].position, s_creep.spawnPoints[spawnPointIndex].rotation);
			smallCreeps.Add(new LivingCreep {life=s_creep.life, type="Small", creep = crp});
		}
	}

	public void SpawnMedium(){
		// Find a random index between zero and one less than the number of spawn points
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		if(mediumCreeps.Count < med_creep.maxCreeps){
			Debug.Log("SPAWN MEDIUM CREEP");
			int spawnPointIndex = Random.Range (0, med_creep.spawnPoints.Length);
			GameObject crp = (GameObject)Instantiate (med_creep.creep, med_creep.spawnPoints[spawnPointIndex].position, med_creep.spawnPoints[spawnPointIndex].rotation);
			mediumCreeps.Add(new LivingCreep {life=med_creep.life, type="Medium", creep = crp});
		}
	}
	public void SpawnBig(){
		// Find a random index between zero and one less than the number of spawn points
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		if(bigCreeps.Count < big_creep.maxCreeps){
			Debug.Log("SPAWN BIG CREEP");
			int spawnPointIndex = Random.Range (0, big_creep.spawnPoints.Length);
			GameObject crp = (GameObject)Instantiate (big_creep.creep, big_creep.spawnPoints[spawnPointIndex].position, big_creep.spawnPoints[spawnPointIndex].rotation);
			bigCreeps.Add(new LivingCreep {life=big_creep.life, type="Big", creep = crp});
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

	public void destroySmallCreep(GameObject creep){
		Destroy(creep);
	}
	
	public void HurtCreep(GameObject creep){
		
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
