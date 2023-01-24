using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] Slider slider_BGMVolume;
    [SerializeField] Slider slider_SFXVolume;

    [SerializeField] Toggle toggleAlram;
    [SerializeField] Toggle toggleAutoPlay;

    public void Awake()
    {
        slider_BGMVolume.onValueChanged.AddListener(value => SaveManager.MusicVolume = value);
        slider_SFXVolume.onValueChanged.AddListener(value => SaveManager.SoundEffectVolume = value);

        toggleAlram.onValueChanged.AddListener(value => SaveManager.IsPushAlarmOn = value);
        toggleAutoPlay.onValueChanged.AddListener(value => SaveManager.IsStoryAutoPlayOnUnlocked = value);
    }

    private void OnEnable()
    {
        slider_BGMVolume.SetValueWithoutNotify(SaveManager.MusicVolume);
        slider_SFXVolume.SetValueWithoutNotify(SaveManager.SoundEffectVolume);

        toggleAlram.SetIsOnWithoutNotify(SaveManager.IsPushAlarmOn);
        toggleAutoPlay.SetIsOnWithoutNotify(SaveManager.IsStoryAutoPlayOnUnlocked);
    }
}
