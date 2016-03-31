
	using UnityEngine;
	using System.Collections;
	using UnityEngine.UI;

public class ConstructorControl : MonoBehaviour {
		private float vel = 12;
		private float respawntime=0;
	private float inmunity=0;
	private float animtime;
	public Animation anim;
	private NetworkView nw;

	int unav=1;
		//public Text resptext;
		// Use this for initialization
		Rigidbody rigidbody;
		private Quaternion rot = new Quaternion ();
	    public GameObject CuboDestructible;
		public GameObject Espejo_45;
		public GameObject Espejo_315;

		private Text TimeText;
		private float tiempo=0f;
		private int Minutos, segundos;

		void Start () {
			rigidbody = GetComponent<Rigidbody>();
		anim = GetComponent<Animation>();
		nw= GetComponent<NetworkView>();
		}

		// Update is called once per frame
		void Update () {

			tiempo += Time.deltaTime;
			Minutos = (int)tiempo / 60;
			segundos = (int)tiempo - (Minutos * 60);


		//Aqui se pone todo lo que pueda hacer mientras está vivo
		if (nw.isMine) {
			if (respawntime <=0) {
			//resptext.text = " ";
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
				Network.Instantiate (CuboDestructible, posact, Quaternion.identity,0);

			}

			if (Input.GetKeyDown (KeyCode.K)) {
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
				Vector3 qua =new Vector3(0,1,0);
					Network.Instantiate (Espejo_45, posact, Quaternion.AngleAxis(45f,qua),0);


			}

			if (Input.GetKeyDown (KeyCode.L)) {
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
				Vector3 qua =new Vector3(0,1,0);
					Network.Instantiate (Espejo_315, posact, Quaternion.AngleAxis(315f,qua),0);

			}



			} else {
				respawntime -= Time.deltaTime;
				if (respawntime <= 0) {
					transform.position= (new Vector3 (18, 0, -10));
				inmunity = 2;
					respawntime = 0;

				}

				float redo = Mathf.Round (respawntime);
				//resptext.text = redo.ToString ();
			}

		if (inmunity >0) {
			inmunity -= Time.deltaTime;
			//resptext.text = "Inmunity";
		}


		}
		}

		void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Laser" && unav==1 && inmunity <=0) {
				
				respawntime = 1f+Minutos*1.5f;;

				unav = 0;

				//resptext.text = respawntime.ToString ();

			anim.Play ("Die");


			}
		}

	}
