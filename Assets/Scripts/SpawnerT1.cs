using UnityEngine;
using System.Collections;

public class SpawnerT1 : MonoBehaviour {
	public GameObject Minotaur;
	// Use this for initialization
	void Start () {
		Instantiate (Minotaur, new Vector3 (-10, 2, -10), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
