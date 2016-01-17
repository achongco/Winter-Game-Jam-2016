using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public string level = "scenes/Game1";


	public void startGame(){
		Application.LoadLevel (level);
	}

}
