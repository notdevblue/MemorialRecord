using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLibrary : MonoBehaviour
{
    [SerializeField] LayerMask whatIsBook;
    [SerializeField] Camera libcam;
    [SerializeField] Camera mainCam;
    [SerializeField] LibraryBook[] books;
    [SerializeField] Transform[] contents;
    [SerializeField] PanelLibrary_Content[] outlines;

    [SerializeField] PanelLibrary_Detail detailPanel;

    [SerializeField] Button btnClose;

    private void Awake()
    {
        for (int i = 0; i < outlines.Length; i++)
        {
            int y = i;
            outlines[i].onContentClick += () => detailPanel.SetText(books[y].title, books[y].detail[0], books[y].detail[1]);
        }

        btnClose.onClick.AddListener(() =>
        {
            FindObjectOfType<SlideEffector>().SlideInFrom(Direction.Bottom, 1f, () =>
            {
                transform.gameObject.SetActive(false);
                mainCam.gameObject.SetActive(true);

                FindObjectOfType<SlideEffector>().SlideOutTo(Direction.Bottom, 1f);
            });
        });
    }

    private void OnEnable()
    {
        detailPanel.transform.parent.gameObject.SetActive(true);

        int level = 0;
        for (int i = 0; i < outlines.Length; i++)
        {
            SaveManager.TryGetContentLevel(DataType.Book, i, out level);

            outlines[i].gameObject.SetActive(level >= 100);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(libcam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100, whatIsBook);

            if(hit)
            {
                PanelLibrary_Content library_Content = hit.transform.gameObject.GetComponent<PanelLibrary_Content>();

                if (library_Content)
                {
                    library_Content.onContentClick();
                }
            }
        }
    }
}

[System.Serializable]
public class LibraryBook
{
    public string title;
    public string[] detail;
}