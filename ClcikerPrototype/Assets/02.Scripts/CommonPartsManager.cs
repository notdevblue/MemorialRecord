using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPartsManager : MonoBehaviour
{
    [Header("StartSetting")]
    AudioClip originBGM;

    private void Awake()
    {
        if(FindObjectOfType<CommonPartsManager>())
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        FindObjectOfType<BGMSelector>().SetBGM(originBGM);
    }
}
