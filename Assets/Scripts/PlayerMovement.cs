using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    float max_Speed = 7f;
    float scale = .7f;

    public AudioClip foodSound;
    public GameObject foodParticle;
    public LayerMask blockingLayer;
    public Image hungerBar;

    int Eaten;
    static int TIER_SHIFT = 35;

    private bool move = false;
    public bool isDead = false;
    private RaycastHit2D hit;
    Rigidbody2D rbody;

    // Use this for initialization
    void Start()
    {
        Eaten = 0;
        rbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Food"){
            Eat();
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
        }
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }
    void Grow(){
        if(Eaten> TIER_SHIFT*4)
            transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        if (Eaten > TIER_SHIFT*3)
            transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
        else if (Eaten >= TIER_SHIFT*2)
            transform.localScale = new Vector3(1.9f, 1.9f, 1.9f);
        else if (Eaten >= TIER_SHIFT)
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    void RotateCharacter(float hori, float vert)
    {
        if(vert > 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 0f); }
        else if(vert < 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 180f); }
        else if(hori > 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 270f); }
        else if(hori < 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 90f); }

    }

    public void Eat(){
        Eaten++;
        hungerBar.fillAmount = 1.0f;
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
