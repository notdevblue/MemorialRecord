using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CustomSceneManager
{
    static int curChapter = 0;

    static bool isLoading = false;

    public static void MainSceneChangeFromStory()
    {
        if (isLoading)
            return;

        isLoading = true;

        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);

        SceneManager.sceneLoaded += OnSceneChangedFromStory;
    }

    public static void StorySceneChangeFromMain(int chapter)
    {
        if (isLoading)
            return;

        isLoading = true;

        curChapter = chapter;
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
                isLoading = false;
            };

            SceneManager.sceneLoaded -= OnSceneChangedFromMain;
        }
    }

    private static void OnSceneChangedFromStory(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoadingScene")
        {
            SaveManager.WatchedStories[curChapter] = true;
            GameObject.FindObjectOfType<LoadingSceneFade>().onFadeOutComplete += () =>
            {
                SceneManager.UnloadSceneAsync("StoryScene");
                SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
                UnityEngine.GameObject.FindObjectOfType<BGMSelector>()?.SetMainBGM();
            };

            GameObject.FindObjectOfType<LoadingSceneFade>().onFadeInComplete += () =>
            {
                SceneManager.UnloadSceneAsync("LoadingScene");
                isLoading = false;
            };

            SceneManager.sceneLoaded -= OnSceneChangedFromStory;
        }
    }
}
