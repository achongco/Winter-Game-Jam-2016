using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowController : MonoBehaviour {

    public GameObject player;
    public GameObject animalSpriteObj;
    public Sprite[] animalSprites;
    GameObject[] specialLocations;
    public List<Vector3> specialPositions = new List<Vector3>();
    public List<string> animalType = new List<string>();
    int index_of_closests;


    // Use this for initialization
    void Start()
    {
        specialLocations = GameObject.FindGameObjectsWithTag("specialFoodSpot");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if(specialPositions.Count > 0 && Vector2.Distance(transform.position, GetClosests()) < 15f)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Vector3 dir = specialPositions[index_of_closests] - transform.position;
            SetPointerPortrait();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90f, Vector3.forward);

        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void SetPointerPortrait(){
        switch (animalType[index_of_closests])
        {
            case "COW":
                animalSpriteObj.transform.localScale= new Vector3(.7f, .7f, .7f);
                animalSpriteObj.GetComponent<SpriteRenderer>().sprite = animalSprites[0];
                break;
            case "TURKEY":
                animalSpriteObj.transform.localScale = new Vector3(.15f, .15f, .15f);
                animalSpriteObj.GetComponent<SpriteRenderer>().sprite = animalSprites[1];
                break;
        }

        Debug.Log(animalType[index_of_closests]);
    }

    Vector3 GetClosests()
    {
        index_of_closests = 0;

        for(int i = 1; i<specialPositions.Count; i++){
            if(Vector2.Distance(transform.position, specialPositions[i]) < Vector2.Distance(transform.position, specialPositions[index_of_closests]))
            {
                index_of_closests = i;
            }
        }
        
        return specialPositions[index_of_closests];
    }

    public void RemoveItem(Vector3 pos)
    {
        int index = specialPositions.IndexOf(pos);
        specialPositions.Remove(pos);
        animalType.RemoveAt(index);
    }
}
