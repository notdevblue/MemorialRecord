using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPartsManager : MonoBehaviour
{
    [Header("StartSetting")]
    [SerializeField] AudioClip originBGM;

    private void Awake()
    {
        if(FindObjectsOfType<CommonPartsManager>() != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GetComponentInChildren<BGMSelector>().SetBGM(originBGM);
    }
}
