using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class Player : MonoBehaviour {

    public float speed = 5;
    public int rotation_speed = 5;

    public Transform graphics;

    public SkeletonAnimation skeletonAnimation;

    public Swipe SwipeControl;

    private float x;
    private Rigidbody2D rigidBody2D;
    private string currentAnimation = "";
    private Quaternion goalRotation = Quaternion.identity;

    private Camera cam;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        cam = Camera.main;

    }

    [SerializeField]
    private float mouse_tap_x;

    [SerializeField]
    private Vector2 object_in_pixels;

    void Update()
    {

        if (SwipeControl.Tap)
        {
            //this should convert from pixels to world coordinates
            mouse_tap_x = SwipeControl.TapCoordinates.x;
            var objPos = graphics.transform.position;
            object_in_pixels = (Vector2)cam.WorldToScreenPoint(objPos);

            if (mouse_tap_x > object_in_pixels.x)
            {
                x = speed;
                goalRotation = Quaternion.Euler(0, 0, 0);
                SetAnimation("walk", true);
            }
            else if (mouse_tap_x < object_in_pixels.x)
            {
                x = -speed;
                goalRotation = Quaternion.Euler(0, 180, 0);
                SetAnimation("walk", true);
            }
            else
            {
                SetAnimation("debugfade", true);
            }
        }
        else
        {
            x = Input.GetAxis("Horizontal") * speed;
            if (x > 0)
            {
                Debug.Log("x_key-right = " + x);
                goalRotation = Quaternion.Euler(0, 0, 0);
                SetAnimation("walk", true);
            }
            else if (x < 0)
            {
                Debug.Log("x_key-left = " + x);
                goalRotation = Quaternion.Euler(0, 180, 0);
                SetAnimation("walk", true);
            }
            else
            {
                SetAnimation("debugfade", true);
            }
        }

        graphics.localRotation = Quaternion.Lerp(graphics.localRotation, goalRotation, rotation_speed * Time.deltaTime);
    }

    void SetAnimation( string name, bool loop)
    {
        if (name == currentAnimation) return;

        skeletonAnimation.state.SetAnimation(0, name, loop);
        currentAnimation = name;
    }

    void FixedUpdate () {
        rigidBody2D.velocity = new Vector2(x, rigidBody2D.velocity.y);
	}

}
