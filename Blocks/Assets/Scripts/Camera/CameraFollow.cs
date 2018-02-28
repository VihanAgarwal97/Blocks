using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //Reference to the player's position
    public GameObject playerToFollow;
    //Offset for the camera
    public Vector3 camera_offset = new Vector3(1f,3.5f,-3f);
    //Speed of lerp
    public float smoothSpeed = 2.5f;
    //How long do we show the level
    public float levelShowTime = 3.5f;
    //Start position of the Camera
    private Vector3 startPos = new Vector3(2.77f, 21.2f, -3f);
    //Position that shows the whole level
    private Vector3 showPos = new Vector3(2.77f,2.27f, -3f);
    //Is the camera currently showing the level?
    private bool isShowingLevel;
    
    //On creation
    void Awake()
    {
        StartShowLevel();
    }

    void OnEnable()
    {
        StartShowLevel();
    }

    void StartShowLevel()
    {
        isShowingLevel = true;
        transform.position = startPos;
        StartCoroutine(showLevel());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 smoothedPos;

        if (isShowingLevel)
        {
            smoothedPos = Vector3.Lerp(transform.position, showPos, smoothSpeed * Time.deltaTime);

        }
        else if (playerToFollow != null && !isShowingLevel)
        {
            Vector3 targetPos = playerToFollow.transform.position + camera_offset;
            smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        } else
        {
            return;
        }

        transform.position = smoothedPos;
    }

    //Shows the player the entire level for a short time
   IEnumerator showLevel()
    {
        yield return new WaitForSeconds(levelShowTime);
        isShowingLevel = false;
    }

}
