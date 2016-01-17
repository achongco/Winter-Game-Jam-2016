﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public Image hungerBar;
    public Text scoreText;
    public GameObject player;
    public GameObject GameOverScreen;
    public int score;

    bool deathRunning = false;
    static float STARVATION_RATE = .0025f;

    void Awake(){
        hungerBar.fillAmount = 1f;
    }
    
    void Update()
    {
        scoreText.text = score.ToString();
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
        player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isDead");
        player.transform.GetComponent<PlayerMovement>().isDead = true;
        yield return new WaitForSeconds(1f);
        GameOverScreen.SetActive(true);
    }
}
