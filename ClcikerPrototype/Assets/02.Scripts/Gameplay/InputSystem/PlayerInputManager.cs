using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] TouchEffectManager _touchEffectManager = null;
    [SerializeField] TouchCost _touchCost = null;
    [SerializeField] ValuePrinter _valuePrinter = null;

    private void Awake()
    {
        SaveManager.LoadData();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CustomSceneManager.StorySceneChange(0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _touchEffectManager.GetEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            EventSystem touchEvent = EventSystem.current;
            if (touchEvent.IsPointerOverGameObject() && touchEvent.currentSelectedGameObject)
                return;

            _touchCost.OnTouch();
        }
#elif PLATFORM_ANDROID
        for (int i = 0; i < Input.touches.Length; i++)
        {
            Touch curTouch = Input.touches[i];

            EventSystem touchEvent = EventSystem.current;
            if (touchEvent.IsPointerOverGameObject() && touchEvent.currentSelectedGameObject)
                return;

            if (curTouch.phase == TouchPhase.Began)
            {
                _touchEffectManager.GetEffect(Camera.main.ScreenToWorldPoint(curTouch.position));
                _touchCost.OnTouch();
            }
        }
#endif

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<GameExitUI>()?.SetNoticePanel(() => Application.Quit(), () => gameObject.SetActive(true), "확인", "취소", "게임을 종료하시겠습니까?");
            gameObject.SetActive(false);
        }
    }
}
