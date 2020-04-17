using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buggle : MonoBehaviour
{
    shootController shootController;
    Animator anim;

	public int defence = 0;
	public float reloadMultiplier = 1;
	public float dmgMultiplier = 1;
	public float bltSpeedMultiplier = 1; // bullet speed, got with drops
	public float runSpeedMultiplier = 1; // run speed got by holding speed buggle
    public bool allowedToShoot = true;
    public bool buffed = false;
    public float buffTime = 0f;
    public bool enemy = false;
    public float touchDmg = 1f;
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
            shootController.reloadMultiplier = reloadMultiplier;
            shootController.dmgMultiplier = dmgMultiplier;
            shootController.bltSpeedMultiplier = bltSpeedMultiplier;
            shootController.shoot();
        }
        toggleAnimations();
        if(buffTime <= 0){
            noBuff();
        } else {
            buffTime -= 0.02f;
        }
    }

    void toggleAnimations() {
        anim.SetBool("allowedToShoot", allowedToShoot);
        // animator.SetBool("cutJumpShort", cutJumpShort);
    }
    void noBuff() {
        if(gameObject.transform.Find("buffAura(Clone)")!=null){
            GameObject oldAura = gameObject.transform.Find("buffAura(Clone)").gameObject;
			defence = 0;
			reloadMultiplier = 1;
			bltSpeedMultiplier = 1;
			runSpeedMultiplier = 1;
			dmgMultiplier = 1;
            Destroy(oldAura);
        }
    }

}
