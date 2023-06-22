using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{
    public GameObject monsterPrefab; // the monster prefab
    private GameObject monster; //created monster

    private GameManagerBehaviour gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>(); //finds the objects and then its component(script)
    }

    //checks if the monster is variable is null
    //prevents duplication
    private bool CanPlaceMonster(){
        int cost = monsterPrefab.GetComponent<MonsterData>().levels[0].cost;
        return monster == null && gameManager.Gold >= cost; //can only place a monster if theres a space available and they can afford it
    }

    //allows the player to upgrade the monster
    private bool CanUpgradeMonster()
    {
        if (monster != null) //if there is a monster
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>(); //gets the script
            MonsterLevel nextLevel = monsterData.GetNextLevel(); //is the method in MonsterData.cas
            if (nextLevel != null) //if there is space to level up
            {
                return gameManager.Gold >= nextLevel.cost; //then, level up
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
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost; //takes away the value of the element
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();//plays an audio once
            audioSource.PlayOneShot(audioSource.clip);
        }
        else if (CanUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().IncreaseLevel(); //takes the method in monsterData()
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost; //takes away the value of the element
        }
    }
}
