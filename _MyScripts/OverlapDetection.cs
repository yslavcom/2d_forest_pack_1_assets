using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetection : MonoBehaviour {

	public void DetectWithinSphere(Vector2 centre, float radius, out Collider2D [] detectColliders)
    {
        detectColliders = Physics2D.OverlapCircleAll(centre, radius);
    }
}
