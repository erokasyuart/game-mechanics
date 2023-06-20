using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public GameObject enemy2Prefab;
    
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;

    public Wave[] waves; //Wave class
    public int timeBetweenWaves = 5;

    private GameManagerBehaviour gameManager; //the game manager script

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length) //if it is NOT the last wave
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;

            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) //If no enemies have spawned and the time interval has ended
            || (enemiesSpawned != 0 && timeInterval > spawnInterval)) //or there are no enemies and the time passed is more than the enemy spawn interval
            && (enemiesSpawned < waves[currentWave].maxEnemies)) //and the enemies spawned are less than the current wave's max # of enemies
            {
                lastSpawnTime = Time.time; //then reset the lastSpawnTime
                GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab); //instantiate a new enemy
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints; //set their waypoints to the waypoints.
                enemiesSpawned++; //increase the enemy spawn
            }
            //if there are max number of enemies and there are no enemies with a tag (aka, all gone!)
            if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++; //increase the wave
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f); //give the player some gold
                enemiesSpawned = 0; //reset the enemies spawned to 0
                lastSpawnTime = Time.time; //reset the lastspawntime
            }
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
        
    }
}
