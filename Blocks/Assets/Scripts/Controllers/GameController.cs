using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(this);
        }

        //Don't Destroy this instance on level load
        DontDestroyOnLoad(gameObject);
    }


    //How many balls are available
    private static int NUM_OF_BALLS = 5;
    //How much gold does the player have
    private static int GOLD = 0;
    //List of Active Objectives in the Level
    public List<GameObject> currentObjectives;
    //The current level running on this instance
    private static int CURRENT_LEVEL;
    //Array to hold all the levels in the game
    public int NUM_OF_LEVELS = 10;


    //Set CURRENT_LEVEL
    public void setCurrentLevel(int level)
    {
        CURRENT_LEVEL = level;
    }

    //Get CURRENT_LEVEL
    public int getCurrentLevel()
    {
        return CURRENT_LEVEL;
    }

    //Set NUM_OF_BALLS
    public void setBalls(int balls)
    {
        NUM_OF_BALLS = balls;
    }

    //Get NUM_OF_BALLS
    public int getBalls()
    {
        return NUM_OF_BALLS;
    }

    //Get GOLD
    public int getGold()
    {
        return GOLD;
    }

    //Add passed value to GOLD
    public void addGold(int gold)
    {
        GOLD += gold;
    }

    //Set GOLD
    public void setGold(int gold)
    {
        GOLD = gold;
    }
}
