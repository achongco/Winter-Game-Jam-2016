using UnityEngine;
using System.Collections;

public class TextArea : MonoBehaviour {

	public GameObject mainButtons;
	public GameObject credits;

	public void dispMainButtons(){
		mainButtons.SetActive (true);
		credits.SetActive (false);
	}

	public void dispCredits(){
		mainButtons.SetActive (false);
		credits.SetActive (true);
	}

}
