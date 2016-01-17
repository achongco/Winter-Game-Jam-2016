using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class foodSpawner : MonoBehaviour {

	public static foodSpawner current;
	private int spawnedFood = 0;
	//private int spawnedSpecialFood = 0;
	//private int maxSpecial = 4;
	private int maxFood = 100;
	private Stack<GameObject> foodPool;
	private foodScript foodCode;
	private Object basicFood;
	private GameObject[] specialTiles;
	private GameObject[] regularTiles;

	private List<Transform> specialTransforms;
	private List<Transform> regularTransforms;

	public Sprite[] foodSprites;

	void Awake() {
		current = this;
		specialTransforms = new List<Transform>();
		regularTransforms = new List<Transform>();
		//specialTiles = new GameObject[20];
		//regularTiles = new GameObject[500];

		basicFood = Resources.Load ("Food");
		foodPool = new Stack<GameObject> ();
		for (int i = 0; i < maxFood; ++i) {
			returnFood((GameObject)Instantiate(basicFood));
			++spawnedFood; //compensate for returnFood
		}

		specialTransforms = new List<Transform> ();
		regularTransforms = new List<Transform> ();

		//grab tile objects
		specialTiles = GameObject.FindGameObjectsWithTag("specialFoodSpot");
		//Debug.Log ("specialTiles size " + specialTiles.Length);
		regularTiles = GameObject.FindGameObjectsWithTag ("foodSpot");
		//Debug.Log ("regularTiles size " + regularTiles.Length);


		//add tranforms to lists
		for (int i = 0; i < specialTiles.Length; ++i) {
			specialTransforms.Add (specialTiles [i].transform);
		}
		//Debug.Log ("SpecialTransforms size " + specialTransforms.Count);

		for (int i = 0; i < regularTiles.Length; ++i) {
			regularTransforms.Add (regularTiles [i].transform);
			//Debug.Log (regularTiles [i].transform.position);
		}
		//Debug.Log ("regularTransforms size " + regularTransforms.Count);

	}

	// Use this for initialization
	void Start () {
		

		
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnedFood < maxFood) {
			spawnFood ();

		}

	}

	public void returnFood (GameObject obj)
	{
		obj.SetActive (false);
		foodScript script = obj.GetComponent<foodScript> ();
		if (script.type == foodType.TURKEY || script.type == foodType.COW) {
			specialTransforms.Add (obj.transform);
		} else {
				regularTransforms.Add (obj.transform);
		}
		obj.transform.parent = transform;
		obj.name = "Pooled Food";
		foodPool.Push (obj);
		spawnedFood--;
	}

	private GameObject getFood()
	{
		if (foodPool.Count == 0) {
			Debug.Log ("Whoops");
			return null;
		}
		return foodPool.Pop ();
	}


	private void spawnFood()
	{
		int numOfFoodTypes = System.Enum.GetValues (typeof(foodType)).Length;
		if (foodSprites.Length < numOfFoodTypes) {
			Debug.Log ("Not enough food types defined");
			return;
		}

		Vector2 spawn = Vector2.zero;

		GameObject food = getFood ();
		food.SetActive (true);

		//change this
		//food.transform.position = new Vector2 (Random.Range (-10, 10), Random.Range (-10, 10));

		foodScript script = food.GetComponent<foodScript> ();


		//check if its going to be special
		Random rnd = new Random();
		bool isSpecial = (0 == (int)Random.Range (0, 10));

		if (isSpecial && specialTransforms.Count > 0) {
			if (Random.Range (0, 5) == 0) {
				script.type = foodType.COW;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.COW];
				int randIndx = (int)Random.Range (0, specialTransforms.Count);
				//Debug.Log ("cow accessing " + randIndx);
				food.transform.position = specialTransforms [randIndx].position + new Vector3(0.5f, -0.5f, 0);

				specialTransforms.RemoveAt (randIndx);
			} else {
				script.type = foodType.TURKEY;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.TURKEY];
				int randIndx = (int)Random.Range (0, specialTransforms.Count);
				//Debug.Log ("turkey accessing " + randIndx);
				food.transform.position = specialTransforms [randIndx].position + new Vector3(0.5f, -0.5f, 0);
				specialTransforms.RemoveAt (randIndx);
			}
		} else if (regularTransforms.Count > 0) {
			if (Random.Range (0, 5) == 0) {
				script.type = foodType.HOTDOG;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.HOTDOG];
				int randIndx = (int)Random.Range (0, regularTransforms.Count);
				//Debug.Log ("hotdog accessing " + randIndx);
				food.transform.position = regularTransforms [randIndx].position + new Vector3(0.5f, -0.5f, 0);

				regularTransforms.RemoveAt (randIndx);
			} else {
				script.type = foodType.HAMBURGER;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.HAMBURGER];
				int randIndx = (int)Random.Range (0, regularTransforms.Count);
				//Debug.Log ("hamburger accessing " + randIndx);
				food.transform.position = regularTransforms [randIndx].position + new Vector3(0.5f, -0.5f, 0);
				//Debug.Log (food.transform.position);

				regularTransforms.RemoveAt (randIndx);
			}
		}

//		int i = Random.Range (0, numOfFoodTypes);
//		script.type = (foodType)i;
//		food.name = script.type.ToString();
//		script.sr.sprite = foodSprites [i];

		script.initializeVars ();

		++spawnedFood;

	}



}
