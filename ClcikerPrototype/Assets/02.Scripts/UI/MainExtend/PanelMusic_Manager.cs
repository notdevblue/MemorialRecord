using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMusic_Manager : MonoBehaviour
{
    [SerializeField] string[] names;
    [SerializeField] string[] infos;

    [SerializeField] Sprite _spritePlay;
    [SerializeField] Sprite _spritePause;

    [SerializeField] ToggleGroup toggleGroup = null;
    [SerializeField] Transform parentTransform = null;
    [SerializeField] PanelMusic_Content contentPrefab = null;

    Custom_Slider curSlider = null;
    BGMSelector bgmSelector = null;

    private void Awake()
    {
        for (int i = 0; i < names.Length; i++)
        {
            int y = i;
            PanelMusic_Content content = Instantiate<PanelMusic_Content>(contentPrefab, parentTransform);
            
            content.InitContent(names[i], infos[i], SaveManager.MusicBoughtArr[i], _spritePlay, _spritePause);

            content._toggle.onValueChanged.AddListener((value) => OnChangedToggle(value, y, content));
            content._toggle.group = toggleGroup;

            content._btnBuy.onClick.AddListener(() => OnClickBtnBuy(y));
                
            if (i == 0)
            {
                content._toggle.isOn = true;
            }
        }
    }
    private void Start()
    {
        bgmSelector = FindObjectOfType<BGMSelector>();    
    }

    private void Update()
    {
        curSlider?.SetAmount(bgmSelector.GetBGMPercent());
    }

    private void OnClickBtnBuy(int idx)
    {
        if(SaveManager.MusicBoughtArr[idx] && SaveManager.CurQuillPen < 1000)
        {
            return;
        }

        SaveManager.CurQuillPen -= 1000;
        SaveManager.MusicBoughtArr[idx] = true;
    }

    private void OnChangedToggle(bool value, int idx, PanelMusic_Content panel)
    {
        if(value)
        {
            SaveManager.IdxCurMusic = idx;
            curSlider = panel._slider;
        }
    }


}
