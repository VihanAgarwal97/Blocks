using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //Variable to store state of mouse
    private bool isMouseDown = false;
    //Reference to the Rigidbody
    private Rigidbody2D rb2D;
    //Reference to Rigidbody of the Hook
    [SerializeField]
    public Rigidbody2D rb2D_hook;
    //How long until we release the ball
    public float releaseTime = .15f;
    //How far can we pull the ball
    public float maxDragDist = 2.5f;
    //How long until we spawn a new ball
    public float nextBallTime = 5f;


    // Use this for initialization
    void Start()
    {
        //Get the RigidBody2D attached to the current game object
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If the mouse is down
        if (isMouseDown)
        {
            //Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //If it is outside the max distance then dont let it move
            if (Vector3.Distance(mousePosition, rb2D_hook.position) > maxDragDist)
            {
                rb2D.position = rb2D_hook.position + (mousePosition - rb2D_hook.position).normalized * maxDragDist;
            }
            else
            {
                //Otherwise update its postion to the mouse
                rb2D.position = mousePosition;
            }
        }
    }

    //On mouse down, set isMouseDown true; and make the Rigidbody2D Kinematic
    void OnMouseDown()
    {
        isMouseDown = true;
        rb2D.isKinematic = true;
    }

    //On mouse up, set isMouseDown false; and release the ball
    void OnMouseUp()
    {
        isMouseDown = false;
        rb2D.isKinematic = false;

        //Function to release the ball
        StartCoroutine(releaseBall());
    }

    //Release the ball on mouse up
    IEnumerator releaseBall()
    {
        //Wait for release time
        yield return new WaitForSeconds(releaseTime);
        //Disable the spring joint on the ball
        GetComponent<SpringJoint2D>().enabled = false;
        //Disable ball
        this.enabled = false;
        

        //Wait for nextBallTime
        yield return new WaitForSeconds(nextBallTime);
        //Destroy ball
        Destroy(gameObject);
        
    }
}