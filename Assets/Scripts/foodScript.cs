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
	private Vector3 origTextScale;

	void Awake() {
		texture = transform.Find ("Texture");
		foodSpawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<foodSpawner>();
		texture = transform.Find("Texture").transform;
		theBox.enabled = true;
		sr = texture.GetComponent<SpriteRenderer> ();

		origTextScale = texture.localScale;
	}

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D hamburgerCollision)
	{
		if (hamburgerCollision.gameObject.tag == "Player") {
			transform.position -= new Vector3 (0.5f, 0.5f, 0);
			foodSpawner.current.returnFood(gameObject);
		}
	}

	public void initializeVars()
	{
		//texture.localScale = origTextScale * theBox.size;
		sr.enabled = true;
		theBox.enabled = true;

		theBox.isTrigger = true;

		if (type == foodType.HAMBURGER)
			foodVal = 1000;
		else if (type == foodType.HOTDOG)
			foodVal = 1250;
		else if (type == foodType.TURKEY)
			foodVal = 20000;
		else if (type == foodType.COW)
			foodVal = 30000;
	}

	public int getFoodVal()
	{
		return foodVal;
	}
}
