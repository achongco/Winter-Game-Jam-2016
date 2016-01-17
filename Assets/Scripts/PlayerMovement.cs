using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    float max_Speed = 9f;
    float scale = .7f;

    public AudioClip foodSound;
    public GameObject foodParticle;
    public LayerMask blockingLayer;
    public Image hungerBar;

	public TimerController sb;

    int Eaten;
	static int startEaten = 10;
	static int TIER_SHIFT = 25;

    private bool move = false;
    public bool isDead = false;
    private RaycastHit2D hit;
    Rigidbody2D rbody;

	//colision testing equipment
	//GameObject tester;
	CircleCollider2D testColider;
	Vector3? delayedGrow = null;

    // Use this for initialization
    void Start()
    {
		Eaten = startEaten;
        rbody = GetComponent<Rigidbody2D>();
		//set up the tester
		//tester = new GameObject ();
		//testColider = tester.AddComponent<CircleCollider2D> ();
		//testColider.radius = GetComponent<CircleCollider2D> ().radius + 0.1f;
		testColider = GetComponentInChildren<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Food"){
			Eat (col.gameObject.GetComponent<foodScript> ());
            FoodParticle();
            FoodSound();
            Debug.Log(Eaten);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Directional command handlers
        if (!isDead)
        {
            float move_x = Input.GetAxis("Horizontal");    //Get input for x-axis
            float move_y = Input.GetAxis("Vertical");      //Get input for y-axis
            if (move_x != 0f)
                move_y = 0f;
            RotateCharacter(move_x, move_y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(move_x * max_Speed, move_y * max_Speed);
			//address cached growths
			//tester.transform.position = transform.position;
			if (delayedGrow != null) {
				GrowTo (delayedGrow);
			}
        }
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }

    void Grow(){
        if(Eaten> TIER_SHIFT*4)
			GrowTo(new Vector3(3.0f, 3.0f, 3.0f));
        else if (Eaten > TIER_SHIFT*3)
			GrowTo(new Vector3(2.7f, 2.7f, 2.7f));
        else if (Eaten >= TIER_SHIFT*2)
			GrowTo(new Vector3(1.9f, 1.9f, 1.9f));
        else if (Eaten >= TIER_SHIFT)
			GrowTo(new Vector3(1.3f, 1.3f, 1.3f));
    }

	void GrowTo(Vector3? scale){
		Debug.Log ("Growing");
		//makes shure the player will not grow into a collision before they grow 
		testColider.transform.localScale = (Vector3)scale;
		if (!testColider.IsTouchingLayers (8)) {
			GameObject throwAway = new GameObject();
			//throwAway.transform.position = testColider.transform.position;
			//throwAway.AddComponent<CircleCollider2D>().radius = testColider.radius;
			transform.localScale = (Vector3)scale;
			delayedGrow = null; //remove the need to grow
			//debug
		} else {
			delayedGrow = scale; //stash the scale to continue trying
		}
	}

    void RotateCharacter(float hori, float vert)
    {
        if(vert > 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 0f); }
        else if(vert < 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 180f); }
        else if(hori > 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 270f); }
        else if(hori < 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 90f); }

    }

	public void Eat(foodScript food){
        Eaten++;
        hungerBar.fillAmount = 1.0f;
		sb.score += food.getFoodVal ();
        Grow();
    }


    void FoodParticle()
    {
        Object particle = Instantiate(foodParticle, transform.position, transform.rotation);
        Destroy(particle, 1);

    }

    void FoodSound()
    {
        AudioSource.PlayClipAtPoint(foodSound, transform.position);
    }

}
