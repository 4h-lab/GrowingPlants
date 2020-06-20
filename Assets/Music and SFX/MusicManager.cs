using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks;
    [SerializeField] private int changeAfter = 2;
    private AudioSource audio;
    private int songIndex;
    private EventEmitter ee;
    private int sceneIndex;
    private int changeMusic;
    // Start is called before the first frame update
    
    void Start()
    {
        sceneIndex=changeMusic = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(gameObject);
        
        audio = this.GetComponent<AudioSource>();
        songIndex = SceneManager.GetActiveScene().buildIndex % tracks.Length;
        SceneManager.activeSceneChanged += nextScene;
        playNewTrack(sceneIndex);
        
    }

    // Update is called once per frame
    private void stopMusic(Object[] p)
    {
        audio.Stop();
    }

    private void playNewTrack(int index)
    {
        index = index % tracks.Length;
        audio.clip = tracks[index];
        audio.Play();
    }
    private void nextScene(Scene scene, Scene next)
    {
        
        if (sceneIndex==next.buildIndex) return;
        if (next.buildIndex - changeMusic >= changeAfter)
        {
            playNewTrack(next.buildIndex);
            changeMusic = next.buildIndex;
        }
        sceneIndex = next.buildIndex;
        
        
    }
}
