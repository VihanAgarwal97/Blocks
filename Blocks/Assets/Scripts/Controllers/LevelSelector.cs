using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    //List of all the levels
    public GameObject level_button;
    //Reference to the Transform of the grid
    public Transform level_select_grid;

    //On start
    void Awake()
    {
        updatePane();
    }

    //Load a scene with the passed build index
    public void loadLevel(int build)
    {
        //set the current level of the instance
        GameController.instance.setCurrentLevel(build);
        //load the level
        SceneManager.LoadScene(build);

    }
    
    //Referesh the buttons on the UI
    public void updatePane()
    {
        //Get a player prefs stored maximum level
        int levelReached = PlayerPrefs.GetInt("PLAYER_LEVEL_REACHED", 1);

        for (int i = 0; i < GameController.instance.NUM_OF_LEVELS; i++)
        {
            //Instatiate a new button as part of the grid
            GameObject levelButtonPrefab = Instantiate(level_button, level_select_grid);

            //Reference to the button of the gameobject
            Button levelButton = levelButtonPrefab.GetComponent<Button>();

            //Update the text of the button
            levelButton.GetComponentInChildren<Text>().text = (i + 1).ToString();

            //Update the onclick event
            int buildIndex = i + 2; 
            levelButton.onClick.AddListener(() => loadLevel(buildIndex));
            

            //if the level button is higher than our level
            if (i+1 > levelReached)
                //lock the level button
                levelButton.interactable = false;

        }
    }
}
