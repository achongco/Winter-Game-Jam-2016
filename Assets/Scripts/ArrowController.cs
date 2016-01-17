using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowController : MonoBehaviour {

    public GameObject player;
    GameObject[] specialLocations;
    public List<Vector3> specialPositions = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
        specialLocations = GameObject.FindGameObjectsWithTag("specialFoodSpot");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if(specialPositions.Count > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Vector3 dir = GetClosests() - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90f, Vector3.forward);

        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    Vector3 GetClosests()
    {
        int index_of_closests = 0;

        for(int i = 1; i<specialPositions.Count; i++){
            if(Vector2.Distance(transform.position, specialPositions[i]) < Vector2.Distance(transform.position, specialPositions[index_of_closests]))
            {
                index_of_closests = i;
            }
        }
        Debug.Log(specialPositions[index_of_closests]);
        return specialPositions[index_of_closests];
    }
}
