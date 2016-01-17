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

	private List<Vector3> specialPositions;
	private List<Vector3> regularPositions;

	public Sprite[] foodSprites;

	void Awake() {
		current = this;
		specialPositions = new List<Vector3>();
		regularPositions = new List<Vector3>();
		//specialTiles = new GameObject[20];
		//regularTiles = new GameObject[500];

		basicFood = Resources.Load ("Food");
		foodPool = new Stack<GameObject> ();
		for (int i = 0; i < maxFood; ++i) {
			returnFood((GameObject)Instantiate(basicFood));
			++spawnedFood; //compensate for returnFood
		}

		specialPositions = new List<Vector3> ();
		regularPositions = new List<Vector3> ();

		//grab tile objects
		specialTiles = GameObject.FindGameObjectsWithTag("specialFoodSpot");
		//Debug.Log ("specialTiles size " + specialTiles.Length);
		regularTiles = GameObject.FindGameObjectsWithTag ("foodSpot");
		//Debug.Log ("regularTiles size " + regularTiles.Length);


		//add tranforms to lists
		for (int i = 0; i < specialTiles.Length; ++i) {
			specialPositions.Add (specialTiles [i].transform.position);
		}
		//Debug.Log ("SpecialTransforms size " + specialPositions.Count);

		for (int i = 0; i < regularTiles.Length; ++i) {
			regularPositions.Add (regularTiles [i].transform.position);
			//Debug.Log (regularTiles [i].transform.position);
		}
		//Debug.Log ("regularPositions size " + regularPositions.Count);

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
		Debug.Log (obj.transform.position);
		obj.SetActive (false);
		foodScript script = obj.GetComponent<foodScript> ();
		if (script.type == foodType.COW) {
			script.texture.transform.localScale -= new Vector3 (0.3f, 0.3f, 0.0f);
		}
			
		if (script.type == foodType.TURKEY || script.type == foodType.COW) {
			specialPositions.Add (obj.transform.position);
		} else {
				regularPositions.Add (obj.transform.position);
		}
		//obj.transform.parent = transform;
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

		GameObject food = getFood ();
		food.SetActive (true);

		//change this
		//food.transform.position = new Vector2 (Random.Range (-10, 10), Random.Range (-10, 10));

		foodScript script = food.GetComponent<foodScript> ();


		//check if its going to be special
		bool isSpecial = (0 == (int)Random.Range (0, 10));

		if (isSpecial && specialPositions.Count > 0) {
			if (Random.Range (0, 5) == 0) {
				script.type = foodType.COW;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.COW];
				int randIndx = (int)Random.Range (0, specialPositions.Count);
				//Debug.Log ("cow accessing " + randIndx);
				food.transform.position = specialPositions [randIndx] + new Vector3(0.5f, -0.5f, 0);
				specialPositions.RemoveAt (randIndx);
			} else {
				script.type = foodType.TURKEY;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.TURKEY];
				int randIndx = (int)Random.Range (0, specialPositions.Count);
				//Debug.Log ("turkey accessing " + randIndx);
				food.transform.position = specialPositions [randIndx] + new Vector3(0.5f, -0.5f, 0);
				specialPositions.RemoveAt (randIndx);
			}
		} else if (regularPositions.Count > 0) {
			if (Random.Range (0, 5) == 0) {
				script.type = foodType.HOTDOG;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.HOTDOG];
				int randIndx = (int)Random.Range (0, regularPositions.Count);
				//Debug.Log ("hotdog accessing " + randIndx);
				food.transform.position = regularPositions [randIndx] + new Vector3(0.5f, -0.5f, 0);

				regularPositions.RemoveAt (randIndx);
			} else {
				script.type = foodType.HAMBURGER;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.HAMBURGER];
				int randIndx = (int)Random.Range (0, regularPositions.Count);
				//Debug.Log ("hamburger accessing " + randIndx);
				food.transform.position = regularPositions [randIndx] + new Vector3(0.5f, -0.5f, 0);
				//Debug.Log (food.transform.position);

				regularPositions.RemoveAt (randIndx);
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
