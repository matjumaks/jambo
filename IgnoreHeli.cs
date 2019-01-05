using UnityEngine;
using System.Collections;

public class IgnoreHeli : MonoBehaviour {

	void Start () {
		Physics2D.IgnoreLayerCollision(14, 13);
		Physics2D.IgnoreLayerCollision (14, 14);
	}
}
