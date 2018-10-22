using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingSceneTransition : MonoBehaviour {

    private IEnumerator coroutine;
    public float duration;
    public int index;

    void Start()
    {
        coroutine = SceneTransition(duration, index);
        StartCoroutine(coroutine);
    }

    public void LoadScene(float waitTime, int sceneIndex)
    {
        coroutine = SceneTransition(waitTime, sceneIndex);
        StartCoroutine(coroutine);
    }

    IEnumerator SceneTransition(float waitTime, int sceneIndex)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
