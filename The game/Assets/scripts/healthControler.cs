using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthControler : MonoBehaviour
{
    [SerializeField] private healthBar hBar;
	private Animator animator;
	private Animator camAnim;
    private playerController controller;
    // public GameObject blood;
    // Sprite bloodV;
    public Image blood;
    float bloodOpacity = 0f;
    bool alive = true;

    public float startHealth = 1f;
    public float health;


    private void Start() {
        // hBar.setSize(health);
        health = startHealth;
    }
    private void Update() {
        if(blood != null){
            if(alive){
                bloodOpacity -= .05f;
                blood.color = new Color(1f, 1f, 1f, bloodOpacity);
            } else{
                bloodOpacity += .002f;
                blood.color = new Color(0f, 0f, 0f, bloodOpacity);
            }
        }
    }
    public void takeDmg(float bulletDmg){
        animator = gameObject.GetComponent<Animator>();
        camAnim = Camera.main.GetComponent<Animator>();
        animator.SetBool("hit", true);

        if(gameObject.tag=="Player"){
            camAnim.SetBool("shake", true);
            if(alive)
                bloodOpacity = .4f;
            StartCoroutine(stopHurt(1));
        }
        health = health - bulletDmg/1000000;
        if(health <= 0){
            health = 0;
            death();
        } else {
            animator.SetBool("hit", false);
        }
        if(hBar != null)
            hBar.setSize(health / startHealth);
    }



    void playerDmg(){

    }
    void death(){
        alive = false;
        // animator = gameObject.GetComponent<Animator>();
        animator.SetBool("dead", true);
        animator.Play("death", 0);
        controller = gameObject.GetComponent<playerController> ();
        Destroy(GameObject.Find("healthBar"));
        controller.enabled = false;
    }


    IEnumerator stopHurt(float time)
 {
     yield return new WaitForSeconds(time/10);
 
    camAnim.SetBool("shake", false);
 }
}
