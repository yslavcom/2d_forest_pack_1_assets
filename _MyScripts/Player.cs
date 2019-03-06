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

    private Rigidbody2D _rigidBody2D;
    private string _currentAnimation = "";
    private Quaternion _goalRotation = Quaternion.identity;
    private Camera _cam;

    private PlayerCollider _playerCollider;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _cam = Camera.main;

        _playerCollider = GetComponent<PlayerCollider>();
    }

    void Update()
    {
        TouchOrMouseInput.StTap tap = _touch_or_mouse.Tap;
        if (tap.Tap)
        {
            Vector3 new_position;
            _goalRotation = ApplyTap(tap, out new_position);
            _graphics.transform.position = new_position;
        }
        else
        {
            Vector3 new_position;
            _goalRotation = ApplyKeyLeftRight(out new_position);
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

    #endregion

    #region CalledByEvents
    public int _swipe_count;
    public Vector2 _swipe_vec;
    public void DoSwipeOnPlayer()
    {
#if false
        Vector2 swiped_vector = (Vector2)_cam.ViewportToWorldPoint(_touch_or_mouse.Swiped.Distance);

        float x0 = _rigidBody2D.velocity.x;
        float y0 = _rigidBody2D.velocity.y;

        float x = swiped_vector.x;
        float y = swiped_vector.y;

        var t = Time.deltaTime;
        float v_x0 = (x - x0) / t;
        float v_y0 = (y - y0) / t - 4.9f * t;

        _rigidBody2D.AddForce(new Vector2(v_x0, v_y0) * _rigidBody2D.mass, ForceMode2D.Impulse);

#else
        Vector2 swiped_vector = _touch_or_mouse.Swiped.Distance;
        Debug.Log("swiped count = " + _swipe_count);
        _swipe_count++;

        if (_playerCollider.BoGrounded)
        {
            _rigidBody2D.AddForce(new Vector2(swiped_vector.x * _x_force_scale, swiped_vector.y * _y_force_scale), ForceMode2D.Impulse);
            _swipe_vec = new Vector2(swiped_vector.x, swiped_vector.y);
        }
#endif
    }
#endregion
}
