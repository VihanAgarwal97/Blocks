using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hook : MonoBehaviour {

    //Reference to the ball prefab
    public GameObject ballPrefab;
    //Is there currently a ball in play
    private bool isBallActive;
    //The current ball in play
    private GameObject currentBall;
    //Reference to the Game camera
    private Camera gameCamera;

    //On awake assign a new ball to the hook
    void Awake()
    {
        //Refer to the current game camera
        gameCamera = Camera.main;

        //If there is no CameraFollow attached, attach it to this camera
        if (gameCamera.GetComponent<CameraFollow>() == null)
            gameCamera.gameObject.AddComponent<CameraFollow>();
   
        //Create a new ball
        createNewBall();
  
    }

    //Called every frame
    public void Update()
    {
        //If the currentBall has been destroyed
        if(currentBall == null)
        {
            //No ball is active
            isBallActive = false;
        }

        //If there is no ball active
        if (!isBallActive)
        {
            //If there are more balls available
            if(GameController.instance.getBalls() > 0)
            {
                //If there are more objectives
                if(GameController.instance.currentObjectives.Count > 0)
                {
                    //Create a new ball
                    createNewBall();
                }
                else
                {
                    //Level Win
                    LevelWin();

                }
            } else if(GameController.instance.currentObjectives.Count > 0)
            {
                //Fail Level
                LevelFailed();
            } else
            {
                LevelWin();
            }

        }

    }


    //Level Win Function 
    private void LevelWin()
    {
        Debug.Log("Level Complete...");
        //Disable the Camera follow script
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        //Load the new scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    //Level Loss Function
    private void LevelFailed()
    {
        Debug.Log("Level Failed...");
        //Load the level select scene
        SceneManager.LoadScene(1);
    }
	
    //Create a new ball and attach it to the hook
    private void createNewBall()
    {   
        //Instantiate a new ball
        currentBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        //Reduce the number of balls left
        GameController.instance.setBalls(GameController.instance.getBalls() - 1);
        //Make the camera follow new ball
        gameCamera.GetComponent<CameraFollow>().playerToFollow = currentBall;
        //Get the current rigidbody of the hook
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //Connect it to the ball
        currentBall.GetComponent<SpringJoint2D>().connectedBody = rb;
        currentBall.GetComponent<Ball>().rb2D_hook = rb;
        //Ball is active
        isBallActive = true;
    }
}
