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

    private void Awake()
    {
        textFinishImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        ssDirector = FindObjectOfType<StorySceneDirector>();
        SetText(0);
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
                    yield return ssDirector.SetAction(actionStr);
                    isPlayingScirpt = true;
                }

                if(splitScirpts[i][j] == '#')
                {
                    continue;
                }

                textMsg.text += splitScirpts[i][j];
                yield return new WaitForSeconds(isSkipScript ? 0.0f : 0.15f);
            }
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

        Debug.Log("All Text Is Finished");
        yield return null;
    }

    public void SetNameText(string name, float duration)
    {
        name = name.Trim('\"');
        textName.text = "";
        textName.DOText(name, duration);
    }
}