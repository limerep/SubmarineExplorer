using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scene2 : MonoBehaviour {

	// Use this for initialization
	void Start () {

		StartCoroutine (LoadLevel());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	IEnumerator LoadLevel() {
		yield return new WaitForSeconds(3);
		SceneManager.LoadSceneAsync(PlayerPrefs.GetString ("LevelName"));

	}



}
