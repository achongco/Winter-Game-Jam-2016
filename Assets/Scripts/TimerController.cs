using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public Image hungerBar;

    static float STARVATION_RATE = .0005f;

    void Awake(){
        hungerBar.fillAmount = .5f;
    }

	// Update is called once per frame
	void FixedUpdate () {
        hungerBar.fillAmount -= STARVATION_RATE;
        CheckForDeath();
	}

    void CheckForDeath(){
        if (hungerBar.fillAmount <= 0.0f)
            Debug.Log("DIED FROM STARVATION");
    }
}
