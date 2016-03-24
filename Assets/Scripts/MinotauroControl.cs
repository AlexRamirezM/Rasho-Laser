using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinotauroControl : MonoBehaviour {
	private float vel = 10;
	float respawntime=5;
	float inmunity=0;
	int unav=1;
	float animtime;
	public Text resptext;
	// Use this for initialization
	Rigidbody rigidbody;
	Quaternion rot = new Quaternion ();
	public GameObject Laserbomb;

	private Text TimeText;
	private float tiempo=0f;
	private int Minutos, segundos;

	public Animation anim;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {

		tiempo += Time.deltaTime;
		Minutos = (int)tiempo / 60;
		segundos = (int)tiempo - (Minutos * 60);



		if (respawntime <=0) {
			resptext.text = " ";
			unav = 1;
			float movh = Input.GetAxis ("Horizontal");
			float movv = Input.GetAxis ("Vertical");

			Vector3 mov = new Vector3 (movh, 0, movv);
			transform.position += mov * Time.deltaTime * vel;

			if (movh == 0 && movv == 0) {
				animtime -= Time.deltaTime;
				if (animtime <= 0) {
					anim.Play ("Idle_2");
				}

			} else {
				animtime = 2;
				rot.SetLookRotation (mov);
				transform.rotation = rot;


				//anim.PlayQueued("RunCycle", QueueMode.PlayNow);
				anim.Play("RunCycle");
			}


			if (Input.GetKeyDown (KeyCode.Q)) {
				anim.Play ("Attack_2");
				animtime = 2;
				Vector3 posact = transform.position;
				int sx = 0, sz = 0;
				if (rot.y == 0) {
					sz =  2;
				} 
				if (rot.y == 1) {
					sz = -2;
				} 
				if (rot.y <1 && rot.y >0) {
					sx = 2;
				} 
				if (rot.y < 0) {
					sx = -2;
				} 


				posact.x = (Mathf.Round(transform.position.x / 2) * 2)+sx;
				posact.z = (Mathf.Round(transform.position.z / 2) * 2)+sz;
				posact.y = 2;

				if (posact.x >= 20  ) {
					posact.x=18;
				}
				if (posact.x <= -12) {
					posact.x=-10;
				}
				if (posact.z >= 12) {
					posact.z = 10;
				}
				if (posact.z <= -12) {
					posact.z=-10;
				}
				Instantiate (Laserbomb, posact, Quaternion.identity);
				


			}
			if (Input.GetKeyDown (KeyCode.R)) {
				anim.Play ("Attack_3");
				animtime = 2;
			}


		} else {
			respawntime -= Time.deltaTime;
			if (respawntime <= 0) {
				transform.position= (new Vector3 (-10, 0, -10));
				inmunity = 2;
				respawntime = 0;

			}

			float redo = Mathf.Round (respawntime);
			resptext.text = redo.ToString ();
		}
		if (inmunity >= 0) {
			inmunity -= Time.deltaTime;
			resptext.text = "Inmunity";
		} 

	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Laser" && unav==1 && inmunity<=0) {
			//Intervalotime debe ser 1 + el numero de minutos transcurridos.
			respawntime = 1f+Minutos*1.5f;;

			unav= 0;

			resptext.text = respawntime.ToString ();
			anim.Play ("Die");


		}
	}

}
