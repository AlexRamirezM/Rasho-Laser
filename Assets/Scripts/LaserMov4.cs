using UnityEngine;
using System.Collections;

public class LaserMov4 : MonoBehaviour {
	private float velz = -20;
	private float velx = 0;
	Quaternion rot = new Quaternion ();
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 mov = new Vector3 (velx,0,velz);
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

		if (collision.gameObject.tag == "Espejo")
		{
			//Cambia de dirección 90 grados hacia arriba o hacia abajo dependiendo la dirección del espejo

		}


	}

}
