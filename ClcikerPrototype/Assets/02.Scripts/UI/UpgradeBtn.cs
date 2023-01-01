using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UpgradeBtn : Button, IPointerDownHandler, IPointerUpHandler
{
    public Action onButtonDown;
    public Action onButtonUp;

    override public void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        onButtonDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        onButtonUp?.Invoke();
    }
}
