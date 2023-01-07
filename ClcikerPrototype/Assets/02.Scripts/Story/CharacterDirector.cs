using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDirector : MonoBehaviour
{
    [SerializeField] Animator[] charAnims;
    [SerializeField] SpriteRenderer[] shadowSR;

    Character curChar;

    public void SetCharacterEmotionAction(string key, Character character)
    {
        try
        {
            charAnims[(int)character].gameObject.SetActive(true);
            charAnims[(int)character].SetTrigger(key);
        }
        catch (System.Exception)
        {
            Debug.Log(character + "에게 해당하지 않는 표정이 할당되었습니다!");
            throw;
        }
    }

    public void FadeInCharacter(Character character, float duration)
    {
        charAnims[(int)curChar].GetComponent<SpriteRenderer>().DOFade(0.0f, 0.25f);

        curChar = character;

        charAnims[(int)curChar].gameObject.SetActive(true);
        charAnims[(int)curChar].GetComponent<SpriteRenderer>().DOFade(1.0f, duration);
    }

    public void FadeOutCharacter(float duration)
    {
        charAnims[(int)curChar].GetComponent<SpriteRenderer>().DOFade(0.0f, duration).OnComplete(() => charAnims[(int)curChar].gameObject.SetActive(false));
    }

    public void SetCharacterShadow(float duration)
    {
        shadowSR[(int)curChar].DOFade(0.5f, duration);
    }

    public void RemoveCharacterShadow(float duration)
    {
        shadowSR[(int)curChar].DOFade(1.0f, duration);
    }

    //public void SetCharacterShake(Axis axis, float duration)
    //{
    //    charSR[(int)curChar].transform.DOShakePosition()
    //}
}
