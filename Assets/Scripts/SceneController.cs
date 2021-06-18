using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType { MainScene, VideoPlayerScene}
public class SceneController : Singleton<SceneController>
{
    [SerializeField] string mainSceneName, videoPlayerSceneName;

    public void LoadVideoScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.MainScene:
                StartCoroutine(LoadYourAsyncScene(mainSceneName));

                break;
            case SceneType.VideoPlayerScene:
                StartCoroutine(LoadYourAsyncScene(videoPlayerSceneName));
                break;
            default:
                break;
        }
    }
    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
