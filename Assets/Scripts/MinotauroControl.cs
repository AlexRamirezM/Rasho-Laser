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


	public Text TimeText;
	private float tiempo=0f;
	public int Minutos, segundos;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		tiempo += Time.deltaTime;
		Minutos = (int)tiempo / 60;
		segundos = (int)tiempo - (Minutos * 60);



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

			float redo = Mathf.Round (respawntime);
			resptext.text = redo.ToString ();
		}

	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Laser" && bandera==1) {
			//Intervalotime debe ser 1 + el numero de minutos transcurridos.
			respawntime = 0.5f+Minutos*1.5f;;

			bandera = 0;

			resptext.text = respawntime.ToString ();

	


		}
	}

}
