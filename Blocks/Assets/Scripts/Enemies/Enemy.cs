using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Health of the Enemy
    public float health = 0f;
    //Particle Effect to play on death
    public GameObject DeathParticles = null;
    //Value of the Enemy
    public int value = 0;
    //Type of the enemy
    public string enemyType = "";
   

    //Collision Logic
    protected void OnCollisionEnter2D(Collision2D other)
    {
        //Get the magnitude of the velocity
        float hitMagnitude = other.relativeVelocity.magnitude;
        //Subtract it from the health
        health -= hitMagnitude;
        //If health is less than 0 then kill the enemy
        if(health < 0)
        {
            KillEnemy();
        }
       
    }

    //Kill Enemy Function
    protected void KillEnemy()
    {
        //Instantiate the death effect particles
        GameObject particles = Instantiate(DeathParticles, transform.position, Quaternion.identity);
        
        if(enemyType == "Coin")
        {
            //remove the coin
            GameController.instance.currentObjectives.Remove(gameObject);
            //Add gold to the player
            GameController.instance.addGold(1000);
        } else if(enemyType == "Danger")
        {
            //Remove gold from the player
            GameController.instance.addGold(-250);
        }
        
        //Destroy the Enemy
        Destroy(gameObject);
        //Remove the Particles
        Destroy(particles, 1.25f);
    }


}

