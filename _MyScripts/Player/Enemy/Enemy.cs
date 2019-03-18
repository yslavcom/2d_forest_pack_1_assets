using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed ;
    [SerializeField]
    protected Transform pointA, pointB;

    public virtual void Atack()
    {
        Debug.Log("May name is = " + this.gameObject.name);
    }

    public abstract void Update();
}
