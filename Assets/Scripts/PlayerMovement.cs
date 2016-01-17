using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float time;
    public float distance;
    public float max_Speed = 5f;
    float scale = .7f;

    public LayerMask blockingLayer;
    public Image hungerBar;

    TimerController tcScript;

    private bool move = false;
    private RaycastHit2D hit;
    Rigidbody2D rbody;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        tcScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimerController>();
        Grow();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Directional command handlers
        float move_x = Input.GetAxis("Horizontal");    //Get input for x-axis
        float move_y = Input.GetAxis("Vertical");      //Get input for y-axis
        if (move_x != 0f)
            move_y = 0f;
        RotateCharacter(move_x, move_y);
        GetComponent<Rigidbody2D>().velocity = new Vector2(move_x * max_Speed, move_y * max_Speed);
    }
    void Grow(){
        if(tcScript.score>=280000)
            transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        if (tcScript.score>=210000)
            transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
        else if (tcScript.score>=140000)
            transform.localScale = new Vector3(1.9f, 1.9f, 1.9f);
        else if (tcScript.score>=70000)
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    void RotateCharacter(float hori, float vert)
    {
        if(vert > 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 0f); }
        else if(vert < 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 180f); }
        else if(hori > 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 270f); }
        else if(hori < 0f) { transform.rotation = Quaternion.Euler(0f, 0f, 90f); }

    }

    public void Eat(float fillValue){
        hungerBar.fillAmount += fillValue;
        Grow();
    }



}
