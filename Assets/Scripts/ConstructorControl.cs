
	using UnityEngine;
	using System.Collections;
	using UnityEngine.UI;

public class ConstructorControl : MonoBehaviour {
		private float vel = 12;
		private float respawntime=0;
	private float animtime;
	public Animation anim;

	int unav=1;
		public Text resptext;
		// Use this for initialization
		Rigidbody rigidbody;
		private Quaternion rot = new Quaternion ();
	    public GameObject CuboDestructible;

		private Text TimeText;
		private float tiempo=0f;
		private int Minutos, segundos;

		void Start () {
			rigidbody = GetComponent<Rigidbody>();
		anim = GetComponent<Animation>();
		}

		// Update is called once per frame
		void Update () {

			tiempo += Time.deltaTime;
			Minutos = (int)tiempo / 60;
			segundos = (int)tiempo - (Minutos * 60);


		//Aqui se pone todo lo que pueda hacer mientras está vivo
			if (respawntime <=0) {
			unav = 1;
				float movh = Input.GetAxis ("Horizontal");
				float movv = Input.GetAxis ("Vertical");

				Vector3 mov = new Vector3 (movh, 0, movv);
				transform.position += mov * Time.deltaTime * vel;

				if (movh == 0 && movv == 0) {
					if (animtime <= 0) {
					anim.Play ("Idle_2");
					}
				animtime -= Time.deltaTime;

				} else {
					rot.SetLookRotation (mov);
					transform.rotation = rot;
				animtime = 2;
				anim.Play("RunCycle");

				}


			if (Input.GetKeyDown (KeyCode.J)) {
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

				Instantiate (CuboDestructible, posact,Quaternion.identity);
			}





			} else {
				respawntime -= Time.deltaTime;
				if (respawntime <= 0) {
					transform.position= (new Vector3 (18, 0, -10));
					respawntime = 0;

				}

				float redo = Mathf.Round (respawntime);
				resptext.text = redo.ToString ();
			}


		}


		void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Laser" && unav==1) {
				
				respawntime = 1f+Minutos*1.5f;;

				unav = 0;

				resptext.text = respawntime.ToString ();

			anim.Play ("Die");


			}
		}

	}
