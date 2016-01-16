using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float time;
    public float distance;

    private bool move = false;
    Rigidbody2D rbody;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        int horizontal = 0;
        int vertical = 0;

        vertical = (int)Input.GetAxisRaw("Vertical");
        horizontal = (int)Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            vertical = 0;
        }

        Debug.Log(vertical);
        Debug.Log(horizontal);

        if (move == false)
        {
            if (horizontal != 0 || vertical != 0)
            {
                Vector2 movement = new Vector2(horizontal, vertical);
                Vector2 start = transform.position;
                Vector2 end = start + movement * distance;
                Debug.Log(end);
                StartCoroutine(MyCoroutine(end));

            }
        }

    }

    IEnumerator MyCoroutine(Vector3 end)
    {
        move = true;

        float rate = 1.0f / time;

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;


        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPostion = Vector3.MoveTowards(rbody.position, end, rate * Time.deltaTime);
            rbody.MovePosition(newPostion);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        move = false;
    }


}
