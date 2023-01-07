using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    Horizontal,
    Vertical
}

public enum Direction
{
    Left,
    Right,
    Top,
    Bottom
}

public enum Character
{
    Boy,
    Girl
}
 
public class StorySceneDirector : MonoBehaviour
{
    [Header("About Sound")]
    [SerializeField] AudioClip[] bgmSources;

    [Header("Others")]
    [SerializeField] PanelText panelText;
    [SerializeField] PanelName panelName;
    [SerializeField] PanelSubObj panelSubObj;
    [SerializeField] IllustrationDirector illustDirector;
    [SerializeField] SceneEffectDirector seDirector;
    [SerializeField] CharacterDirector chDirector;

    public void SetChapter(int chapter)
    {
        panelText.SetText(chapter);
    }

    public object SetAction(string text = "")
    {
        List<object> objects = new List<object>();

        char[] trimChar = { ' ', '#' };

        string funcName = "";

        string[] strs = text.Split(':');

        funcName = strs[0].Trim(trimChar);

        string[] strs1;
        if (strs.Length < 2)
        {
            strs1 = null;
        }
        else
        {
            strs1 = strs[1].Split(',');
        }

        if(strs1 != null)
        {
            for (int i = 0; i < strs1.Length; i++)
            {
                objects.Add(strs1[i].Trim(trimChar));
            }
        }

        return PlaySceneAction(funcName, objects.ToArray());
    }

    private object PlaySceneAction(string funcName, params object[] args)
    {
        float duration;
        object result = null;

        switch (funcName)
        {
            case "WaitForSeconds":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);
                break;
            case "DisappearAll":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                DisappearAll(duration);
                break;
            case "FadeInBackground":
                duration = float.Parse(args[1].ToString());
                result = new WaitForSeconds(duration);

                seDirector.FadeInBackground(int.Parse(args[0].ToString()), duration);
                break;
            case "FadeOutBackgorund":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                seDirector.FadeOutBackground(duration);
                break;
            case "ShakeBackground":
                duration = seDirector.ShakeBackground((Axis)Enum.Parse(typeof(Axis), args[0].ToString()));
                result = new WaitForSeconds(duration);
                break;
            case "FadeInWhiteBlank":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                seDirector.FadeInWhiteBlank(duration);
                break;
            case "FadeInBlackBlank":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                seDirector.FadeInBlackBlank(duration);
                break;
            case "FadeOutWhiteBlank":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                seDirector.FadeOutWhiteBlank(duration);
                break;
            case "FadeOutBlackBlank":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                seDirector.FadeOutWhiteBlank(duration);
                break;
            case "SlideInFrom":
                duration = float.Parse(args[1].ToString());
                result = new WaitForSeconds(duration);

                seDirector.SlideInFrom((Direction)Enum.Parse(typeof(Direction), args[0].ToString()), duration);
                break;
            case "SlideOutFrom":
                duration = float.Parse(args[1].ToString());
                result = new WaitForSeconds(duration);

                seDirector.SlideOutTo((Direction)Enum.Parse(typeof(Direction), args[0].ToString()), duration);
                break;
            case "FadeInNameBox":
                duration = float.Parse(args[0].ToString());
                result = new WaitUntil(panelName.FadeInNameBox(duration));
                break;
            case "FadeOutNameBox":
                duration = float.Parse(args[0].ToString());
                result = new WaitUntil(panelName.FadeOutNameBox(duration));
                break;
            case "FadeInSubObject":
                duration = float.Parse(args[1].ToString());
                result = new WaitForSeconds(duration);

                panelSubObj.FadeInSubObject(int.Parse(args[0].ToString()), duration);
                break;
            case "FadeOutSubObject":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                panelSubObj.FadeOutSubObject(duration);
                break;
            case "TypeInNameText":
                duration = float.Parse(args[1].ToString());

                panelText.SetNameText(args[0].ToString(), duration);
                break;
            case "RemoveNameText":
                panelText.SetNameText(args[0].ToString(), 0.0f);
                break;
            case "FadeInIllustration":
                duration = float.Parse(args[1].ToString());
                result = new WaitForSeconds(duration);

                illustDirector.FadeInIllurst(int.Parse(args[0].ToString()), duration);
                break;
            case "FadeOutIllustration":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                illustDirector.FadeOutIllurst(duration);
                break;
            case "SetCharEmotion":
                chDirector.SetCharacterEmotionAction(args[0].ToString(), (Character)Enum.Parse(typeof(Direction), args[1].ToString()));
                break;
            default:
                break;
        }
        return result;
    }

    public void DisappearAll(float duration)
    {

    }

    public void Skip()
    {

    }
}
