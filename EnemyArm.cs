using UnityEngine;
using System.Collections;

public class EnemyArm : MonoBehaviour {
		

	// Update is called once per frame
		public float speed = 5f;

		public Transform target;

		void FixedUpdate()
		{
		if (!target) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;

		} else {
			Vector2 direction = target.position - transform.position;

			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;	// find the angle in degrees

			Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, speed * Time.deltaTime);
			}
		}
}

