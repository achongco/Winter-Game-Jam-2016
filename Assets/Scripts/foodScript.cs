using UnityEngine;
using System.Collections;

public enum foodType {
	HAMBURGER,
	HOTDOG,
	TURKEY,
	COW
}

public class foodScript : MonoBehaviour {

	public int foodVal;
	public foodSpawner foodSpawn;
	private Transform texture;
	public foodType type;
	public BoxCollider2D theBox;
	public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		foodSpawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<foodSpawner>();
		texture = transform.Find("Texture").transform;
		theBox.enabled = true;
		sr = texture.GetComponent<SpriteRenderer> ();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D hamburgerCollision)
	{
		if (hamburgerCollision.gameObject.tag == "Player") {
			//foodSpawn.counter--;
			foodSpawner.current.returnFood(gameObject);
		}
	}

	public void initializeVars()
	{
		if (type == foodType.HAMBURGER)
			foodVal = 1000;
		else if (type == foodType.HOTDOG)
			foodVal = 1250;
		else if (type == foodType.TURKEY)
			foodVal = 20000;
		else if (type == foodType.COW)
			foodVal = 30000;
	}
}
