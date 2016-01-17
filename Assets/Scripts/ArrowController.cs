using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

    public GameObject player;
    public GameObject[] specialLocations;

    // Use this for initialization
    void Start()
    {
        specialLocations = GameObject.FindGameObjectsWithTag("specialFoodSpot");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
