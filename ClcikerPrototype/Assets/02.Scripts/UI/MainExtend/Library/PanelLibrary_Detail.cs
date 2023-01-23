using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLibrary_Detail : MonoBehaviour
{
    [SerializeField] Text _textTitle;
    [SerializeField] Text[] _textDetail;

    public void SetText(string title, string leftDetail, string rightDetail)
    {
        gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(true);
        _textTitle.text = title;
        _textDetail[0].text = leftDetail;
        _textDetail[1].text = rightDetail;
    }
}
