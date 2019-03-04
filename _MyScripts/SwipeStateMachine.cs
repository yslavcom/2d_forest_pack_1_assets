using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeStateMachine : MonoBehaviour {

    enum State{
        enState__Idle,
        enState__Tapped,
        enState__Released,
    }

    private TouchOrMouseInput _touch;
    private State _state = State.enState__Idle;
    private Vector2 _start_coord;
    private Vector2 _end_coord;

    private void Update()
    {
        TouchOrMouseInput.StTap tap = _touch.Tap;

        switch(_state)
        {
            case State.enState__Idle:
                if(tap.Tap)
                {
                    _start_coord = tap.Coord;
                    _state = State.enState__Tapped;
                }
                break;

            case State.enState__Tapped:
                break;

            case State.enState__Released:
                break;

            default:
                _state = State.enState__Idle;
                break;
        }
    }

}
