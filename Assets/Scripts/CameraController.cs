using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = player.transform.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        gameObject.transform.position = player.transform.position - offset;
          
	}
}
