using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSelector : MonoBehaviour
{
    [SerializeField] AudioClip[] bgmClips = null;
    [SerializeField] AudioSource bgmSource = null;

    float maxVolume = 0f;

    private void Start()
    {
        bgmSource.volume = SaveManager.MusicVolume;
        maxVolume = SaveManager.MusicVolume;
        SaveManager.OnChangeMusicVolume += (value) => bgmSource.volume = value;
        SaveManager.OnChangeMusicVolume += (value) => maxVolume = value;
    }

    public float GetBGMPercent()
    {
        return bgmSource.time / bgmSource.clip.length;
    }

    public void SetBGM(AudioClip newClip)
    {
        SetActiveBGM(false, 0.5f, () =>
        {
            bgmSource.clip = newClip;
            SetActiveBGM(true, 0.5f);
            bgmSource.Play();
        });
    }

    public void SetBGM(int idx)
    {
        SetActiveBGM(false, 0.5f, () =>
        {
            bgmSource.clip = bgmClips[idx];
            SetActiveBGM(true, 0.5f);
            bgmSource.Play();
        });
    }

    public void SetMainBGM()
    {
        SetBGM(SaveManager.IdxCurMusic);
    }

    public void SetActiveBGM(bool isActive, float duration, System.Action onComplete = null)
    {
        bgmSource.DOFade(isActive ? maxVolume : 0, duration).OnComplete(() => onComplete?.Invoke());
    }
}
