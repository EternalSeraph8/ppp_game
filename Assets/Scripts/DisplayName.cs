using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    public Text nametag;

    // Start is called before the first frame update
    void Start()
    {
        nametag = GetComponent<Text>();
    }

    public void setSpriteName(string name)
    {
        //If this is slow, manually load all the sprites into memory first
        nametag.enabled = true;
        nametag.text = name;
    }

    public void unsetSpriteName()
    {
        //disable nametag again
        nametag.text = null;
        nametag.enabled = false;
    }
}
