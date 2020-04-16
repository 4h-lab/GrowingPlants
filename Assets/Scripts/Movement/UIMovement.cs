using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMovement : MonoBehaviour
{
    private PlayerPlatformerController player;
    private Image right;
    private Image left;

    private Color white = Color.white;
    private Color Awhite = new Color(1f, 1f, 1f, .65f);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>();
        right = transform.GetChild(0).gameObject.GetComponent<Image>();
        left = transform.GetChild(1).gameObject.GetComponent<Image>();
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
}
