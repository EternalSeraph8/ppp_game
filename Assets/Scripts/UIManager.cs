using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager GM;
    public MusicManager MM;

    //private Slider musicSlider;

    void Start()
    {
        //Feature removed
        //musicSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
    }

    void Update()
    {
        ScanForKeyStroke();
    }

    void ScanForKeyStroke()
    {
        if (Input.GetKeyDown("escape"))
        {
            GM.TogglePauseMenu();
        }
    }

    /* Feature removed
    public void MusicSliderUpdate(float val)
    {
        MM.SetVolume(val);
    }
    */
}
