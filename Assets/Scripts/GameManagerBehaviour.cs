using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour
{
    //gold
    public Text goldLabel;
    private int gold;
    public int Gold
    { 
        get { return gold; } set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "GOLD: " + gold;
        }
    }
    //enemy waves
    public Text waveLabel;
    public GameObject[] nextWaveLabels;
    public bool gameOver = false;
    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1); //+1 because it should start at 1 on screen
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Gold = 1000;
        Wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
