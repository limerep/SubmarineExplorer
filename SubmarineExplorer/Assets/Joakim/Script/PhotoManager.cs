using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using SubExplorer;
using System.IO;

public class PhotoManager : MonoBehaviour {

    
    public List<Photo> photoList;
    public int photos;
    
    public RawImage image1;
    public RawImage image2;
    public RawImage image3;
    public RawImage image4;
    public RawImage image5;
    public RawImage image6;
    public RawImage image7;
    public RawImage image8;
 
    // Use this for initialization
    void Start ()
    {
        photos = 0;
	}
	

	

   public void CreatePhoto(string name, Texture2D tex, List<GameObject> creatures)
    { 
      if (photoList == null)
      {
         photoList = new List<Photo>();
      }

        if (photos > 8)
        {
            photos = 0;
        }
        
        photoList.Insert(photos,new Photo(name, tex, creatures));
        SetPhotoInCanvas();
        photos++;
        

      byte[] bytes = tex.EncodeToPNG();

      //Object.Destroy(tex);
      File.WriteAllBytes(Application.dataPath + "/../" + photos + ".png", bytes);

    }


    public void RemovePhoto(int photo)
    {
        photoList.RemoveAt(photo);
        photos = 8; 
    }
    public void SetPhotoInCanvas()
    {
        switch (photos)
        {
            case 0: image1.texture = photoList[photos].getTexture();
                    break; 
            case 1: image2.texture = photoList[photos].getTexture();
                    break;
            case 2: image3.texture = photoList[photos].getTexture();
                    break;
            case 3:
                    image4.texture = photoList[photos].getTexture();
                    break;
            case 4:
                    image5.texture = photoList[photos].getTexture();
                    break;
            case 5:
                    image6.texture = photoList[photos].getTexture();
                    break;
            case 6:
                    image7.texture = photoList[photos].getTexture();
                    break;
            case 7:
                    image8.texture = photoList[photos].getTexture();
                    break;

            default: break; 
        }
    }
};
