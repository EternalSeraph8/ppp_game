using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrate
{
    public class LoadGift : MonoBehaviour
    {
        //Position we will move the camera to for art viewing
        public Vector3 artPosition;
        //Case sensitive name of the Narration object assigned to this Penpal
        public string narrationToPlay;
        public Sprite hdPenpalSprite;
        public string penpalName;

        private bool firstPress = true;
        private bool noArt = false;
        private bool isCounted = false;

        private GameObject main_camera;
        private PlayerFollow pfScript;
        private GameObject player;
        private Platformer playerScript;
        private GameObject hd_penpal;
        private DisplayHDPenpal hd_penpal_script;
        private GameObject nametag;
        private DisplayName nametag_display_script;
        private GameObject nametag_bg;
        private DisplayNameBG nametagBG_display_script;

        //Access Manager methods
        private MusicManager MM;
        private GameManager GM;

        // Start is called before the first frame update
        void Start()
        {
            //Cache these as they won't change        
            main_camera = GameObject.Find("Main Camera");
            pfScript = main_camera.GetComponent<PlayerFollow>();
            player = GameObject.Find("PinaPlayer");
            playerScript = player.GetComponent<Platformer>();
            hd_penpal = GameObject.Find("HD_Penpal");
            hd_penpal_script = hd_penpal.GetComponent<DisplayHDPenpal>();
            nametag = GameObject.Find("Nametag");
            nametag_display_script = nametag.GetComponent<DisplayName>();
            nametag_bg = GameObject.Find("Nametag_BG");
            nametagBG_display_script = nametag_bg.GetComponent<DisplayNameBG>();

            GameObject mm = GameObject.Find("MusicManager");
            MM = mm.GetComponent<MusicManager>();
            GameObject gm = GameObject.Find("GameManager");
            GM = gm.GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            //Do nothing unless the player is in range        
            if (IsWithinRange(player, 3.8f, 4f))
            {
                //DEBUG
                //print("player: " + player.transform.position.x + ", " + player.transform.position.y);
                //print("penpal: " + this.transform.position.x + ", " + this.transform.position.y);
                if (Input.GetKeyDown("e") || Input.GetKeyDown("space") || Input.GetKeyDown("enter"))
                {
                    if (artPosition.x == 0 && artPosition.y == 0 && artPosition.z == 0)
                    {
                        //short-circuit firstPress mechanism because there is no art to view
                        noArt = true;
                        //Temporarily pause the camera follow and player movement logic                        
                        pfScript.viewingArt = true;
                        playerScript.viewingArt = true;
                    }

                    if (firstPress == true && !noArt)
                    {
                        //Update flags
                        firstPress = false;

                        //Display art using provided position
                        main_camera.transform.position = artPosition;

                        //Temporarily pause the camera follow and player movement logic                        
                        pfScript.viewingArt = true;                        
                        playerScript.viewingArt = true;

                    }
                    else if (firstPress == false || noArt)
                    {
                        //Assuming we find the Narration object attached, trigger audio and text playback
                        GameObject narration = GameObject.Find(narrationToPlay);
                        OnEnableNarrationTrigger trigger = narration.GetComponent<OnEnableNarrationTrigger>(); 
                        //AudioSource audioSource = bgm.GetComponent<AudioSource>();

                        if (trigger != null)
                        {
                            //Load HD_Penpal sprite
                            hd_penpal_script.loadSprite(hdPenpalSprite);
                            nametag_display_script.setSpriteName(penpalName);
                            nametagBG_display_script.enableTextBG();

                            //Lower BGM audio
                            //audioSource.volume = 0.05f;
                            //Use a fancy fade because it sounds better
                            MM.FadeBGMVolume(0.5f, 0.02f);
                            //Moved to after the dialog finishes
                            //GM.UpdatePenpalCountTracker();

                            //Trigger voiceline and text box
                            trigger.OnEnable();
                        }
                        else
                        {
                            //Unload HD_Penpal sprite
                            hd_penpal_script.unloadSprite();
                            nametag_display_script.unsetSpriteName();
                            nametagBG_display_script.disableTextBG();

                            //Restore BGM audio
                            MM.RestoreBGMVolume(3f);

                            //Return back to the player's position
                            main_camera.transform.position = player.transform.position;

                            if (isCounted == false)
                            {
                                GM.UpdatePenpalCountTracker();
                                isCounted = true;
                            }

                            //Restore the camera follow and player movement logic
                            pfScript.viewingArt = false;
                            playerScript.viewingArt = false;
                        }
                    }
                }
            }            
        }

        //Performs range-checking
        bool IsWithinRange(GameObject p, float x, float y)
        {
            if ((Mathf.Abs(p.transform.position.x - this.transform.position.x) < x) 
                && (Mathf.Abs(p.transform.position.y - this.transform.position.y) < y))
            {
                //DEBUG
                //print(this.name);
                //print("player: " + p.transform.position.x + ", " + p.transform.position.y);
                //print("penpal: " + this.transform.position.x + ", " + this.transform.position.y);
                return true;
            }
            return false;
        }
    }
}

