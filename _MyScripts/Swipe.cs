using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

#region TapStructure 
    public struct StTap
    {
        private bool _tap;
        public bool Tap
        {
            get { return _tap; }
            set { _tap = value; }
        }

        private Vector2 _coord;
        public Vector2 Coord
        {
            get { return _coord; }
            set { _coord = value; }
        }

        public StTap(bool tap, Vector2 coord)
        {
            _tap = tap;
            _coord = coord;
        }
    }
#endregion

    [SerializeField]
    private StTap tap = new StTap(false, Vector2.zero);

    private bool boSwiped;
    private Vector2 vec2StartTouchTapSwipe;

    private void Start()
    {
    }

    private void Update()
    {
#region Standalone Inputs
        if(Input.GetMouseButtonDown(0))
        {
            if (!tap.Tap) vec2StartTouchTapSwipe = (Vector2)Input.mousePosition;

            tap.Tap = true;
            tap.Coord = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (tap.Tap) boSwiped = true;

            tap.Tap = false;
            tap.Coord = Vector2.zero;
        }
#endregion

#region Mobile Inputs
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                if (!tap.Tap) vec2StartTouchTapSwipe = Input.touches[0].position;

                tap.Tap = true;
                tap.Coord = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended 
                || Input.touches[0].phase == TouchPhase.Canceled)
            {
                if (tap.Tap) boSwiped = true;

                tap.Tap = false;
                tap.Coord = Vector2.zero;
            }
        }
#endregion

    }


    public StTap Tap { get { return tap; } }
}
