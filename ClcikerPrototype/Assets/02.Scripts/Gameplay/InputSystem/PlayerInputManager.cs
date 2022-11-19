using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] TouchEffectManager _touchEffectManager = null;
    [SerializeField] TouchCost _touchCost = null;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _touchEffectManager.GetEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
#endif
        for (int i = 0; i < Input.touches.Length; i++)
        {
            Touch curTouch = Input.touches[i];
            if (curTouch.phase == TouchPhase.Began)
            {
                _touchEffectManager.GetEffect(Camera.main.ScreenToWorldPoint(curTouch.position));
            }
        }
    }
}
