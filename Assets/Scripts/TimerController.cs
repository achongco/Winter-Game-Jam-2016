using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public Image hungerBar;
    public Text scoreText;
    public GameObject player;
    public int score;

    bool deathRunning = false;
    static float STARVATION_RATE = .004f;

    void Awake(){
        hungerBar.fillAmount = .5f;
    }
    
    void Update()
    {
        scoreText.text = score.ToString();
    }

	// Update is called once per frame
	void FixedUpdate () {
        hungerBar.fillAmount -= STARVATION_RATE;
        CheckForDeath();
	}

    

    void CheckForDeath(){
        if (hungerBar.fillAmount <= 0.0f && !deathRunning) {
            StartCoroutine("DeathSequence");
        }
    }

    IEnumerator DeathSequence()
    {
        deathRunning = true;
        player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isDead");
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0.0f;
    }
}
