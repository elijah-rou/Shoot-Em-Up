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

	void FixedUpdate(){
		
	}

	void OnTriggerEnter2D(Collider2D other){
		Instantiate(laserEffect, transform.position, transform.rotation);
		anim.SetTrigger("Impact");
		rb.velocity = new Vector2(0f, 0f);
		triggerDecay = true;
	}

	Vector3 direction(){
		return Quaternion.AngleAxis(rb.rotation, Vector3.forward) * Vector3.up;
	}
}
