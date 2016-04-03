using UnityEngine;
using System.Collections;

public class DestroyBomb : MonoBehaviour {
	private float tiempo;
	public GameObject Laser1, Laser2, Laser3, Laser4;
	// Use this for initialization
	void Start () {
		tiempo = 3;
	}
	
	// Update is called once per frame
	void Update () {

		if (tiempo < 0) {
			
//			Network.Instantiate (Laser1, transform.position, Quaternion.identity,0);
			Instantiate(Laser1, transform.position, Quaternion.identity);

//			Network.Instantiate (Laser2, transform.position, Quaternion.identity,0);
			Instantiate (Laser2, transform.position, Quaternion.identity);

//			Network.Instantiate (Laser3, transform.position, Quaternion.identity,0);
			Instantiate (Laser3, transform.position, Quaternion.identity);

//			Network.Instantiate (Laser4, transform.position, Quaternion.identity,0);
			Instantiate (Laser4, transform.position, Quaternion.identity);

			Destroy (gameObject);
		}
		tiempo -= Time.deltaTime;
	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "CuboDestructible" || collision.gameObject.tag == "CuboIndestructible" || collision.gameObject.tag == "Limiver" || collision.gameObject.tag == "Limihori") {
			
			Destroy (gameObject);

		}

	}
}
