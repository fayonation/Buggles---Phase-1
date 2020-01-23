using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public float bulletLife = 10; // seconds... kinda
    public float bulletBirth;
    public float bulletDmg = .1f;


    public void setSpeed(float newSpeed){ // hppens when you instantiate bullet
        speed = newSpeed;
        bulletBirth = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(Time.time >  bulletBirth + bulletLife){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        healthControler livingObject = other.GetComponent<healthControler>();
        if(livingObject != null){
            livingObject.takeDmg(bulletDmg);
        }
        Destroy(gameObject);
    }
    // private void OnTriggerStay(Collider other) {
    //     Destroy(gameObject);
    // }
    // private void OnTriggerStay(Collider other) {
    //     Destroy(gameObject);
    //     Debug.Log(gameObject);
    // }
}
