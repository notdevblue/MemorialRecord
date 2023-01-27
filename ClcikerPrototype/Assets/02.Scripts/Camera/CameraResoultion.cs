using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResoultion : MonoBehaviour
{
    void Awake()
    {
        SetGameResolution();
    }

    void SetGameResolution()
    {
        Camera.main.orthographicSize *= ((float)Screen.height / Screen.width) / (1920f / 1080);
        Camera.main.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 1920f / 1080 * Screen.width / 2, 0));
    }
}
