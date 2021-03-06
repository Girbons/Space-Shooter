﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable	]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;

    public float fireRate = 0.25F;

    private float nextFire = 0.5F;
    private float myTime = 0.0F;

	void Update() {
		myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireRate;
			Instantiate(shot, transform.position, transform.rotation);	
            nextFire = nextFire - myTime;
            myTime = 0.0F;
			AudioSource audio = gameObject.GetComponent<AudioSource>();
			audio.Play();
        }
	}
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Rigidbody rb = gameObject.GetComponent<Rigidbody>();

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
