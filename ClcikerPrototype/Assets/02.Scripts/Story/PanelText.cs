using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelText : MonoBehaviour
{
    bool isPlayingScirpt = false;
    bool isSkipScript = false;
    bool isEndScirpt = false;

    StorySceneDirector ssDirector;
    [SerializeField] Text textMsg;
    [SerializeField] Text textName; 
    [SerializeField] TextAsset[] scenarios;
    [SerializeField] Image textFinishImage;
    [SerializeField] Button btnSkip;

    private void Awake()
    {
        textFinishImage.gameObject.SetActive(false);
        btnSkip.onClick.AddListener(() => CustomSceneManager.MainSceneChangeFromStory());
    }

    private void Start()
    {
        ssDirector = FindObjectOfType<StorySceneDirector>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isPlayingScirpt)
            {
                isSkipScript = true;
            }
        }
    }

    public void SetText(int idx)
    {
        if(scenarios.Length <= idx)
        {
            CustomSceneManager.MainSceneChangeFromStory();
            return;
        }

        StartCoroutine(PlayScenarios(scenarios[idx]));
    }

    private IEnumerator PlayScenarios(TextAsset script)
    {
        List<string> splitScirpts = script.text.Split('\n').ToList();

        isPlayingScirpt = true;
        for (int i = 0; i < splitScirpts.Count; i++)
        {
            for (int j = 0; j < splitScirpts[i].Length; j++)
            {
                if (splitScirpts[i].Contains("PlayerName") && SaveManager.CharName != "$InitalizedPlayerName$")
                {
                    splitScirpts[i] = splitScirpts[i].Replace("PlayerName", SaveManager.CharName);
                }

                if (splitScirpts[i][j] == '#')
                {
                    j++;
                    string actionStr = "";
                    while (splitScirpts[i][j] != '#')
                    {
                        actionStr += splitScirpts[i][j];
                        j++;
                    }
                    isPlayingScirpt = false;
                    object obj = ssDirector.SetAction(actionStr);
                    yield return obj;
                    isPlayingScirpt = true;
                }

                if(splitScirpts[i][j] == '#')
                {
                    continue;
                }

                textMsg.text += splitScirpts[i][j];
                yield return new WaitForSeconds(isSkipScript ? 0.0f : 0.2f);
            }
            yield return new WaitForSeconds(isSkipScript ? 0.4f : 0.0f); // ½ºÅµ µô·¹ÀÌ

            isEndScirpt = true;
            isSkipScript = false;

            textFinishImage.gameObject.SetActive(true);

            yield return new WaitUntil(() => isSkipScript && isEndScirpt);

            isSkipScript = false;
            isEndScirpt = false;

            textFinishImage.gameObject.SetActive(false);

            textMsg.text = "";
        }
        isPlayingScirpt = false;

        CustomSceneManager.MainSceneChangeFromStory();
        yield return null;
    }

    public void SetNameText(string name, float duration)
    {
        name = name.Trim('\"');

        if (name == "PlayerName")
        {
            name = SaveManager.CharName;
        }

        textName.text = "";
        textName.DOText(name, duration);
    }
}
