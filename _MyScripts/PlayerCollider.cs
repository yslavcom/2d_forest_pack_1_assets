using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public string _groundTag = "";

    private bool _boOnGround = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag(_groundTag))
        {
            _boOnGround = true;
            print("Grounded");
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(_groundTag))
        {
            _boOnGround = false;
            print("Not grounded");
        }
    }

    public bool BoGrounded { get { return _boOnGround; } }
}