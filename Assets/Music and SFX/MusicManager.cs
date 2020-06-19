using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks;
    private AudioSource audio;
    private int songIndex;
    // Start is called before the first frame update
    
    void Start()
    {
        songIndex = SceneManager.GetActiveScene().buildIndex % tracks.Length;
        audio = this.GetComponent<AudioSource>();
        audio.clip = tracks[songIndex];
        audio.Play();
        
    }

    // Update is called once per frame
    
}
