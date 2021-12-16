using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject player;
    private Platformer playerScript;

    //Used to store the volume level so that we can restore it after a voiceMessage finishes
    private float desiredVolume;
    private AudioSource bgm;
    //private GameObject narration_system;
    private AudioSource narrationAudio;
    private bool fadingMusicNow;

    void Start()
    {
        bgm = GetComponentInChildren<AudioSource>();
        bgm.volume = 0.2f;
        desiredVolume = 0.2f;

        GameObject narration_system = GameObject.Find("2DNarrationSystem");
        narrationAudio = narration_system.GetComponent<AudioSource>();
        playerScript = player.GetComponent<Platformer>();
    }

    public void SetVolume(float val)
    {
        //If we're viewing art then don't change the audio
        desiredVolume = val;
        if (!playerScript.viewingArt)
        {
            bgm.volume = val;
            narrationAudio.volume = val;            
        }
    }

    public float GetVolume()
    {
        return desiredVolume;
    }

    public void FadeBGMVolume(float duration, float targetVol)
    {
        StartCoroutine(FadeAudioSource.StartFade(bgm, duration, targetVol));
    }

    public void RestoreBGMVolume(float duration)
    {
        StartCoroutine(FadeAudioSource.StartFade(bgm, duration, desiredVolume));
    }
}
