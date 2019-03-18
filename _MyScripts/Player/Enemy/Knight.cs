using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy {

    [SerializeField]
    private Vector3 _currentTarget;

    private Animator _anim;

    private void Start()
    {
        speed = 5;

        _anim = GetComponentInChildren<Animator>();
        Debug.Log(this.gameObject.name + " animator name = " + _anim.name);
    }

    public override void Atack()
    {
        Debug.Log("Knight");
    }

    public override void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        Patrol();
    }

    private void Patrol()
    {
        if (transform.position.x == pointA.position.x)
        {
            _currentTarget = pointB.position;
        }
        else if (transform.position.x == pointB.position.x)
        {
            _currentTarget = pointA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }
}
