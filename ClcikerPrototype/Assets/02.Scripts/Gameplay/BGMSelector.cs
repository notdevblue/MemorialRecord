using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSelector : MonoBehaviour
{
    [SerializeField] AudioSource bgmSource = null;

    public void SetBGM(AudioClip newClip)
    {
        bgmSource.DOPitch(0, 0.2f);
        bgmSource.clip = newClip;
        bgmSource.DOPitch(1, 0.2f);
    }
}
