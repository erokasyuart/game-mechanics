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

    private bool CanUpgradeMonster()
    {
        if (monster != null) //if there is a monster
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>(); //gets the script
            MonsterLevel nextLevel = monsterData.GetNextLevel(); //is the method in MonsterData.cas
            if (nextLevel != null) //if there is space to level up
            {
                return true; //then, level up
            }
        }
        return false;
    }

    //Player mouse clicks
    void OnMouseUp(){
        
        if (CanPlaceMonster())//if CanPlaceMonster() returns a null (no monster)
        {   
            //then it will instantiate a prefab at the openspot transform
            //quaternion.identity is default rotation
            //(GameObject) is a cast from inheritance
            monster = (GameObject)Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();//plays an audio once
            audioSource.PlayOneShot(audioSource.clip);
        }
        else if (CanUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().IncreaseLevel(); //takes the method in monsterData()
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
