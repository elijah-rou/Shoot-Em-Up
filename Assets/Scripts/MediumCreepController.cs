using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumCreepController : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;
	private Animator anim;
	private Transform player1;
	private Transform player2;
	public float counter;
	public GameObject laser;
	public Transform shootPoint;
	public float laserRate;
	private float timer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		foreach (PlayerController k  in FindObjectsOfType<PlayerController>()){
			if(k.tag == "Player1"){
				player1 = k.GetComponent<Rigidbody2D>().transform;
			}
			else if(k.tag == "Player2"){
				player2 = k.GetComponent<Rigidbody2D>().transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// follow closest player
		if(Vector2.Distance(transform.position, player1.position) < Vector2.Distance(transform.position, player2.position)){
			//rotate to look at the player
			transform.LookAt(player1.position);
			transform.Rotate(new Vector3(0, -90, 90));

			//move towards the player
			if (Vector2.Distance(transform.position,player1.position) > 10){
				transform.position = Vector2.MoveTowards(transform.position, player1.position, speed*Time.deltaTime);
			}
			else{
				rb.velocity = new Vector2(0, 0);
			}
		}
		else{
			//rotate to look at the player
			transform.LookAt(player2.position);
			transform.Rotate(new Vector3(0,-90,90), Space.Self);

			//move towards the player
			if (Vector2.Distance(transform.position,player2.position) > 10){
				transform.position = Vector2.MoveTowards(transform.position, player2.position, speed*Time.deltaTime);
			}
			else{
				rb.velocity = new Vector2(0, 0);
			}
		}
		timer += Time.deltaTime;
		if(timer > 1/laserRate){
			Instantiate(laser, shootPoint.position, shootPoint.rotation);
			timer = 0;
		}
	}

	Vector3 direction(){
		return Quaternion.AngleAxis(rb.rotation, Vector3.forward) * Vector3.up;
	}
}
