using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// parameters
	public float boostForce;
	public float boostLength;
	public float boostCooldown;
	public bool boostIsCooldown;
	public bool boostActive;
	private float counter;
	public float velocity;
	public float angular_velocity;

	// gmae bodies
	private Animator animator;
	private Rigidbody2D rb;

	// controls
	public KeyCode left;
	public KeyCode right;
	public KeyCode thrust;
	public KeyCode fire;
	public KeyCode boost;

	// laser
	public GameObject laser;
	public Transform shootPoint;
	public float laserRate;
	private float timer;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame

	void FixedUpdate(){	
		if(boostIsCooldown){
			if(boostActive){
				counter += Time.fixedDeltaTime;
				if(counter >= boostLength){
					boostActive = false;
					counter = 0;
				}
			}
			else{
				counter += Time.fixedDeltaTime;
				if(counter >= boostCooldown){
					boostIsCooldown = false;
					counter = 0;
				}
			}
	
		}
		// rotate left or right
		if(Input.GetKey(left)){
			 rb.MoveRotation(rb.rotation+angular_velocity);
			 animator.SetTrigger("LeftTurn");
		}
		else if(Input.GetKey(right)){
			 rb.MoveRotation(rb.rotation-angular_velocity);
			 animator.SetTrigger("RightTurn");
		}
		else{
			rb.MoveRotation(rb.rotation);
		}
		if(!boostActive){
			if(Input.GetKey(thrust)){
			// determine angle
			//rb.AddForce(direction()*this.velocity*Time.fixedDeltaTime);
			rb.velocity = direction()*this.velocity;
			}
			else{
				rb.velocity = new Vector2(0f, 0f);
			}
		}
		
		if(Input.GetKey(boost) && boostActive == false && boostIsCooldown == false){
			boostActive = true;
			boostIsCooldown = true;
			rb.velocity = direction();
			rb.AddForce(direction()*boostForce*Time.fixedDeltaTime);
		}
	}
	
	void Update(){
		//animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		//animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
		timer += Time.deltaTime;
		if(Input.GetKey(fire)){
			if(timer > 1/laserRate){
				Instantiate(laser, shootPoint.position, shootPoint.rotation);
				timer = 0;
			}
		}
	}

	Vector3 direction(){
		return Quaternion.AngleAxis(rb.rotation, Vector3.forward) * Vector3.up;
	}
}
