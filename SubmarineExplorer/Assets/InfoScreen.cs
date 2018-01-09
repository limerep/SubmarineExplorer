using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScreen : MonoBehaviour {

    [SerializeField]
        Button button;

    Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
        button.onClick.AddListener(ShrinkAndDestroy);  
	}

    void ShrinkAndDestroy()
    {
        anim.SetTrigger("ScaleDown");
        StartCoroutine(WaitForSomeTime());
    }

    IEnumerator WaitForSomeTime()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
