using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SubExplorer {


public class Photo {

    private string name;
    private Texture2D picture;
    private List<string > creatures; 

    public Photo(string fishName, Texture2D file, List<string> otherFishes)
        {

            name = fishName;
            picture = file;
            creatures = otherFishes; 
        }

    public string getName()
    {
        return name; 
    }
    public Texture2D getTexture()
    {
         return picture; 
    }
        public List<string> getCreatures()
        {
            return creatures; 
        }
}
}
