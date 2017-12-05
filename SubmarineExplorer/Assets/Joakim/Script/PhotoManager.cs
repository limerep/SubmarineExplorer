using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour {


    List<Photo> photoList;
    public int photos; 

    public class Photo 
    {
        
        public Photo(Texture2D file, GameObject creature)
        {
           picture = file;
           fish = creature; 
        }

        private Texture2D picture;
        private GameObject fish; 
    };

	// Use this for initialization
	void Start () {
        photos = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreatePhoto( Texture2D tex, GameObject creature)
    { 
        photoList.Add(new Photo(tex, creature));
    }


};
