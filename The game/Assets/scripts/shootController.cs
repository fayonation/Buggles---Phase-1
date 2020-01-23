using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootController : MonoBehaviour
{
    public Transform bulletOrigin;
    public projectile bullet;
    public float msBetweenShots = 1500;
    public float bulletVelocity = 20;
    public float nextShotTime;

    private void Update() {
        // if(Input.GetMouseButton(0))
        //     shoot();
    }
    public void shoot(){
        if(Time.time > nextShotTime){
            nextShotTime = Time.time + msBetweenShots/1000; // milliseconds to seconds
            projectile newBullet = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation) as projectile;
            newBullet.setSpeed(bulletVelocity);
        }
    }
}
