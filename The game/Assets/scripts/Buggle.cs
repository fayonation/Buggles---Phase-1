using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buggle : MonoBehaviour
{
    shootController shootController;
    Animator anim;
    public bool allowedToShoot = true;
    public bool enemy = false;
    public float touchDmg = 200f;
    // Start is called before the first frame update
    void Start()
    {
		shootController = GetComponent<shootController> ();
		anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(allowedToShoot){
            shootController.shoot();
        }
        toggleAnimations();
    }

    void toggleAnimations() {
        anim.SetBool("allowedToShoot", allowedToShoot);
        // animator.SetBool("cutJumpShort", cutJumpShort);
    }
}
