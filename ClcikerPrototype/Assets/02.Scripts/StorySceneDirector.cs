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

public enum DirectionOne
{
    Left,
    Right
}
 
public class StorySceneDirector : MonoBehaviour
{
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
                seDirector.ShakeBackground((Axis)Enum.Parse(typeof(Axis), args[0].ToString()));
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

                seDirector.FadeOutBlackBlank(duration);
                break;
            case "SlideInFrom":
                duration = float.Parse(args[1].ToString());
                result = new WaitForSeconds(duration);

                seDirector.SlideInFrom((Direction)Enum.Parse(typeof(Direction), args[0].ToString()), duration);
                break;
            case "SlideOutTo":
                duration = float.Parse(args[1].ToString());
                result = new WaitForSeconds(duration);

                seDirector.SlideOutTo((Direction)Enum.Parse(typeof(Direction), args[0].ToString()), duration);
                break;
            case "FadeInNameBox":
                duration = float.Parse(args[0].ToString());
                if (SaveManager.CharName != "$InitalizedPlayerName$")
                {
                    result = null;
                    break;
                }
                result = new WaitUntil(panelName.FadeInNameBox(duration));
                break;
            case "FadeOutNameBox":
                duration = float.Parse(args[0].ToString());
                if (SaveManager.CharName != "$InitalizedPlayerName$")
                {
                    result = null;
                    break;
                }
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
            case "FadeInCharacter":
                duration = float.Parse(args[1].ToString());
                result = new WaitForSeconds(duration);

                chDirector.FadeInCharacter((Character)Enum.Parse(typeof(Character), args[0].ToString()), duration);
                break;
            case "FadeOutCharacter":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                chDirector.FadeOutCharacter(duration);
                break;
            case "SetCharacterShake":
                duration = float.Parse(args[1].ToString());

                chDirector.SetCharacterShake((Axis)Enum.Parse(typeof(Axis), args[0].ToString()), duration);
                break;
            case "MoveCharacter":
                duration = float.Parse(args[1].ToString());

                chDirector.CharacterMove((DirectionOne)Enum.Parse(typeof(DirectionOne), args[0].ToString()), duration);
                break;
            case "MoveCharacterReturn":
                duration = float.Parse(args[0].ToString());

                chDirector.MoveCharacterReturn(duration);
                break;
            case "SetCharacterStandUp":
                duration = float.Parse(args[0].ToString());

                chDirector.SetCharacterStandUp(duration);
                break;
            case "SetCharacterJump":
                duration = float.Parse(args[0].ToString());

                chDirector.SetCharacterJump(duration);
                break;
            case "SetCharacterShadow":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                chDirector.SetCharacterShadow(duration);
                break;
            case "RemoveCharacterShadow":
                duration = float.Parse(args[0].ToString());
                result = new WaitForSeconds(duration);

                chDirector.RemoveCharacterShadow(duration);
                break;
            case "SetCharEmotion":
                chDirector.SetCharacterEmotionAction(args[0].ToString(), (Character)Enum.Parse(typeof(Character), args[1].ToString()));
                break;
            case "PlaySoundEffect":
                FindObjectOfType<SoundEffector>()?.PlaySoundEffect(args[0].ToString());
                break;
            case "PauseSoundEffect":
                FindObjectOfType<SoundEffector>()?.StopSoundEffect();
                break;
            case "PlayBGM":
                FindObjectOfType<BGMSelector>()?.SetBGM(int.Parse(args[0].ToString()));
                break;
            case "StopBGM":
                FindObjectOfType<BGMSelector>()?.SetActiveBGM(false, 0.5f);
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
