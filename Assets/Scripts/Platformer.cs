using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platformer : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public bool viewingArt;

    private Animator anim;
    private bool gamePaused;

    //Pause Menu
    private GameObject pauseUI;
    private GameObject exitGame;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pauseUI = GameObject.Find("PauseUI");
        exitGame = GameObject.Find("ExitGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (!viewingArt)
        {
            Move();
        }
        else
        {
            //Fixes a bug where Pina continues walking off the planet if you are holding a key while you interact with a penpal
            rb.velocity = new Vector2(0, 0);

            //Fixes a bug where Pina continues looping her movement animations while viewing art
            ResetMovementTriggers();
            anim.SetTrigger("stopped");
        }
    }

    //IKZ
    void Move()
    {
        //Translational movement in X and Y
        float x = Input.GetAxisRaw("Horizontal");
        float moveByX = x * speed;

        float y = Input.GetAxisRaw("Vertical");
        float moveByY = y * speed;

        rb.velocity = new Vector2(moveByX, moveByY);

        //Animation State Machine

        ResetMovementTriggers();
        //Set facing triggers
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            //Reset facing triggers
            anim.ResetTrigger("facing_down");
            anim.ResetTrigger("facing_left");
            anim.ResetTrigger("facing_right");

            anim.SetTrigger("moving_up");
            anim.SetTrigger("facing_up");
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            //Reset facing triggers
            anim.ResetTrigger("facing_up");
            anim.ResetTrigger("facing_left");
            anim.ResetTrigger("facing_right");

            anim.SetTrigger("moving_down");
            anim.SetTrigger("facing_down");
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //Reset facing triggers
            anim.ResetTrigger("facing_down");
            anim.ResetTrigger("facing_up");
            anim.ResetTrigger("facing_right");

            anim.SetTrigger("moving_left");
            anim.SetTrigger("facing_left");
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //Reset facing triggers
            anim.ResetTrigger("facing_down");
            anim.ResetTrigger("facing_left");
            anim.ResetTrigger("facing_up");

            anim.SetTrigger("moving_right");
            anim.SetTrigger("facing_right");
        }

        //Set movement triggers
        if ((moveByX == 0 && moveByY == 0))
        {
            //We are stopped, play an idle animation
            anim.SetTrigger("stopped");
        }
        else
        {
            //Halt idle triggers
            anim.ResetTrigger("stopped");
        }
    }

    //Reset movement triggers before setting them based on input
    void ResetMovementTriggers()
    {
        anim.ResetTrigger("moving_down");
        anim.ResetTrigger("moving_up");
        anim.ResetTrigger("moving_left");
        anim.ResetTrigger("moving_right");
    }
}
