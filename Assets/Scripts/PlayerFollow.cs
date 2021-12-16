using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public bool viewingArt;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PinaPlayer");
        offset = new Vector3(0, 0, -60); //hardcode the offset so the camera snaps back to the player if we move
        //offset = transform.position - player.transform.position;
        viewingArt = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera tracking logic using player position and assuming a fixed Z position (-100)

        //DEBUG
        //print("player: " + player.transform.position.x + ", " + player.transform.position.y);
        //print("camera: " + this.transform.position.x + ", " + this.transform.position.y);

        //Simply follow the player for now
        if (!viewingArt)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
