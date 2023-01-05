using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CustomSceneManager
{
    public static void SceneChange(string targetScene, Action onCompleteChange = null)
    {
        var sceneLoading = SceneManager.LoadSceneAsync(targetScene);

        sceneLoading.completed += (x) => onCompleteChange?.Invoke();
    }

    public static void SceneChange(string targetScene, bool isChangeBGM = false, AudioClip clip = null, Action onCompleteChange = null)
    {
        var sceneLoading = SceneManager.LoadSceneAsync(targetScene);

        sceneLoading.completed += (x) => onCompleteChange?.Invoke();

        UnityEngine.GameObject.FindObjectOfType<BGMSelector>().SetBGM(clip);
    }

    public static void StorySceneChange(string targetScene, bool isChangeBGM, AudioClip clip, Action onCompleteChange = null)
    {
        var sceneLoading = SceneManager.LoadSceneAsync(targetScene);
    }
}
