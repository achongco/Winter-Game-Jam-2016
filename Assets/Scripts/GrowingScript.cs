using UnityEngine;
using System.Collections;

public class GrowingScript : MonoBehaviour {

    public int eaten;
    float scale = 1.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        Grow();
	}
   
    void Grow()
    {
        if (scale > 5f)
            return;
        scale += .1f;
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }
}
