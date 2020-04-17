using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_consumable : MonoBehaviour
{
	globalManager gManager;

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
        if(other.gameObject.tag=="Player"){
		    gManager = GameObject.Find("GlobalManager").GetComponent<globalManager> ();
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
        gManager.defenceBuffs++;
        gManager.aquiredItems[1] = true;
        gManager.inventoryLength();
        dude.updateItems();
        Destroy(gameObject);
    }
    void gotReload(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        gManager.reloadBuffs++;
        gManager.aquiredItems[4] = true;
        gManager.inventoryLength();
        dude.updateItems();
        Destroy(gameObject);
    }
    void gotbSpeed(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        gManager.bltSpeedBuffs++;
        gManager.aquiredItems[2] = true;
        gManager.inventoryLength();
        dude.updateItems();
        Destroy(gameObject);
    }
    void gotDmg(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        gManager.dmgBuffs++;
        gManager.aquiredItems[3] = true;
        gManager.inventoryLength();
        dude.updateItems();
        Destroy(gameObject);
    }
    void gotRunSpeed(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        gManager.runSpeedBuffs++;
        gManager.aquiredItems[5] = true;
        gManager.inventoryLength();
        dude.updateItems();
        Destroy(gameObject);
    }
    void gotCurrency(GameObject player){
        playerController dude = player.GetComponent<playerController> ();
        gManager.bananas+=5;
        gManager.aquiredItems[0] = true;
        gManager.inventoryLength();
        dude.updateItems();
        // Debug.Log(gManager.bananas);
        Destroy(gameObject);
    }
}
