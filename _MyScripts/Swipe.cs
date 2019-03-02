using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    public int Boundary = 125;

    private bool boTap, boSwiped;

    [SerializeField]
    private Vector2 vec2StartTouchTap;
    private Vector2 vec2StartTouchTapSwipe;

    private void Start()
    {
        boTap = boSwiped = false;
        vec2StartTouchTap = vec2StartTouchTapSwipe = Vector2.zero;
}


    private void Update()
    {
        #region Standalone Inputs
        if(Input.GetMouseButtonDown(0))
        {
            if (!boTap) vec2StartTouchTapSwipe = (Vector2)Input.mousePosition;

            boTap = true;
            vec2StartTouchTap = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (boTap) boSwiped = true;

             boTap = false;
            vec2StartTouchTap = Vector2.zero;
        }
        #endregion

        #region Mobile Inputs
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                if (!boTap) vec2StartTouchTapSwipe = Input.touches[0].position;

                boTap = true;
                vec2StartTouchTap = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended 
                || Input.touches[0].phase == TouchPhase.Canceled)
            {
                if (boTap) boSwiped = true;

                boTap = false;
                vec2StartTouchTap = Vector2.zero;
            }
        }
        #endregion

    }


    public bool Tap { get { return boTap; } }
    public Vector2 TapCoordinates { get { return vec2StartTouchTap; } }
}
