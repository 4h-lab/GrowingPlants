using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour{
    public int maxHealth;
    public int health { get; private set; } 


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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
