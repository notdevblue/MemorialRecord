using DG.Tweening;
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

    [SerializeField] Button btnClose = null;
    [SerializeField] Image imageFade;

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

        btnClose.onClick.AddListener(() =>
        {
            imageFade.gameObject.SetActive(true);
            imageFade.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                transform.parent.gameObject.SetActive(false);

                imageFade.DOFade(0.0f, 0.0f).OnComplete(() => imageFade.gameObject.SetActive(false));
            });
        });
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
        if(SaveManager.MusicBoughtArr[idx] && SaveManager.CurInk < 1000)
        {
            return;
        }

        FindObjectOfType<PanelGameNotice>().SetShopNoticePanel(
        () =>
        {
            SaveManager.MusicBoughtArr[idx] = true;
            RefreshAll();
        },
        () =>
        {

        },
        "구매", "취소", "구매", $"곡 '{names[idx]}'를 구매하시겠습니까?", 10
        );

    }

    private void RefreshAll()
    {

        PanelMusic_Content[] panels = GetComponentsInChildren<PanelMusic_Content>();
        for (int i = 0; i < panels.Length; i++)
        {
            int y = i;
            PanelMusic_Content content = panels[i];

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

    private void OnChangedToggle(bool value, int idx, PanelMusic_Content panel)
    {
        if(value)
        {
            if(SaveManager.IdxCurMusic != idx)
            {
                SaveManager.IdxCurMusic = idx;
            }
            curSlider = panel._slider;
        }
    }


}
