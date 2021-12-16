using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager UI;
    public MusicManager MM;
    public GameObject player;
    public Text progressTracker;
    public Camera main_cam;

    //The sprite we will swap to to end the game
    public Sprite bioreactorSprite;

    private Platformer playerScript;
    private PlayerFollow pfScript;
    private Canvas canvas;
    private SceneSwitch sceneSwitch;
    private Animator cake_anim;
    private GameObject particleSystemGO;
    private ParticleSystem particle_system;

    //Used in progress tracker
    private GameObject carrot_cake;
    private GameObject subtitle_display;
    private GameObject quest_bg;
    private GameObject quest;
    private int totalPenpalCount;
    private int spokenPenpalCount;
    private bool bioTriggered;
    private float triggerTime = 0f; 

    //private Vector3 tempCamPos;

    // Start is called before the first frame update
    void Start()
    {
        canvas = UI.GetComponentInChildren<Canvas>();
        playerScript = player.GetComponent<Platformer>();
        sceneSwitch = GetComponent<SceneSwitch>();
        subtitle_display = GameObject.Find("2DNarrationSystem/SubtitleManager/SubtitleDisplay");
        quest_bg = GameObject.Find("2DNarrationSystem/SubtitleManager/Quest Tracker BG");
        quest = GameObject.Find("2DNarrationSystem/SubtitleManager/Quest Tracker");
        carrot_cake = GameObject.Find("DefinitelyCarrotCake");
        particleSystemGO = GameObject.Find("DefinitelyCarrotCake/Particle System");
        particle_system = particleSystemGO.GetComponent<ParticleSystem>();
        cake_anim = carrot_cake.GetComponent<Animator>();
        pfScript = main_cam.GetComponent<PlayerFollow>();

        var emission = particle_system.emission;
        emission.enabled = false;

        //Progress Tracker
        totalPenpalCount = 32;
#if UNITY_EDITOR
        totalPenpalCount = 2;
#endif
        spokenPenpalCount = 0;
        bioTriggered = false;
    }

    void Update()
    {
        //Timer used to display the Bioreactor animation for only 5 seconds
        if (triggerTime > 0 && Time.time - triggerTime > 5.5)
        {
            pfScript.viewingArt = false;
            playerScript.viewingArt = false;

            //Turn off particles
            var emission = particle_system.emission;
            emission.enabled = false;
        }

        cake_anim.ResetTrigger("BioTriggered");       

        //Terminating condition for progress so that the last Penpal's message doesn't get chopped
        if (spokenPenpalCount == totalPenpalCount && !subtitle_display.activeSelf && !playerScript.viewingArt)
        {           
            //No need to do this every frame, just do it once
            if (!bioTriggered)
            {
                //Set flags to stop player movement and camera tracking, and start 5s timer
                bioTriggered = true;
                pfScript.viewingArt = true;
                playerScript.viewingArt = true;
                triggerTime = Time.time;

                //Position to view the bioreactor animation
                main_cam.transform.position = new Vector3(0,-22,-100);

                //loop one-frame animation instead of swapping sprites
                //SpriteRenderer sr = carrot_cake.GetComponent<SpriteRenderer>();
                //sr.sprite = bioreactorSprite;
                
                //shift cake up to accomidate resized model
                carrot_cake.transform.Translate(0, 2.565f, 0);

                //fade BGM out and play sfx
                MM.FadeBGMVolume(0.5f, 0.02f);
                carrot_cake.GetComponent<AudioSource>().Play();

                //Trigger bioreactor animation                
                cake_anim.SetTrigger("BioTriggered");

                //Trigger particle system
                var emission = particle_system.emission;
                emission.enabled = true;
            }

            //DEBUG
            //print("x: " + Mathf.Abs(carrot_cake.transform.position.x - player.transform.position.x));
            //print("y: " + Mathf.Abs(carrot_cake.transform.position.y - player.transform.position.y));

            //Interaction key
            if ((Input.GetKeyDown("e") || Input.GetKeyDown("space") || Input.GetKeyDown("enter")) && bioTriggered)
            {
                //Check range on Bioreactor.  It's big so large range parameters.
                if (IsWithinRange(carrot_cake, player, 15.2f, 15.2f))
                {
                    quest_bg.SetActive(false);
                    quest.SetActive(false);
                    EndTheGame();
                }
            }
        }
    }

    //Performs range-checking
    private bool IsWithinRange(GameObject bio, GameObject player, float x, float y)
    {
        if ((Mathf.Abs(bio.transform.position.x - player.transform.position.x) < x)
            && (Mathf.Abs(bio.transform.position.y - player.transform.position.y) < y))
        {
            return true;
        }
        return false;
    }

    public void TogglePauseMenu()
    {
        //Only pause if we aren't viewing art
        if (!playerScript.viewingArt)
        {
            //Simply freeze or unfreeze time
            if (canvas.enabled)
            {
                canvas.enabled = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                canvas.enabled = true;
                Time.timeScale = 0f;
            }
        }
    }

    public bool isPaused()
    {
        return canvas.enabled;
    }

    public void UpdatePenpalCountTracker()
    {
        spokenPenpalCount++;
        if (spokenPenpalCount == totalPenpalCount)
        {
            progressTracker.text = spokenPenpalCount + " / " + totalPenpalCount + " Interact with the Bioreactor to continue...";
        }
        else if (spokenPenpalCount < 10)
        {
            progressTracker.text = "0" + spokenPenpalCount + " / " + totalPenpalCount + ": Penpals";
        }
        else
        {
            progressTracker.text = spokenPenpalCount + " / " + totalPenpalCount + ": Penpals";
        }
    }

    public void EndTheGame()
    {
        sceneSwitch.GotoSecretScene();   
    }

    public void RestartGame()
    {
        //Reload the scene
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //First disable the pause menu
        TogglePauseMenu();
    }

    public void ExitGame()
    {
        //Because Application.Quit doesn't work in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
