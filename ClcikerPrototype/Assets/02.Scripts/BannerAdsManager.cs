using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    string BANNER_PLACEMENT = "banner";

    public void OnInitializationComplete()
    {

        Advertisement.Banner.SetPosition(_bannerPosition);
        Advertisement.Banner.Load(BANNER_PLACEMENT);
        Advertisement.Banner.Show(BANNER_PLACEMENT);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }

    public void OnUnityAdsAdLoaded(string placementId)
    {

    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    private void Start()
    {
        if (!SaveManager.IsRemoveAds)
        {
            if (Advertisement.isInitialized == false)
            {
                Advertisement.Initialize("5131655", true, this);
            }
            else
            {
                Advertisement.Banner.Show(BANNER_PLACEMENT);
            }
        }
    }

    private void OnDestroy()
    {
        if(Advertisement.isInitialized)
        {
            Advertisement.Banner.Hide(false);
        }
    }
}
