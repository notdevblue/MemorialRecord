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
            if (curTouch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(curTouch.fingerId))
            {
                _touchEffectManager.GetEffect(Camera.main.ScreenToWorldPoint(curTouch.position));
                _touchCost.OnTouch();
            }
        }
#endif
    }
}
