using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowCredits : MonoBehaviour
{
    private Canvas canvas;
    private GameObject scroll_view;

    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        scroll_view = GameObject.Find("Main Camera/Canvas/Scroll View");
    }

    public void ToggleCredits()
    {
        if (!scroll_view.activeSelf)
        {
            scroll_view.SetActive(true);
        }
        else
        {
            scroll_view.SetActive(false);
        }
    }
    
}
