using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public string level = "scenes/Game";


	public void startGame(){
		Application.LoadLevel (1);
	}

}
