using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateLoadProgress : MonoBehaviour
{
    //Two references to the same thing because Unity
    public Slider progressBar;
    public GameObject progressBar2;

    private AsyncOperation loadingOperation;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.value = 0.0f;
    }

    // FixedUpdate is called every 0.02 seconds regardless of framerate
    void FixedUpdate()
    {
        //print(LoadAsyncProgress());
        progressBar.value = LoadAsyncProgress();
    }

    public void LoadIglooSceneAsync()
    {
        progressBar.enabled = true;
        //Unity can't SetActive on a Slider? Why?
        progressBar2.SetActive(true);
        loadingOperation = SceneManager.LoadSceneAsync("igloo_interior");
    }

    public float LoadAsyncProgress()
    {
        if (loadingOperation != null)
        {
            return loadingOperation.progress;
        }
        else
        {
            return 0.0f;
        }
    }

    public bool IsLoadFinished()
    {
        return loadingOperation.isDone;
    }
}
