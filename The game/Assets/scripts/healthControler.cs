using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthControler : MonoBehaviour
{
    Transform mainCam;
    [SerializeField] private healthBar hBar;
	private Animator animator;
	private Animator camAnim;
    private playerController controller;
    // public GameObject blood;
    // Sprite bloodV;
    public Image blood;
    public Image deathBg;
    public Text deathNote;
    float bloodOpacity = 0f;
    float deathOpacity = 0f;
    bool alive = true;

    public float startHealth = 1f;
    public float health;


    private void Start() {
        health = startHealth;
        mainCam = Camera.main.transform;
        // making health bar green if not enemy
        if(gameObject.GetComponent<Buggle>()!=null){
            if(!gameObject.GetComponent<Buggle>().enemy){
                gameObject.transform.Find("healthBar").Find("bar").Find("barSprite").GetComponent<SpriteRenderer>().color = Color.green;
            }
        }

    }
    private void Update() {
        controlBloodyDeath();
        faceCamera();
    }
    void faceCamera(){
        var thisHealthBar = gameObject.transform.Find("healthBar");
        if(thisHealthBar)
            thisHealthBar.transform.LookAt(thisHealthBar.transform.position + mainCam.rotation * Vector3.back, mainCam.rotation * Vector3.up);
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


        var buggleDefence = gameObject.GetComponent<Buggle>();
        var playerDefence = gameObject.GetComponent<playerController>();
        var defence = 0;
        // this will make sure that we get the defence of the player or the buggle
        if(buggleDefence != null){
            defence = buggleDefence.defence;
        } else if(playerDefence != null) {
            defence = playerDefence.defence;
        }
        var effectiveDamage = bulletDmg - defence;

        // Debug.Log(effectiveDamage);

        if(effectiveDamage<0)
            effectiveDamage = 0;
        if(alive)
            health = health - effectiveDamage;
        if(health <= 0){
            health = 0;
            
            if(gameObject.tag=="Player")
                death();
            else
                Destroy(gameObject);
        } else {
            animator.SetBool("hit", false);
        }
        
        // Debug.Log(health);
        updateBar();
    }



    public void updateBar(){
        if(hBar != null)
            hBar.setSize(health / startHealth);

    }
    void death(){
        alive = false;
        // animator = gameObject.GetComponent<Animator>();
        animator.SetBool("dead", true);
        animator.Play("death", 0);
        controller = gameObject.GetComponent<playerController> ();
        controller.drop();
        Destroy(GameObject.Find("healthBar"));
        controller.enabled = false;
    }


    IEnumerator stopHurt(float time)
 {
     yield return new WaitForSeconds(time/10);
 
    camAnim.SetBool("shake", false);
 }

    void controlBloodyDeath(){
        if(blood != null){
            if(alive){
                bloodOpacity -= .05f;
                deathOpacity = 0;
                blood.color = new Color(1f, 1f, 1f, bloodOpacity);
            } else{
                if(bloodOpacity<.5f)
                    bloodOpacity += .002f;
                    
                if(deathOpacity<.6f)
                    deathOpacity += .002f;

                blood.color = new Color(0f, 0f, 0f, bloodOpacity);
                deathBg.color = new Color(0f, 0f, 0f, deathOpacity);
                deathNote.color = new Color(.6f, 0f, 0f, deathOpacity);
                // Debug.Log(deathBg.color);
            }
        }
    }
}
