using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Jump : MonoBehaviour {

	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	[SerializeField] private LayerMask m_WhatIsGround;

	[SerializeField] private float m_JumpForce = 400f; 

	// Use this for initialization
	void Awake() {
		// Setting up references.
		m_GroundCheck = transform.Find("GroundCheck");
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool wasGrounded = m_Grounded;

		m_Grounded = true;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders [i].gameObject != gameObject) {
				m_Grounded = false;
				m_Rigidbody2D.AddForce (new Vector2 (0f, m_JumpForce));
			} 
		}
	}
}
