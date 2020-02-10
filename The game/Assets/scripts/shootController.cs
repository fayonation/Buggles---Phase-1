using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootController : MonoBehaviour
{
    public Transform bulletOrigin;
    public projectile bullet;
    [HideInInspector] public bool held = false;
    [HideInInspector] public float reloadMultiplier = 1;  // done
	[HideInInspector] public float dmgMultiplier = 1; // done
	[HideInInspector] public float bltSpeedMultiplier = 1;  // done
    public float msBetweenShots = 1500;
    public float bulletVelocity = 20;
    public float nextShotTime;
    float randomness;

    private void Start() {
        randomness = Random.Range(-1.0f, 1.0f);
    }
    private void Update() {
        // if(Input.GetMouseButton(0))
        //     shoot();
    }
    public void shoot(){
        if(Time.time > nextShotTime){
            nextShotTime = randomness + Time.time + (msBetweenShots*(2-reloadMultiplier))/1000; // milliseconds to seconds, =  1500 * ( 2 - 1+0.05 * 10) which is half speed
            randomness = 0;
            projectile newBullet = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation) as projectile;
            newBullet.setSpeed(bulletVelocity * bltSpeedMultiplier); // simple, speed * 1 or 1.1 all the way up tp 2 double speed
            newBullet.bulletDmg = newBullet.bulletDmg * dmgMultiplier;
        }
    }
}
