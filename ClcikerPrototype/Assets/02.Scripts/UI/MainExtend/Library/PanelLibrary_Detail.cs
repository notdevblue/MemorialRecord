using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLibrary_Detail : MonoBehaviour
{
    [SerializeField] Text _textTitle;
    [SerializeField] Text[] _textDetail;

    [SerializeField] Button _btnLeft;
    [SerializeField] Button _btnRight;

    [SerializeField] string[] _details = null;
    string _title = null;

    int _curidx = 0;

    private void Awake()
    {
        _btnLeft.onClick.AddListener(OnClickLeft);
        _btnRight.onClick.AddListener(OnClickRight);
    }

    public void SetText(string title, string[] details)
    {
        _details = details;
        _title = title;
        gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(true);
        Refresh();
    }

    public void Refresh()
    {
        if (_curidx == 0)
        {
            _textTitle.gameObject.SetActive(true);
            _textDetail[0].gameObject.SetActive(true);
            _textDetail[2].gameObject.SetActive(false);

            _textTitle.text = _title;
            _textDetail[0].text = _details[_curidx * 2 + 0];
        }
        else
        {
            _textTitle.gameObject.SetActive(false);
            _textDetail[0].gameObject.SetActive(false);
            _textDetail[2].gameObject.SetActive(true);

            _textDetail[2].text = _details[_curidx * 2 + 0];
        }

        _textDetail[1].text = "";
        if (_curidx * 2 + 1 < _details.Length)
        {
            _textDetail[1].text = _details[_curidx * 2 + 1];
        }
    }

    public void OnClickLeft()
    {
        if (_curidx > 0)
        {
            _curidx--;
            Refresh();
        }
    }

    public void OnClickRight()
    {
        if ((_curidx + 1) * 2 < _details.Length)
        {
            _curidx++;
            Refresh();
        }
    }
}
