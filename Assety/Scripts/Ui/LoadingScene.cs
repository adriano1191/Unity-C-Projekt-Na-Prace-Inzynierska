using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoadingScene : MonoBehaviour {

    public Texture2D ProgressBar;
    AsyncOperation ao;
    float progress;
    float timer;

    private AsyncOperation async = null;
    //Always start this coroutine in the start function
    private void Start()
    {
        StartCoroutine(LoadLevel("Test"));
    }
    //CoRoutine to return async progress, and trigger level load.
    private IEnumerator LoadLevel(string Level)
    {
        yield return null;

         ao = SceneManager.LoadSceneAsync(Level);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            Debug.Log(ao.progress);
            // [0, 0.9] > [0, 1]
            progress = Mathf.Clamp01(ao.progress / 0.9f);
            //Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                Debug.Log("Press a key to start");
                if (Input.anyKey)
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    private void OnGUI()
    {
        timer += Time.deltaTime;
        GUI.DrawTexture(new Rect(0, 0, 100 * ao.progress, 50), ProgressBar);
        //Debug.Log("Działa " + async.progress);
        // Check if it's loading;
        if (async != null)
        {

           // GUI.DrawTexture(new Rect(0, 0, 100 * async.progress, 50), ProgressBar);
            
        }
    }

    void Update()
    {
         
    }
}
