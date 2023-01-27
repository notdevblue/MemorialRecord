using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSNS : MonoBehaviour
{
    public void OnClickTwitter()
    {
        Application.OpenURL("https://twitter.com/outer_games");
    }

    public void OnClickYoutube()
    {
        Application.OpenURL("https://youtube.com/channel/UCywFsjOXilW01ocUr438rcA");
    }

    public void OnClickInstagram()
    {
        Application.OpenURL("https://www.instagram.com/outer_games/");
    }
}
