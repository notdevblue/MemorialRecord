using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CustomSceneManager
{
    static int curChapter = 0;

    public static void MainSceneChangeFromStory()
    {
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);

        SceneManager.sceneLoaded += OnSceneChangedFromStory;
    }

    public static void StorySceneChangeFromMain(int chapter)
    {
        curChapter = chapter;
        SaveManager.SetStoryDict(chapter, true);
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);

        SceneManager.sceneLoaded += OnSceneChangedFromMain;

        SceneManager.LoadScene("StoryScene", LoadSceneMode.Additive);
    }

    private static void OnSceneChangedFromMain(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoadingScene")
        {
            GameObject.FindObjectOfType<LoadingSceneFade>().onFadeOutComplete += () =>
            {
                SceneManager.UnloadSceneAsync("MainScene");
                GameObject.FindObjectOfType<StorySceneDirector>(true).SetChapter(curChapter);
            };

            GameObject.FindObjectOfType<LoadingSceneFade>().onFadeInComplete += () =>
            {
                SceneManager.UnloadSceneAsync("LoadingScene");
            };

            SceneManager.sceneLoaded -= OnSceneChangedFromMain;
        }
    }

    private static void OnSceneChangedFromStory(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoadingScene")
        {
            GameObject.FindObjectOfType<LoadingSceneFade>().onFadeOutComplete += () =>
            {
                SceneManager.UnloadSceneAsync("StoryScene");
                SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
                UnityEngine.GameObject.FindObjectOfType<BGMSelector>()?.SetMainBGM();
            };

            GameObject.FindObjectOfType<LoadingSceneFade>().onFadeInComplete += () =>
            {
                SceneManager.UnloadSceneAsync("LoadingScene");
            };

            SceneManager.sceneLoaded -= OnSceneChangedFromStory;
        }
    }
}
