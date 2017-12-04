using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scene3 : MonoBehaviour {


	public GUISkin skin;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{

		GUI.skin = skin;
		GUI.skin.button.fontSize = (int)(0.05f * Screen.height);
		GUI.skin.label.fontSize = (int)(0.045f * Screen.height);


		Rect button_rect = new Rect(0.31f * Screen.width, 0.60f * Screen.height,
			0.40f * Screen.width, 0.10f * Screen.height);


		if (GUI.Button (button_rect, "Load 1st level")) {

			PlayerPrefs.SetString ("LevelName","LoadingBars/scene1");
			SceneManager.LoadSceneAsync("LoadingBars/scene2");


		}



	}

}
