using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthControler : MonoBehaviour
{
    [SerializeField] private healthBar hBar;

    public float startHealth = 1f;
    public float health;


    private void Start() {
        // hBar.setSize(health);
        health = startHealth;
    }
    private void Update() {
    }
    public void takeDmg(float bulletDmg){
        if(health>0f){
            health = health - bulletDmg/1000000;
            if(health <= 0)
                Destroy(gameObject);
        }
        hBar.setSize(health / startHealth);
    }
}
