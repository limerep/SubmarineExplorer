using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour {


    List<Photo> photoList;
    public int photos; 

    public class Photo 
    {
        
        public Photo(string path, GameObject creature)
        {
            filePath = path;
            fish = creature; 
        }

        private string filePath;
        private GameObject fish; 
    };

	// Use this for initialization
	void Start () {
        photos = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreatePhoto( string path, GameObject creature)
    { 
        //photoList.Add(new Photo(path, creature));
    }
};
