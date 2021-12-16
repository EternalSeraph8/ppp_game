using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNameBG : MonoBehaviour
{
    private Image nametag_bg;

    // Start is called before the first frame update
    void Start()
    {
        nametag_bg = GetComponent<Image>();
    }

    // Enable nametagBG when nametag is enabled
    public void enableTextBG()
    {
        nametag_bg.enabled = true;
    }

    public void disableTextBG()
    {
        //disable nametagBG again
        nametag_bg.enabled = false;
    }
}
