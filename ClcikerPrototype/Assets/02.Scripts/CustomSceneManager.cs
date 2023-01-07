using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CustomSceneManager
{
    public static void SceneChange(string targetScene, Action onCompleteChange = null)
    {
        UnityEngine.GameObject.FindObjectOfType<BGMSelector>().SetActiveBGM(false);

        var sceneLoading = SceneManager.LoadSceneAsync(targetScene);
        onCompleteChange += () => UnityEngine.GameObject.FindObjectOfType<BGMSelector>().SetActiveBGM(true);

        sceneLoading.completed += (x) => onCompleteChange?.Invoke();
    }

    public static void SceneChange(string targetScene, bool isChangeBGM = false, AudioClip clip = null, Action onCompleteChange = null)
    {
        var sceneLoading = SceneManager.LoadSceneAsync(targetScene);

        sceneLoading.completed += (x) => onCompleteChange?.Invoke();

        UnityEngine.GameObject.FindObjectOfType<BGMSelector>().SetBGM(clip);
    }

    public static void StorySceneChange(int chapter)
    {
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);

        //var loading = SceneManager.LoadSceneAsync("StoryScene");
        //loading.allowSceneActivation = false;
        //Debug.Log(loading);

        SceneManager.sceneLoaded += (x, y) =>
        {
            if(x.name == "LoadingScene")
            {
                GameObject.FindObjectOfType<LoadingSceneFade>().onFadeOutComplete += () =>
                {
                    SceneManager.UnloadSceneAsync("MainScene");
                    GameObject.FindObjectOfType<StorySceneDirector>().SetChapter(chapter);
                };
            }
        };

    }
}
