using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class Player : MonoBehaviour {

    public float _speed = 5;
    public int _rotation_speed = 5;

    public Transform _graphics;
    public SkeletonAnimation _skeletonAnimation;
    public TouchOrMouseInput _touch_or_mouse;

    public float _y_force_scale = 0.01f;
    public float _x_force_scale = 0.01f;

    public LayerMask _groundLayer;

    private Rigidbody2D _rigidBody2D;
    private string _currentAnimation = "";
    private Quaternion _goalRotation = Quaternion.identity;
    private Camera _cam;

    [SerializeField]
    private bool _boGrounded = false;

    private Vector2 _old_velocity = Vector2.zero;
    [SerializeField]
    private bool _boInJump = false;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _cam = Camera.main;

      //  _playerCollider = GetComponent<PlayerCollider>();
    }

    void Update()
    {
        TouchOrMouseInput.StTap tap = _touch_or_mouse.Tap;
        Vector3 new_position = _graphics.transform.position;
        if (tap.Tap)
        {
            _goalRotation = ApplyTap(tap, out new_position);
        }
        else
        {
            _goalRotation = ApplyKeyLeftRight(out new_position);
        }
        //if (_boInJump)
        {
            _graphics.transform.position = new_position;
        }

        _graphics.localRotation = Quaternion.Lerp(_graphics.localRotation, _goalRotation, _rotation_speed * Time.deltaTime);
    }

    void SetAnimation( string name, bool loop)
    {
        if (name == _currentAnimation) return;

        _skeletonAnimation.state.SetAnimation(0, name, loop);
        _currentAnimation = name;
    }

    void FixedUpdate () {
        var velocity = _rigidBody2D.velocity;
        _boInJump = (_old_velocity.y != velocity.y);
        _old_velocity = velocity;

        _boGrounded = IsGrounded();
    }

#region PrivateHelpFunctions
    Quaternion ApplyTap(TouchOrMouseInput.StTap tap, out Vector3 new_position)
    {
        Quaternion goal_rotation = _goalRotation;

        //this should convert from pixels to world coordinates
        float mouse_tap_x = tap.Coord.x;
        var objPos = _graphics.transform.position;
        Vector2 object_in_pixels = (Vector2)_cam.WorldToScreenPoint(objPos);

        float x = 0;
        if (mouse_tap_x > object_in_pixels.x)
        {
            x = _speed * Time.deltaTime;
            goal_rotation = Quaternion.Euler(0, 0, 0);
            SetAnimation("walk", true);
        }
        else if (mouse_tap_x < object_in_pixels.x)
        {
            x = -_speed * Time.deltaTime;
            goal_rotation = Quaternion.Euler(0, 180, 0);
            SetAnimation("walk", true);
        }
        else
        {
            SetAnimation("debugfade", true);
        }

        objPos.x += x;
        new_position = objPos;

        return goal_rotation;
    }

    Quaternion ApplyKeyLeftRight( out Vector3 new_position)
    {
        Quaternion goal_rotation = _goalRotation;
        var objPos = _graphics.transform.position;

        float x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        objPos.x += x;
        new_position = objPos;

        if (x > 0)
        {
            goal_rotation = Quaternion.Euler(0, 0, 0);
            SetAnimation("walk", true);
        }
        else if (x < 0)
        {
            goal_rotation = Quaternion.Euler(0, 180, 0);
            SetAnimation("walk", true);
        }
        else
        {
            SetAnimation("debugfade", true);
        }

        return goal_rotation;
    }

    bool IsGrounded()
    {
        Vector2 position = _graphics.transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, _groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    #endregion

    #region CalledByEvents
    public float _jump_force__vert = 1000;
    public float _jump_force__hor = 750;
    public void DoSwipeOnPlayer()
    {
        Vector2 swiped_vector = _touch_or_mouse.Swiped.Distance;

        if(_boGrounded)
        {
            float hor_jump = swiped_vector.x;
            if (Math.Abs(hor_jump) < 10) hor_jump = 0;

            Quaternion goal_rotation = _goalRotation;
            if (hor_jump > 0)
            {
                _rigidBody2D.AddForce(new Vector2(_jump_force__hor, _jump_force__vert), ForceMode2D.Impulse);
                goal_rotation = Quaternion.Euler(0, 0, 0);
            }
            else if(hor_jump < 0)
            {
                _rigidBody2D.AddForce(new Vector2(-1.0f *_jump_force__hor, _jump_force__vert), ForceMode2D.Impulse);
                goal_rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                _rigidBody2D.AddForce(new Vector2(0, _jump_force__vert), ForceMode2D.Impulse);
            }
            _goalRotation = goal_rotation;
        }
    }
    #endregion
}
