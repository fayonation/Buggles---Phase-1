using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (healthControler))]
public class playerController : MonoBehaviour
{
	// [SerializeField] private Material hightlightedMaterial;
   	[Tooltip("Is this character currently controllable by the player"), SerializeField]
	public bool isHandlingInput = true;
	public bool isMoving = false;
	public float fallMultiplier = 10f;
	private float jumpTimeLimit;
	GameObject thingToGrab;
	float throwForce = 250f;
	public GameObject HoldingForceField;
	GameObject selector;
	GameObject newHoldingForceField = null;
	public bool isHolding = false;
	healthControler playerHealthControler;
	public int defence = 0;
	public float reloadMultiplier = 1;
	public float dmgMultiplier = 1;
	public float bltSpeedMultiplier = 1; // bullet runSpeed, got with drops
	public float runSpeedMultiplier = 1; // run runSpeed got by holding runSpeed buggle

	[Tooltip("The runSpeed the player will move"), SerializeField]
	public int runSpeed = 15;
	private Vector3 movement;
	[Tooltip("The amount of upwards force to apply to the character when they jump"), SerializeField]
	private float jumpVelocity = 20;
	[Tooltip("If the player lets go of the jump button, their y velocity will be reduced to this number."), SerializeField]
	private float jumpReduction = 10;
	[Tooltip("The maximum velocity the character should be able to reach"), SerializeField]
	private Vector3 maxVelocity = new Vector3(0,0,0);

	private bool hasJumped = false; 
	private bool cutJumpShort = false;
	private bool isGrounded;
	private bool holding = false;

	private Rigidbody rigidbody;
	private Animator animator;
	[Tooltip("The character will consider anything in this LayerMask to be 'Ground'"), SerializeField]
	private LayerMask groundLayerMask;

	void Awake () {
		rigidbody = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator>();
		playerHealthControler = GetComponent<healthControler> ();

	}

	void FixedUpdate () {
		ApplyJumpPhysics ();
		CapVelocity ();
		
        if (Input.GetKeyUp(KeyCode.E)){
			if(!isHolding)
				grab();
			else{
				drop();
			}
		}
	}
    private void LateUpdate() {
        toggleAnimations();
    }
		
	void Update () {
		move();
		
	}

	void move(){
		if (movement != Vector3.zero) {
			rigidbody.transform.rotation = Quaternion.LookRotation (movement);
            isMoving = true;
		} else{
            isMoving = false;
        }

		float horizontalInput = Input.GetAxisRaw ("Horizontal");
		float verticalInput = Input.GetAxisRaw ("Vertical");
		ManageMovement (horizontalInput, verticalInput);

		if (Input.GetButtonDown ("Jump") && isGrounded) {
			Jump ();
		}

		if (Input.GetButtonUp ("Jump") && !isGrounded) {
			CutJumpShort ();
		}
	}
	public void ManageMovement (float h, float v) {
		if (!isHandlingInput) {
			return;
		}

		Vector3 forwardMove = Vector3.Cross (Camera.main.transform.right, Vector3.up); // gives the direction of movement from the two vectors
		Vector3 horizontalMove = Camera.main.transform.right;

		movement = forwardMove * v + horizontalMove * h;


		movement = movement.normalized * runSpeed * Time.deltaTime;
		rigidbody.MovePosition (transform.position + movement);
	}

	public void Jump () {
		hasJumped = true;
	}
	public void CutJumpShort () {
		cutJumpShort = true;
	}
	private void ApplyJumpPhysics () {
		if (hasJumped) {
			
			rigidbody.velocity = Vector3.up * jumpVelocity;
			// rigidbody.velocity = new Vector3 (rigidbody.velocity.x, jumpVelocity, rigidbody.velocity.z);
			hasJumped = false;
		}
		// if(rigidbody.velocity.y < 0){
			rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		// }

		if (cutJumpShort) {
			if (rigidbody.velocity.y > jumpReduction) {
				rigidbody.velocity = new Vector3 (rigidbody.velocity.x, jumpReduction, rigidbody.velocity.z);
			}
			cutJumpShort = false;
		}
	}

	void CapVelocity () {
		Vector3 _velocity = GetComponent<Rigidbody> ().velocity;
		_velocity.x = Mathf.Clamp (_velocity.x, -maxVelocity.x, maxVelocity.x);
		_velocity.y = Mathf.Clamp (_velocity.y, -maxVelocity.y, maxVelocity.y);
		_velocity.z = Mathf.Clamp (_velocity.z, -maxVelocity.z, maxVelocity.z);
		rigidbody.velocity = _velocity;
	}

    void toggleAnimations() {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("hasJumped", hasJumped);
        animator.SetBool("isGrounded", isGrounded);
        // animator.SetBool("cutJumpShort", cutJumpShort);
    }





	void grab(){
		if(thingToGrab!=null){
			Transform holdPosition = GameObject.Find("buggleHold").GetComponent<Transform>();
			isHolding = true;
			// HoldingForceField
            newHoldingForceField = Instantiate(HoldingForceField, holdPosition.position, holdPosition.rotation);
			newHoldingForceField.transform.parent = holdPosition;
			thingToGrab.GetComponent<shootController>().held = true;
			thingToGrab.GetComponent<Rigidbody>().isKinematic = true;
			thingToGrab.GetComponent<Collider>().enabled = false;
			thingToGrab.GetComponent<Buggle>().allowedToShoot = false;
			thingToGrab.transform.parent = holdPosition;
			thingToGrab.GetComponent<Rigidbody>().velocity = rigidbody.velocity;
			thingToGrab.transform.position = holdPosition.position;
			thingToGrab.transform.rotation = holdPosition.rotation;
			//give picked up buggle same stats
			thingToGrab.GetComponent<Buggle>().defence = defence;
			thingToGrab.GetComponent<Buggle>().reloadMultiplier = reloadMultiplier;
			thingToGrab.GetComponent<Buggle>().bltSpeedMultiplier = bltSpeedMultiplier;
			thingToGrab.GetComponent<Buggle>().runSpeedMultiplier = runSpeedMultiplier;
			thingToGrab.GetComponent<Buggle>().dmgMultiplier = dmgMultiplier;

		}
	}
	void drop(){
		isHolding = false;
		Destroy(newHoldingForceField);
		thingToGrab.GetComponent<shootController>().held = false;
		thingToGrab.GetComponent<Rigidbody>().isKinematic = false;
		thingToGrab.GetComponent<Collider>().enabled = true;
		thingToGrab.GetComponent<Buggle>().allowedToShoot = true;
		thingToGrab.transform.parent = null;
		selector.GetComponent<Renderer>().enabled = false;
		if(isMoving)
			thingToGrab.GetComponent<Rigidbody>().AddForce((transform.forward*2 + transform.up) * throwForce);
		// velocity is back on its own
		thingToGrab = null;
		
	}
	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "ground")
			isGrounded = true;	


	}
    private void OnTriggerStay(Collider other)
    {
		if(other.gameObject.tag == "buggle") {
			Buggle theBuggle = other.GetComponent<Buggle>();

			if(!theBuggle.enemy){
				if(selector==null)
					selector = GameObject.Find("selector");
				selector.GetComponent<Renderer>().enabled = true;
				if(thingToGrab ==null)
					thingToGrab = other.gameObject;
			}else{
				playerHealthControler.takeDmg(theBuggle.touchDmg);
			}
		}
		if(other.gameObject.tag == "ground")
			isGrounded = true;
    }

    private void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "ground")
			isGrounded = false;

		if(other.gameObject.tag == "buggle") {
        	GameObject selector = GameObject.Find("selector");
			selector.GetComponent<Renderer>().enabled = false;
			thingToGrab = null;
		}
	}

}
