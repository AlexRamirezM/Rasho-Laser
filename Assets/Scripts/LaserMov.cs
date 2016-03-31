﻿using UnityEngine;
using System.Collections;

public class LaserMov : MonoBehaviour {
	public float velz = 0;
	public float velx = 20;


	Quaternion rot = new Quaternion ();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame



	void Update () {
		Vector3 mov = new Vector3 (velx,0,velz);
		if (velx == 0) {
			var encaja= transform.position;
			encaja.x = Mathf.Round (encaja.x/2)*2;
			transform.position = encaja;
		}
		if (velz == 0) {
			var encaja= transform.position;
			encaja.z = Mathf.Round (encaja.z/2)*2;
			transform.position = encaja;

		}

		transform.position += mov*Time.deltaTime;


		rot.SetLookRotation (mov);
		transform.rotation = rot;

	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Limiver")
		{
			velz = velz * (-1);

		}
		if (collision.gameObject.tag == "Limihori")
		{
			
			velx = velx * (-1);
		}
		if (collision.gameObject.tag == "CuboDestructible" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "CuboIndestructible")
		{
			Destroy (gameObject);
		}

		if (collision.gameObject.tag == "Espejo_45")
		{
			if (velx == 0) {
				velx = -velz;
				velz = 0;
			}else {
				velz = -velx;
				velx = 0;
			}
		}
		if (collision.gameObject.tag == "Espejo_315")
		{
			if (velx == 0) {
				velx = velz;
				velz = 0;
			}else {
				velz = velx;
				velx = 0;
			}
		}




		}

}
