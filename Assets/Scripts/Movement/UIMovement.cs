using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIMovement : Controls
{
    private MovementJoystick player;
    private Image right;
    private Image left;
    private GameObject r;
    private GameObject l;

    private Color white = Color.white;
    private Color Awhite = new Color(1f, 1f, 1f, .65f);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementJoystick>();
        
        r = transform.GetChild(0).gameObject;
        l = transform.GetChild(1).gameObject;
        right = r.GetComponent<Image>();
        left = l.GetComponent<Image>();


        onControlsDisabled += onControlsDisabled_handler;
        onControlsEnabled += onControlsEnabled_handler;
    }

    // Update is called once per frame
    void Update()
    {
        var v = player.getTargetVelocityX();
        if (v > 0)
        {
            right.color = white;
            left.color = Awhite;
        } else if (v < 0)
        {
            right.color = Awhite;
            left.color = white;
        }
        else
        {
            right.color = Awhite;
            left.color = Awhite;
        }
    }

    private void onControlsDisabled_handler(object obj, EventArgs e) {
        l.SetActive(false);
        r.SetActive(false);
    }
    private void onControlsEnabled_handler(object obj, EventArgs e) {
        l.SetActive(true);
        r.SetActive(true);
    }
}
