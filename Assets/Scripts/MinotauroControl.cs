using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinotauroControl : MonoBehaviour {
	private float vel = 10;
	float respawntime=5;

	int bandera=1;
	public Text resptext;
	// Use this for initialization
	Rigidbody rigidbody;
	Quaternion rot = new Quaternion ();

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (respawntime <=0) {
			bandera = 1;
			float movh = Input.GetAxis ("Horizontal");
			float movv = Input.GetAxis ("Vertical");

			Vector3 mov = new Vector3 (movh, 0, movv);
			transform.position += mov * Time.deltaTime * vel;

			if (movh == 0 && movv == 0) {
			} else {
				rot.SetLookRotation (mov);
				transform.rotation = rot;
			}


		} else {
			respawntime -= Time.deltaTime;
			if (respawntime <= 0) {
				transform.position= (new Vector3 (-10, 2, -10));
				respawntime = 0;

			}
			resptext.text = respawntime.ToString ();

		}

	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Laser" && bandera==1) {
			//Intervalotime debe ser 1 + el numero de minutos transcurridos.
			respawntime = 5;

			bandera = 0;

			resptext.text = respawntime.ToString ();

	


		}
	}

}
