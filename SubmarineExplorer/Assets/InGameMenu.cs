using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    [SerializeField]
        GameObject menuScreen;

    [SerializeField]
        Button resumeBtn, optionsBtn, exitBtn;

    Animator anim;
    bool isActive;

    private void Awake()
    {
        if (menuScreen)
        {
            menuScreen.SetActive(false);
            isActive = false;
        }

        else
            Debug.Log("Populate menu canvas");
    }

    private void Start ()
    {
        anim = GetComponentInChildren<Animator>();

        resumeBtn.onClick.AddListener(ResumeGame);
        optionsBtn.onClick.AddListener(Options);
        exitBtn.onClick.AddListener(ExitToMenu);
    }
	
	private void Update ()
    {
		
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isActive)
            {
                menuScreen.SetActive(true);
                isActive = true;
            }
            else
            {
                ResumeGame();
            }
        }
	}

    void ResumeGame()
    {
        menuScreen.SetActive(false);
        isActive = false;
    }

    void Options()
    {
        Debug.Log("Put options to change controller setup and other useful stuffz here!"); 
    }

    void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
