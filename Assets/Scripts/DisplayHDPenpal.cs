using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHDPenpal : MonoBehaviour
{
    private Image hd_penpal;
    //private Sprite sprite1;
    //private Sprite sprite2;

    // Start is called before the first frame update
    void Start()
    {
        //HD Penpal will be empty at the start
        hd_penpal = GetComponent<Image>();

        //sprite1 = Resources.Load<Sprite>("texture1");
        //sprite2 = Resources.Load<Sprite>("texture2");
    }

    public void loadSprite(Sprite spriteToLoad)
    {
        if (spriteToLoad == null)
        {
            unloadSprite();
            return;
        }

        //If this is slow, manually load all the sprites into memory first
        hd_penpal.enabled = true;
        hd_penpal.overrideSprite = spriteToLoad;
    }

    public void unloadSprite()
    {
        hd_penpal.overrideSprite = null;
        //hideous box displayed if we don't disable this
        hd_penpal.enabled = false;
    }
}
