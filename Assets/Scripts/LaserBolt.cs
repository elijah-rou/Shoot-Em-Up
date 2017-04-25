using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : MonoBehaviour {

	public float speed;
	public float decayRate;
	public bool triggerDecay;
	private Rigidbody2D rb;
	public GameObject laserEffect;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		// obtain direction
		rb.velocity = direction()*speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(triggerDecay){
			decayRate -= Time.deltaTime;

			if(decayRate < 0){
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Instantiate(laserEffect, transform.position, transform.rotation);
		rb.velocity = new Vector2(0f, 0f);
		anim.SetTrigger("Impact");
		triggerDecay = true;
		if(other.tag == "Player1"){
			FindObjectOfType<GameController>().HurtP1();
		}
		else if(other.tag == "Player2"){
			FindObjectOfType<GameController>().HurtP2();
		}
		else if(other.tag == "SmallCreep"){
			FindObjectOfType<GameController>().destroySmallCreep(other.gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		Destroy(gameObject);
	}

	Vector3 direction(){
		return Quaternion.AngleAxis(rb.rotation, Vector3.forward) * Vector3.up;
	}
}
