using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUIManager : MonoBehaviour{
    public GameObject star_1;
    public GameObject star_2;
    public GameObject star_3;

    public void showStars(int number = 0) {
        Animator anim = GetComponent<Animator>();
        if (number > 0) {
            anim.SetBool("s1", true);
        }if (number > 1) {
            anim.SetBool("s2", true);
        }
        if (number > 2) {
            anim.SetBool("s3", true);
        } 
    }


}
