using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class foodSpawner : MonoBehaviour {

	public static foodSpawner current;
	private int spawnedFood = 0;
	private int maxFood = 100;
	private Stack<GameObject> foodPool;
	private foodScript foodCode;
	private Object basicFood;

	public Sprite[] foodSprites;

	void Awake() {
		current = this;
		basicFood = Resources.Load ("Food");
		foodPool = new Stack<GameObject> ();
		for (int i = 0; i < maxFood; ++i) {
			returnFood((GameObject)Instantiate(basicFood));
			++spawnedFood; //compensate for returnFood
		}
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
		obj.transform.parent = transform;
		obj.SetActive (false);
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


		//Decide where it's going to spawn

		GameObject food = getFood ();
		food.SetActive (true);
		food.transform.position = new Vector2 (Random.Range (-10, 10), Random.Range (-10, 10));

		foodScript script = food.GetComponent<foodScript> ();


		//check if its going to be special
		Random rnd = new Random();
		bool isSpecial = (0 == (int)Random.Range (0, 10));

		if (isSpecial) {
			if (Random.Range (0, 5) == 0) {
				script.type = foodType.COW;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.COW];
			} else {
				script.type = foodType.TURKEY;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.TURKEY];
			}
		} else {
			if (Random.Range (0, 5) == 0) {
				script.type = foodType.HOTDOG;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.HOTDOG];
			} else {
				script.type = foodType.HAMBURGER;
				food.name = script.type.ToString ();
				script.sr.sprite = foodSprites [(int)foodType.HAMBURGER];
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
