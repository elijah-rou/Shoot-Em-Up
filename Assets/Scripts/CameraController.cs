using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject target;
	public float speed;
	private Vector3 targetPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		targetPos = new Vector3(
			target.transform.position.x,
			target.transform.position.y,
			transform.position.z
			);
		transform.position = Vector3.Lerp(
			transform.position,
			targetPos,
			speed*Time.deltaTime
		);
	}
}
