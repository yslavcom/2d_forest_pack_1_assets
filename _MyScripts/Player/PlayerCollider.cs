using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if false

public class PlayerCollider : MonoBehaviour
{
    public string _groundTag = "";
    public string _collider_object_name = "";

    [SerializeField]
    private bool _boOnGround = false;

    public string _test_other_name = "";
    public string _test_other_collider_name = "";

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == _groundTag)
        {
            print("Grounded");
            //if (other.gameObject.name == _collider_object_name)
            //{
            //    Destroy(other.gameObject);
            //}
            //else
            {
                _test_other_name = other.collider.name;
                _test_other_collider_name = other.otherCollider.name;
                _boOnGround = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == _groundTag)
        {
            _boOnGround = false;
            print("Not grounded");
        }
    }

    public bool BoGrounded { get { return _boOnGround; } }

}

#endif
