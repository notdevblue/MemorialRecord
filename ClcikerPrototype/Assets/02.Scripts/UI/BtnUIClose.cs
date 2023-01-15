using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnUIClose : MonoBehaviour
{
    SlideEffector effector = null;
    Button btnClose = null;

    private void Start()
    {
        btnClose = GetComponent<Button>();
        effector = FindObjectOfType<SlideEffector>();
    }

    public void OnclickUIClose()
    {
        
    }
}
