using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;

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

        PlayGamesPlatform.Activate();
    }

    private void Start()
    {
        if(PlayGamesPlatform.Instance.IsAuthenticated() == false)
        {
            Social.localUser.Authenticate(success => { });
        }
    }
}
