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

	public Sprite[] foodSpites;

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


		}
	}

	private void returnFood (GameObject obj)
	{
		obj.transform.parent = transform;
		obj.SetActive (false);
		obj.name = "Pooled Food";
		foodPool.Push (obj);
		spawnedFood--;
	}

	private void spawnFood()
	{
		
	}



}
