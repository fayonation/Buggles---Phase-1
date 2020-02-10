using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public GameObject muzzle;
    public GameObject hitSplash;
    public float speed;
    public float bulletDmg = 1f;
    public float bulletLife = 10; // seconds... kinda
    public float bulletBirth;

    private void Start() {
        if(muzzle!=null){
            var newMuzzle = Instantiate(muzzle, transform.position, Quaternion.identity);
            newMuzzle.transform.forward = gameObject.transform.forward;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(Time.time >  bulletBirth + bulletLife){
            Destroy(gameObject);
        }
    }


    public void setSpeed(float newSpeed){ // hppens when you instantiate bullet
        speed = newSpeed;
        bulletBirth = Time.time;
    }
    private void OnTriggerEnter(Collider other) {
        healthControler livingObject = other.GetComponent<healthControler>();
        if(livingObject != null){
            livingObject.takeDmg(bulletDmg);
        }
        
        if(hitSplash!=null){
            var newHitSplash = Instantiate(hitSplash, transform.position, Quaternion.identity);
        }

        if(other.transform.parent != null){
            drop_consumable drop = other.transform.parent.GetComponent<drop_consumable>(); 
            if(drop == null)
                Destroy(gameObject);
        } else {
                Destroy(gameObject);
        }
    }
}
