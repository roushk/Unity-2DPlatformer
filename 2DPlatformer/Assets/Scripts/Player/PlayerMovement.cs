using UnityEngine;
using System.Collections;
using Prime31;


public class PlayerMovement : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	[HideInInspector]
	private float normalizedVerticalSpeed = 0;

	[HideInInspector]
	private bool playerJumped = false;


	private CharacterController2D controller;
	private Animator animator;
	private RaycastHit2D lastControllerColliderHit;
	private Vector3 velocity;

	private float notGroundedTimer = 0;
	private float minFallingToAnimate = 0.1f;	//100 ms

	enum PlayerFacing
	{
		Left,
		Right
	}
	private PlayerFacing playerFacing = PlayerFacing.Right;

	void Awake()
	{
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		controller.onControllerCollidedEvent += onControllerCollider;
		controller.onTriggerEnterEvent += onTriggerEnterEvent;
		controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{

/****************************************************************************************************************/
//													Movement													//
/****************************************************************************************************************/
		//if on the ground
		if(controller.isGrounded)
		{
			velocity.y = 0;
			notGroundedTimer = 0;
		}
		else
        {
			notGroundedTimer += Time.deltaTime;
        }
		//animator.SetBool("IsFalling", !controller.isGrounded && notGroundedTimer > minFallingToAnimate);
		animator.SetBool("IsFalling", false);
		if (!controller.isGrounded && notGroundedTimer > minFallingToAnimate)
        {
			animator.SetBool("IsFalling", true);
		}

		normalizedHorizontalSpeed = Input.GetAxis("Horizontal");
		normalizedVerticalSpeed = Input.GetAxis("Vertical");
		playerJumped = Input.GetKeyDown(KeyCode.Space);


		PlayerFacing oldFacing = playerFacing;

		//Else if because if not moving we want to face the same direction
		if(velocity.x > 0f && normalizedHorizontalSpeed > 0)
		{
			playerFacing = PlayerFacing.Right;
		}
		else if (velocity.x < 0f && normalizedHorizontalSpeed < 0)
		{
			playerFacing = PlayerFacing.Left;
		}
		

		//if we are now facing a new direction
		if(oldFacing != playerFacing)
		{
			transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}

		// we can only jump whilst grounded
		if(controller.isGrounded && (normalizedVerticalSpeed > 0 || playerJumped))
		{
			velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			animator.SetTrigger("PlayerJumped");
		}


		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		velocity.x = Mathf.Lerp( velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if(controller.isGrounded && normalizedVerticalSpeed < 0)
		{
			velocity.y *= 3f;
			controller.ignoreOneWayPlatformsThisFrame = true;
		}

		//now update blocking
		bool isGuarding = Input.GetAxis("Fire2") > 0;
		animator.SetBool("IsGuarding", isGuarding);

		if(isGuarding && controller.isGrounded)
		{
			velocity.x = 0;
			velocity.y = 0;			
		}

		//Only set running if we are moving and grounded
		animator.SetBool("IsRunning", normalizedHorizontalSpeed != 0 && (controller.isGrounded || notGroundedTimer < minFallingToAnimate) && velocity.x != 0);
		
		controller.move( velocity * Time.deltaTime );

		// grab our current velocity to use as a base for all calculations
		velocity = controller.velocity;

/****************************************************************************************************************/
//													Attacks														//
/****************************************************************************************************************/

		if(Input.GetMouseButtonDown(0))
		{
			//animator.SetTrigger("SlashAttack");
			animator.Play("SlashAttack");
		}
		else if(Input.GetKeyDown(KeyCode.Q))
		{
			//animator.SetTrigger("StabAttack");
			animator.Play("StabAttack");

		}
		else if(Input.GetKeyDown(KeyCode.E))
		{
			//animator.SetTrigger("OverheadAttack");
			animator.Play("OverheadAttack");
		}	
		
	}
}
