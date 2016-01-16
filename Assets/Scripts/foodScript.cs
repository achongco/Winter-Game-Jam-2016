using UnityEngine;
using System.Collections;

public enum foodType {
	HAMBURGER,
	HOTDOG,
	TURKEY,
	SPECIAL
}

public class foodScript : MonoBehaviour {

	public int foodVal;
	public foodSpawner foodSpawn;
	private Transform texture;
	public BoxCollider2D theBox;

	// Use this for initialization
	void Start () {
		foodSpawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<foodSpawner>();
		texture = transform.Find("Texture").transform;
		theBox.enabled = true;


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
