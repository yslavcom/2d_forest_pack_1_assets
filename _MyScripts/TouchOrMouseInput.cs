using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchOrMouseInput : MonoBehaviour
{
    public string _swipe_event_name = "swiped";

    #region TapStructure 
    public class StTap
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

    #region SwipeStructure
    [System.Serializable]
    public class StSwipe 
    {
        [SerializeField]
        private Vector2 _distance;
        public Vector2 Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }
        public StSwipe(Vector2 start, Vector2 end)
        {
            _distance = end-start;
        }

        public bool BoZero()
        {
            return _distance == Vector2.zero;
        }
    }
    #endregion

    private StTap _tap;

    [SerializeField]
    private StSwipe _swipe;

    private void Start()
    {
        _tap = new StTap(false, Vector2.zero);
        _swipe = new StSwipe(Vector2.zero, Vector2.zero);
    }

    private void Update()
    {
#region Mouse Inputs
        if (Input.GetMouseButtonDown(0))
        {
            _tap.Tap = true;
            _tap.Coord = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _swipe = new StSwipe(_tap.Coord, Input.mousePosition);
            if (!_swipe.BoZero())
            {
                EventManager.TriggerEvent(_swipe_event_name);
            }

            _tap.Tap = false;
            _tap.Coord = Vector2.zero;
        }
#endregion

#region Touchscreen Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _tap.Tap = true;
                _tap.Coord = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended
                || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _swipe = new StSwipe(_tap.Coord, Input.touches[0].position);
                if (!_swipe.BoZero())
                {
                    EventManager.TriggerEvent(_swipe_event_name);
                }

                _tap.Tap = false;
                _tap.Coord = Vector2.zero;
            }
        }
#endregion

    }

#region TapsAndSwipesGetters
    /*
     * Return taps & swipes
     */
    public StTap Tap { get { return _tap; } }
    public StSwipe Swiped { get { return _swipe; } }
#endregion
}


