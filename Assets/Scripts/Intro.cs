using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.canvasRenderer.SetAlpha(0.0f);
        StartCoroutine(fadeText());
    }

    private IEnumerator fadeText() {
        yield return new WaitForSeconds(1.5f);
        text.CrossFadeAlpha(1.0f, 1.5f, true);
        yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(1.5f);
        text.CrossFadeAlpha(0.0f, 1.5f, true);
        yield return new WaitForSeconds(2.5f);
        //SceneManager.LoadScene()

    }
}
