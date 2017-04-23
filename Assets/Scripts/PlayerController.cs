using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float velocity;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < 0.5f){
			transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal")*velocity*Time.deltaTime, 0f, 0f));
		}
		if(Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < 0.5f){
			transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical")*velocity*Time.deltaTime, 0f));
		}
		
		animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
	}
	
}
