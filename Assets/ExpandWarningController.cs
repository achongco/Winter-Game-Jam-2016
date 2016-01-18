using UnityEngine;
using System.Collections;

public class ExpandWarningController : MonoBehaviour {

	public PlayerMovement pm;
	public GameObject disp;

	// Update is called once per frame
	void Update () {
		disp.SetActive ((pm.getEaten()-1) % PlayerMovement.TIER_SHIFT > PlayerMovement.TIER_SHIFT -4 &&
			pm.getEaten() < 75);
	}
}
