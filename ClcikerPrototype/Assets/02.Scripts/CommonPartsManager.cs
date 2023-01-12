using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPartsManager : MonoBehaviour
{
    public static CommonPartsManager Instance = null;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
