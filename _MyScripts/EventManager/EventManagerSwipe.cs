using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManagerSwipe : MonoBehaviour {

    public string event_name = "swiped";
    public Player _player;

    private UnityAction swipeListener;


    private void Awake()
    {
        swipeListener = new UnityAction(SwipeFoo);
    }

    private void OnEnable()
    {
        EventManager.StartListening(event_name, swipeListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening(event_name, swipeListener);
    }

    void SwipeFoo()
    {
        _player.DoSwipeOnPlayer();
    }
}
