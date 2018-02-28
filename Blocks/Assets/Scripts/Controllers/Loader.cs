using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

    //Reference to the Game Manager
    public GameObject gameController;

    //On Awake
    void Awake()
    {
        //Don't destroy this on loading a new scene
        DontDestroyOnLoad(this);

        //Initialize a new game manager
        initGameManager();

        //Subscribe to SceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        //TEST MODE
        PlayerPrefs.DeleteAll();

    }
	

    //Initialize the Game manager
    void initGameManager()
    {
        //If there is no game manager present
        if (GameController.instance == null)
        {
            //Instantiate a new Game Manager
            Instantiate(gameController);
        }
    }

    //When the scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            GameController.instance.setCurrentLevel(0);

        }

        else if (scene.name == "LevelSelect")
        {
            GameController.instance.setCurrentLevel(0);
        }
        else
        {
            //Reset the number of balls
            GameController.instance.setBalls(5);
            //Enable the Camera follow
            Camera.main.GetComponent<CameraFollow>().enabled = true;
            //Set the current level to the build index - 1
            GameController.instance.setCurrentLevel(scene.buildIndex - 1);
            //if the current level is greater than the highest level the player has reached
            if (GameController.instance.getCurrentLevel() > PlayerPrefs.GetInt("PLAYER_LEVEL_REACHED"))
            {
                //set the highest level to the current level
                PlayerPrefs.SetInt("PLAYER_LEVEL_REACHED", GameController.instance.getCurrentLevel());
            }


        }

    }

    //Load level with BuildIndex passed
    public void loadLevel(int level)
    {
        Debug.Log("Loading level " + level + "...");
        SceneManager.LoadScene(level);
    }


    //Exit the Game
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }




}
