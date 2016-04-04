﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinotauroControl : MonoBehaviour {
	
	float respawntime = 0;
	float inmunity = 0;
	int unav = 1;
	public Text resptext;

	private NetworkView nw;
	Quaternion Rotation = Quaternion.identity;
	public GameObject Laserbomb;

	private Text TimeText;
	private float tiempo = 0f;
	private int Minutos, segundos;

	Animator Anim_Minotauro;
	int Attack_Hash = Animator.StringToHash ("Attack");		// String a Int = Menos costo computacional
	int Walk_Hash = Animator.StringToHash ("Walk");

	Rigidbody rigidbody;

	public AnimatorStateInfo Animator_State; 
	int AttackState = Animator.StringToHash("Base Layer.Attack");

	Vector3 Posicion_Actual;

	Vector3 Position;
	float Speed = 5.0f;				//Velocidad de movimiento Minotauro
	float Mov_H;
	float Mov_V;

	private GameObject Floor;
	public Vector3 Floor_Scale;		// Se usa para tomar las dimensiones del FLOOR y determinar los limites de movimiento del minotauro
	float Floor_Scale_X;
	float Floor_Scale_Z;

	public GameObject FC;
	public GameObject BC;
	public GameObject LC;
	public GameObject RC;

	public Forward_Check F_Check_Script;
	public Back_Check B_Check_Script;
	public Left_Check L_Check_Script;
	public Right_Check R_Check_Script;

	public bool W_Block, A_Block, S_Block, D_Block;

	void Start () {
		
		rigidbody = GetComponent<Rigidbody>();
		nw = GetComponent<NetworkView>();
		Anim_Minotauro = GetComponent<Animator> ();
		Position = transform.position;

		FC = GameObject.FindGameObjectWithTag ("FC");
		F_Check_Script = FC.GetComponent<Forward_Check> ();

		BC = GameObject.FindGameObjectWithTag ("BC");
		B_Check_Script = BC.GetComponent<Back_Check> ();

		LC = GameObject.FindGameObjectWithTag ("LC");
		L_Check_Script = LC.GetComponent<Left_Check> ();

		RC = GameObject.FindGameObjectWithTag ("RC");
		R_Check_Script = RC.GetComponent<Right_Check> ();

		GameObject Floor = GameObject.Find ("Floor");
		Floor_Scale = Floor.transform.localScale;
		Floor_Scale_X = (Floor_Scale.x / 2) - 1;
		Floor_Scale_Z = (Floor_Scale.z / 2) - 1;

	}
	
	// Update is called once per frame
	void Update () {

//		tiempo += Time.deltaTime;
//		Minutos = (int)tiempo / 60;
//		segundos = (int)tiempo - (Minutos * 60);

		Check_Wall ();			//			Bloquea el movimiento en las direcciones donde colisione con bloques

		Mov_H = Input.GetAxis ("Horizontal");
		Mov_V = Input.GetAxis ("Vertical");
		Animator_State = Anim_Minotauro.GetCurrentAnimatorStateInfo (0);		// Obtiene el estado acutual del Animation Controller

//		if (nw.isMine) {
//		if (respawntime <= 0) {

		Movimiento ();

		if( (Mov_H == 0 && Mov_V == 0) ) {
			Anim_Minotauro.SetBool ("Walk", false);
		}
			

		if (Input.GetKeyDown (KeyCode.Space)) {
			
			Anim_Minotauro.SetTrigger ("Attack");
			Vector3 Orientacion = new Vector3 (1, 0, 0);
			Instantiate (Laserbomb, new Vector3(Position.x, Position.y + 1, Position.z), Quaternion.AngleAxis (90, Orientacion));
		}
			
//				Posicion_Actual = transform.position;
//				int sx = 0, sz = 0;
//				if (Rot.y == 0) {
//					sz = 2;
//				} 
//				if (Rot.y == 1) {
//					sz = -2;
//				} 
//				if (Rot.y < 1 && Rot.y > 0) {
//					sx = 2;
//				} 
//				if (Rot.y < 0) {
//					sx = -2;
//				} 
//
//				Posicion_Actual.x = (Mathf.Round (transform.position.x / 2) * 2) + sx;
//				Posicion_Actual.z = (Mathf.Round (transform.position.z / 2) * 2) + sz;
//				Posicion_Actual.y = 2;
//
//				if (Posicion_Actual.x >= 20) {
//					Posicion_Actual.x = 18;
//				}
//				if (Posicion_Actual.x <= -12) {
//					Posicion_Actual.x = -10;
//				}
//				if (Posicion_Actual.z >= 12) {
//					Posicion_Actual.z = 10;
//				}
//				if (Posicion_Actual.z <= -12) {
//					Posicion_Actual.z = -10;
		//				}Check_Wall ()
//				Vector3 qua = new Vector3 (1, 0, 0);
//				Network.Instantiate (Laserbomb, posact, Quaternion.AngleAxis(90,qua),0);
//			}

//			} else {
//				
//				respawntime -= Time.deltaTime;
//				if (respawntime <= 0) {
//					transform.position = (new Vector3 (-10, 0, -10));
//					inmunity = 2;
//					respawntime = 0;
//
//				}
//
//				float redo = Mathf.Round (respawntime);
//				//resptext.text = redo.ToString ();
//			}
//			if (inmunity >= 0) {
//				inmunity -= Time.deltaTime;
				//resptext.text = "Inmunity";
//			}
		}

	void Movimiento () {
	
		if (Animator_State.nameHash != AttackState) {		// Si se esta realizando la animacion de ataque se bloquea el movimiento

			if (Input.GetKey (KeyCode.A) && transform.position == Position && Position.x != -Floor_Scale_X && A_Block == false) {
				transform.eulerAngles = new Vector3 (0, 270, 0);
				Anim_Minotauro.SetBool ("Walk", true);		// Animacion de RunCycle
				Position = Position + new Vector3 (-2, 0, 0);
			}

			if (Input.GetKey (KeyCode.S) && transform.position == Position && Position.z != -Floor_Scale_Z && S_Block == false) {
				transform.eulerAngles = new Vector3 (0, 180, 0);
				Anim_Minotauro.SetBool ("Walk", true);		// Animacion de RunCycle
				Position = Position + new Vector3 (0, 0, -2);
			}

			if (Input.GetKey (KeyCode.D) && transform.position == Position && Position.x != Floor_Scale_X && D_Block == false) {
				transform.eulerAngles = new Vector3 (0, 90, 0);
				Anim_Minotauro.SetBool ("Walk", true);		// Animacion de RunCycle
				Position = Position + new Vector3 (2, 0, 0);
			}

			if (Input.GetKey (KeyCode.W) && transform.position == Position && Position.z != Floor_Scale_Z && W_Block == false) {
				transform.eulerAngles = new Vector3 (0, 0, 0);
				Anim_Minotauro.SetBool ("Walk", true);		// Animacion de RunCycle
				Position = Position + new Vector3 (0, 0, 2);
			}

			transform.position = Vector3.MoveTowards (transform.position, Position, Time.deltaTime * Speed);

		}
	}

	void Check_Wall () {
	
		int Direction = (int) transform.eulerAngles.y;		// Convierte de FLOAT A INT para poder ser evaluado en el Switch

		Clean_WASD_Block ();			/// Limpia los bloqueos de las teclas WASD 

		if (F_Check_Script.Block_Forward == true) {

			switch (Direction) {
			case 0:						// Se encuentra viendo hacia ARRIBA
				W_Block = true;
				break;
			case 270:					// Se encuentre viendo hacia la IZQUIERDA
				A_Block = true;
				break;
			case 180:					// Se encuetra viendo hacia ABAJO
				S_Block = true;
				break;
			case 90:					// Se encuentra viendo hacia la DERECHA
				D_Block = true;
				break;
			}
		}

		if(B_Check_Script.Block_Back == true) {

			switch (Direction) {
			case 0:						// Se encuentra viendo hacia ARRIBA
				W_Block = true;
				break;
			case 270:					// Se encuentre viendo hacia la IZQUIERDA
				A_Block = true;
				break;
			case 180:					// Se encuetra viendo hacia ABAJO
				S_Block = true;
				break;
			case 90:					// Se encuentra viendo hacia la DERECHA
				D_Block = true;
				break;
			}
		}

		if(L_Check_Script.Block_Left == true) {

			switch (Direction) {
			case 0:					// Se encuentra viendo hacia ARRIBA
				A_Block = true;
				break;
			case 270:				// Se encuentre viendo hacia la IZQUIERDA
				S_Block = true;
				break;
			case 180:				// Se encuetra viendo hacia ABAJO
				D_Block = true;
				break;
			case 90:				// Se encuentra viendo hacia la DERECHA
				W_Block = true;
				break;
			}
		}

		if (R_Check_Script.Block_Right == true) {

			switch (Direction) {
			case 0:					// Se encuentra viendo hacia ARRIBA
				D_Block = true;
				break;
			case 270:				// Se encuentre viendo hacia la IZQUIERDA
				W_Block = true;
				break;
			case 180:				// Se encuetra viendo hacia ABAJO
				A_Block = true;
				break;
			case 90:				// Se encuentra viendo hacia la DERECHA
				S_Block = true;
				break;
			}
		}
	}

	void Clean_WASD_Block ()
	{
		W_Block = false;
		A_Block = false;
		S_Block = false;
		D_Block = false;
	}
}
//		}
	
//		if (collision.gameObject.tag == "Laser" && unav==1 && inmunity<=0) {
//			//Intervalotime debe ser 1 + el numero de minutos transcurridos.
//			respawntime = 1f+Minutos*1.5f;;
//
//			unav= 0;
//
//			//resptext.text = respawntime.ToString ();
//
//
