using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {


	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character


	AudioManager audioManager;

	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.


	private Animator m_Anim;            // Reference to the player's animator component.
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.

	bool playOnce;

	Transform playerGraphics;
	 
	public float xSpeed;
	public float xSpeed2;


	private void Awake()
	{
		// Setting up references.
		m_GroundCheck = transform.Find("GroundCheck");

		m_Anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		playerGraphics = transform.FindChild("Graphics");
		if (playerGraphics == null) {
			Debug.LogError ("no graphics");
		}
	}



	void Start ()
	{
		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
		}
	}



	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;

		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;

		}
		m_Anim.SetBool("Ground", m_Grounded);


		if (m_Rigidbody2D.velocity.y > 1)
			playOnce = true;

		xSpeed = m_Rigidbody2D.velocity.x;
		if (xSpeed < 1) {
			xSpeed *= -1;
		}

		// Set the vertical animation
		m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
		m_Anim.SetFloat("Speed", xSpeed);
	}
		

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = playerGraphics.localScale;
		theScale.x *= -1;
		playerGraphics.localScale = theScale;
	}
}
