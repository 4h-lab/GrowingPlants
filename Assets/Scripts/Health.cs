using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class Health : MonoBehaviour{
    public int maxHealth;
    public int health { get; private set; }

    private Material m;
    private float dissolve = 1.2f;

    private bool isDead = false;


    [Tooltip("The delay (in seconds) between the player death and the level reload --> it is used to do stuff like death animations ecc")]
    public float timeBeforeDying = 1f;
    public Shader dissolver;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        //this.gameObject.GetComponent<SpriteRenderer>().material.shader = dissolver;
        if (dissolver != null) {
            Material m = new Material(dissolver);
            gameObject.GetComponent<Renderer>().material = m;

        }
    
    }

    public void damage(int amount) {
        health -= amount;
        if (health > maxHealth) health = maxHealth;
        if (health <= 0) {
            die();
        }
    }
    
    private void die() {
        if (isDead) return;
        isDead = true;
        //GameObject.FindGameObjectWithTag("Player").transform.Find("Sprite").gameObject.AddComponent<testDissolver>();
        foreach (SpriteRenderer x in GetComponentsInChildren<SpriteRenderer>()) {
            x.material.SetFloat("_Seed", Random.Range(0, 100));
            x.gameObject.AddComponent<testDissolver>();
        }

        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps?.Play();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().ControlsEnabled(false);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().setWinPause(true);

        StartCoroutine(dead(timeBeforeDying ));
        //StartCoroutine(fade(timeBeforeDying ));
        //Destroy(this.gameObject);

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
