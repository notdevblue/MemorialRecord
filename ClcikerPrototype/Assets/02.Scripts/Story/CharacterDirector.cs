using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDirector : MonoBehaviour
{
    [SerializeField] Animator[] charAnims;
    [SerializeField] SpriteRenderer[] shadowSR;

    Character curChar;
    Vector3 originPos = Vector3.zero;

    Action<Vector2> onActOriginPos = (value) => { };

    private void Awake()
    {
        originPos = transform.position;
    }

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
        charAnims[(int)curChar].GetComponent<SpriteRenderer>().DOColor(new Color(0.75f, 0.75f, 0.75f), duration);
    }

    public void RemoveCharacterShadow(float duration)
    {
        charAnims[(int)curChar].GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f), duration);
    }

    public void SetCharacterShake(Axis axis, float duration)
    {
        StartCoroutine(Shake(axis, duration));
    }

    private IEnumerator Shake(Axis axis, float duration)
    {
        float timer = 0f;
        Vector2 originPos = charAnims[(int)curChar].transform.position;
        while (timer <= duration)
        {
            Debug.Log(Mathf.Cos(timer));
            Vector2 plusPos = (axis == Axis.Horizontal ? Vector2.right : Vector2.up) * Mathf.Cos(timer * 100) / 100;
            charAnims[(int)curChar].transform.position += (Vector3)plusPos;

            timer += Time.deltaTime / duration;
            yield return null;
        }
        charAnims[(int)curChar].transform.DOMove(originPos, 0.1f);
    }

    public void CharacterMove(DirectionOne directionOne, float duration)
    {
        originPos = charAnims[(int)curChar].transform.position;
        switch (directionOne)
        {
            case DirectionOne.Left:
                charAnims[(int)curChar].transform.DOMove(charAnims[(int)curChar].transform.position + Vector3.left, duration);
                break;
            case DirectionOne.Right:
                charAnims[(int)curChar].transform.DOMove(charAnims[(int)curChar].transform.position + Vector3.right, duration);
                break;
            default:
                break;
        }
    }

    public void SetCharacterStandUp(float duration)
    {
        charAnims[(int)curChar].transform.DOMoveY(charAnims[(int)curChar].transform.position.y - 0.5f, duration / 2).OnComplete(() =>
        {
            charAnims[(int)curChar].transform.DOMoveY(charAnims[(int)curChar].transform.position.y + 0.5f, duration / 2);
        });
    }

    public void SetCharacterJump(float duration)
    {
        charAnims[(int)curChar].transform.DOMoveY(charAnims[(int)curChar].transform.position.y + 0.5f, duration / 2).OnComplete(() =>
        {
            charAnims[(int)curChar].transform.DOMoveY(charAnims[(int)curChar].transform.position.y - 0.5f, duration / 2);
        });
    }

    public void MoveCharacterReturn(float duration)
    {
        charAnims[(int)curChar].transform.DOMove(originPos, duration);
    }
}
