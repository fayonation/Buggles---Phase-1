using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_consumable : MonoBehaviour
{

// public globalManager GlobalManager;
    public bool currency = false;
    public bool health = false;
    float healthStep = 10f;
    public bool runSpeed = false;
    int runSpdStep = 1;
    public bool reload = false;
    float reloadStep = .05f;
    public bool bSpeed = false;
    float bltSpdStep = .1f;
    public bool dmg = false;
    float dmgStep = .1f;
    public bool defence = false;
    int defenceStep = 1;

    void Update()
    {
        transform.Rotate (0,200*Time.deltaTime,0); //rotates 50 degrees per second around z axis
    }

    private void OnTriggerStay(Collider other) {
        // health
        if(other.gameObject.tag=="Player"){
            if(health)
                gotHealth(other.gameObject);
            if(defence)
                gotDefence(other.gameObject);
            if(reload)
                gotReload(other.gameObject);
            if(bSpeed)
                gotbSpeed(other.gameObject);
            if(dmg)
                gotDmg(other.gameObject);
            if(runSpeed)
                gotRunSpeed(other.gameObject);
            if(currency)
                gotCurrency(other.gameObject);
        }
    }

    void gotHealth(GameObject player){
        healthControler dude = player.GetComponent<healthControler>();
        dude.health += healthStep;
        if(dude.health>dude.startHealth){
            dude.health = dude.startHealth;
        }
        
        dude.updateBar();
        Destroy(gameObject);
    }
    
    void gotDefence(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        dude.defence += defenceStep;
        Destroy(gameObject);
    }
    void gotReload(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        dude.reloadMultiplier += reloadStep; // there are 10 in world so limit is 2 
        Destroy(gameObject);
    }
    void gotbSpeed(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        dude.bltSpeedMultiplier += bltSpdStep; // there are 10 in world so limit is 2 
        Destroy(gameObject);
    }
    void gotDmg(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        dude.dmgMultiplier += dmgStep; // there are 10 in world so limit is 2 
        Destroy(gameObject);
    }
    void gotRunSpeed(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        dude.runSpeed += runSpdStep; // there are 10 in world so limit is 2 
        Destroy(gameObject);
    }
    void gotCurrency(GameObject player){
        globalManager GlobalManager =  GameObject.Find("GlobalManager").GetComponent<globalManager> ();
        GlobalManager.flous += 5;
        Debug.Log(GlobalManager.flous);
        Destroy(gameObject);
    }
}
