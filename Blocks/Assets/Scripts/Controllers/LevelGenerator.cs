using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    //Picture to generate the Level from
    public Texture2D map;
    //Color mappings for the Level
    public ColorToPrefab[] colorMappings;
    //Offset to spawn blocks
    public Vector2 offset = new Vector2(0, 0);
    

    //On Initialization
	void Awake()
    {
        GenerateLevel(map);
    }

    //Generate the level based on the Texture2D passed
    void GenerateLevel(Texture2D level)
    {
        //Clear the active objectives
        GameController.instance.currentObjectives.Clear();

            //For every pixel in the height
            for(int y=0; y<level.height; y++)
            {
                //For every pixel in the width
                for(int x=0; x<level.width; x++)
                {
                    //Create a prefab
                    GenerateTile(x, y);

                }
            }
    }

    //Generate a prefab based on the ColorToPrefab Class
    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if(pixelColor.a == 0)
        {
            return;
        }

        foreach(ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor)) 
            {
                Vector2 position = new Vector2(x, y);
                GameObject block = Instantiate(colorMapping.prefab,position + offset,Quaternion.identity,transform);

                //If we are spawning a coin
                if (pixelColor.Equals(Color.green)) {
                    //Add it to the current list of active objectives for the player
                    GameController.instance.currentObjectives.Add(block);
                }
            }
        }
    }
	
}
