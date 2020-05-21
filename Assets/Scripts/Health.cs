using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class Health : MonoBehaviour{
    public int maxHealth;
    public int health { get; private set; }

    [Tooltip("The delay (in seconds) between the player death and the level reload --> it is used to do stuff like death animations ecc")]
    public float timeBeforeDying = 1f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void damage(int amount) {
        health -= amount;
        if (health > maxHealth) health = maxHealth;
        if (health <= 0) {
            die();
        }
    }
    
    private void die() {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps?.Play();
        StartCoroutine(dead(timeBeforeDying ));
        StartCoroutine(fade(timeBeforeDying ));
        //Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().ControlsEnabled(false);
    }

    private IEnumerator dead(float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator fade(float time) {
        float totalTime = 0;
        GameObject go = GameObject.FindGameObjectWithTag("post-processing-volume");
        DepthOfField dof = null;
        if (go != null) go.GetComponent<PostProcessVolume>().profile.TryGetSettings<DepthOfField>(out dof);

        
        while (totalTime < time) {
            time += Time.deltaTime;
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a = (1 - totalTime / time);
            gameObject.GetComponent<SpriteRenderer>().color = color;

            if (dof != null) {
                Debug.Log("Focus distance --> " + dof.focusDistance.value);
                dof.focusDistance.value = dof.focusDistance.value - Time.deltaTime * 8;
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
