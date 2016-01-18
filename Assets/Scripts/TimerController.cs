using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public Image hungerBar;
    public Text scoreText;
    public Text finalScoreText;
    public GameObject player;
    public GameObject GameOverScreen;
    public AudioClip gameOver;
    public int score;

    bool deathRunning = false;
    static float STARVATION_RATE = .0027f;

    void Awake(){
        hungerBar.fillAmount = 1f;
    }
    
    void Update()
    {
        scoreText.text = string.Format("{0:n0}", score);
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (score > 0) {
			hungerBar.fillAmount -= STARVATION_RATE;
		}
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
        finalScoreText.text = string.Format("Calories: {0:n0}", score);
        player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isDead");
        player.transform.GetComponent<PlayerMovement>().isDead = true;
        yield return new WaitForSeconds(1f);
        GameOverScreen.SetActive(true);
        gameObject.GetComponent<AudioSource>().clip = gameOver;
        gameObject.GetComponent<AudioSource>().loop = false;
        gameObject.GetComponent<AudioSource>().Play();
        
    }
}
