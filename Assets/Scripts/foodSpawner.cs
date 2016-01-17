using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class foodSpawner : MonoBehaviour {

	public static foodSpawner current;
	private int spawnedFood = 0;
	private int maxFood = 20;
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

		int i = Random.Range (0, numOfFoodTypes);
		script.type = (foodType)i;
<<<<<<< HEAD
		food.name = script.name.ToString();
=======
		food.name = script.type.ToString();
>>>>>>> origin/master
		script.sr.sprite = foodSprites [i];

		script.initializeVars ();

		++spawnedFood;

	}



}
