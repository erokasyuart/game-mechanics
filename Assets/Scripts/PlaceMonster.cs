using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{
    public GameObject monsterPrefab; // the monster prefab
    private GameObject monster; //created monster
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //checks if the monster is variable is null
    //prevents duplication
    private bool CanPlaceMonster(){
        return monster == null;
    }

    //Player mouse clicks
    void OnMouseUp(){
        //if CanPlaceMonster() returns a null (no monster)
        if (CanPlaceMonster()){
            //then it will instantiate a prefab at the openspot transform
            //quaternion.identity is default rotation
            //(GameObject) is a cast from inheritance
            monster = (GameObject)Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            //plays an audio once
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
