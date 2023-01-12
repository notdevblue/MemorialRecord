using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSelector : MonoBehaviour
{
    [SerializeField] AudioClip[] bgmClips = null;
    [SerializeField] AudioSource bgmSource = null;

    private void Start()
    {
        bgmSource.volume = SaveManager.MusicVolume;
        SaveManager.OnChangeMusicVolume += (value) => bgmSource.volume = value;
    }

    public float GetBGMPercent()
    {
        return bgmSource.time / bgmSource.clip.length;
    }

    public void SetBGM(AudioClip newClip)
    {
        bgmSource.DOPitch(0, 0.5f).OnComplete(() =>
        {
            bgmSource.clip = newClip;
            SetActiveBGM(true);
            bgmSource.Play();
        });
    }

    public void SetBGM(int idx)
    {
        bgmSource.DOPitch(0, 0.5f).OnComplete(() =>
        {
            bgmSource.clip = bgmClips[idx];
            SetActiveBGM(true);
            bgmSource.Play();
        });
    }

    public void SetMainBGM()
    {
        SetBGM(SaveManager.IdxCurMusic);
    }

    public void SetActiveBGM(bool isActive)
    {
        if(isActive)
        {
            bgmSource.DOPitch(1, 0.5f);
        }
        else
        {
            bgmSource.DOPitch(0, 0.5f);
        }
    }
}
