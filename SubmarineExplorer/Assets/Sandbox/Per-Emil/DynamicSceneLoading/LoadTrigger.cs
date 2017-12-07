using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour
{

    public string loadName;
    public string unloadName;

    private void OnTriggerEnter(Collider col)
    {
        if (loadName != "")
            SceneManagement.Instance.Load(loadName);

        if (unloadName != "")
            StartCoroutine("UnloadScene");
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(.10f);
        SceneManagement.Instance.Unload(unloadName);
    }
}
