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
	public Transform texture;
	public foodType type;
	public BoxCollider2D theBox;
	public SpriteRenderer sr;
    //private Vector3 origTextScale;

    public ArrowController acScript;

    void Awake() {
        acScript = GameObject.FindGameObjectWithTag("Arrow").GetComponent<ArrowController>();

        texture = transform.Find ("Texture");
		foodSpawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<foodSpawner>();
		texture = transform.Find("Texture").transform;
		theBox.enabled = true;
		sr = texture.GetComponent<SpriteRenderer> ();

//		if (type == foodType.COW) {
//			texture.transform.localScale += new Vector3 (0.3f, 0.3f, 0.0f);
//		}

		//origTextScale = texture.localScale;
	}

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D hamburgerCollision)
	{
		if (hamburgerCollision.gameObject.tag == "Player") {
            if(gameObject.name == "COW"|| gameObject.name == "TURKEY")
            {
                acScript.specialPositions.Remove(transform.position);
            }

			transform.position -= new Vector3 (0.5f, -0.5f, 0);

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
		else if (type == foodType.COW) {
			foodVal = 30000;
			texture.localScale += new Vector3 (0.3f, 0.3f, 0.0f);
		}


	}

	public int getFoodVal()
	{
		return foodVal;
	}
}
