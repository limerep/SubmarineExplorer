using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour {

    public static SceneManagement Instance { set; get; }

    private void Awake()
    {
        Instance = this;
        Load("Player");
        Load("Scene01");
        Load("Scene02");
    }

    public void Load(string sceneName)
    {
        
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
            
    }

    public void Unload(string sceneName)
    {
        
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadScene(sceneName);
        }
            
    }
}



