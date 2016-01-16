using UnityEngine;
using System.Collections;

public class foodScript : MonoBehaviour {

	public int foodVal;
	public foodSpawner foodSpawn;

	// Use this for initialization
	void Start () {
		foodSpawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<foodSpawner>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D hamburgerCollision)
	{
		if (hamburgerCollision.gameObject.tag == "Player") {
			//foodSpawn.counter--;
			gameObject.SetActive (false);
		}
	}
}
