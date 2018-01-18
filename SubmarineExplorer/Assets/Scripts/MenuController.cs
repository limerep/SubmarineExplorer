using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [SerializeField]
        GameObject mainCanvas, optionsCanvas;
    [SerializeField]
        Button startBtn, optionsBtn, backBtn, exitBtn;

        Animator mainAnim, optionsAnim;

    void Awake()
    {
        if (mainCanvas && optionsCanvas)
        {
            mainCanvas.SetActive(true);
            optionsCanvas.SetActive(true);
        }
        else
            Debug.Log("Populate the menu canvases plz");
    }

    void Start () {

        mainAnim = mainCanvas.GetComponent<Animator>();
        optionsAnim = optionsCanvas.GetComponent<Animator>();


        optionsBtn.onClick.AddListener(Options);
        backBtn.onClick.AddListener(Back);
        startBtn.onClick.AddListener(StartGame);
        exitBtn.onClick.AddListener(Exit);
	}

    void Options()
    {
        mainAnim.SetTrigger("ScaleDown");
        optionsAnim.SetTrigger("ScaleUp");
        
    }

    void Back()
    {
        mainAnim.SetTrigger("ScaleUp");
        optionsAnim.SetTrigger("ScaleDown");
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void Exit()
    {
        Application.Quit();
    }
}
