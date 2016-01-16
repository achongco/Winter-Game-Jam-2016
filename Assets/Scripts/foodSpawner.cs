using UnityEngine;
using System.Collections;

public class foodSpawner : MonoBehaviour {

	public GameObject hamburger;
	private GameObject[] foodPool = new GameObject[20];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; ++i) {
			foodPool[i] = (GameObject)Instantiate (hamburger, new Vector2 (0, 0), Quaternion.identity);
			foodPool [i].SetActive (false);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
