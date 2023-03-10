using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Level of the Monster
//groups the important things together
//like a subclass
[System.Serializable] //instances of the class editable in inspector
public class MonsterLevel{
    public int cost;
    public GameObject visualisation;
}

public class MonsterData : MonoBehaviour
{
    public List<MonsterLevel> levels; //references the class
    private MonsterLevel currentLevel; //private current level

    public MonsterLevel CurrentLevel{ //public current level
    //used for getting and setting the private current level
        get { 
            return currentLevel;
            }
        set {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel); //takes whatever the index is ofcurrent level

            GameObject levelVisualisation = levels[currentLevelIndex].visualisation;

            for (int i=0; i < levels.Count; i++){
                if (levelVisualisation != null){
                    if (i == currentLevelIndex){
                        levels[i].visualisation.SetActive(true);
                    }
                    else{
                        levels[i].visualisation.SetActive(false);
                    }
                }
            }
        }
    }

    //starts monsters level at 1 when placed
    void OnEnable(){
        CurrentLevel = levels[0];
    }

    //gets the currentlevel and compares it to the last list position
    public MonsterLevel GetNextLevel(){
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;

        //if its not reached the last upgrade then return it
        if (currentLevelIndex < maxLevelIndex){
            return levels[currentLevelIndex+1];
        }
        else{
            return null;
        }
    }

    public void IncreaseLevel(){
        int currentLevelIndex - levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1){
            CurrentLevel = levels[currentLevelIndex+1];
        }
    }
}
