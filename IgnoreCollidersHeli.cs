using UnityEngine;
using System.Collections;

public class IgnoreCollidersHeli : MonoBehaviour {


	void Start () {
		Physics2D.IgnoreLayerCollision(14, 8);
	}		

}
