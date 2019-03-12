using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinControl : MonoBehaviour {

    public float _target_detection_radius = 5;
    public Transform _graphics;

    public string _target_collider_name = "";
    public OverlapDetection _overlap_detection;


    [SerializeField]
    private bool _boDetected;


    // Use this for initialization
    void Start () {
        _boDetected = false;
    }
	
	// Update is called once per frame
	void Update () {
        var centre = _graphics.transform.position;

        Collider2D[] detected_colliders;
        _overlap_detection.DetectWithinSphere((Vector2)centre, _target_detection_radius, out detected_colliders);
        if(null != detected_colliders)
        {
            foreach (var collider in detected_colliders)
            {
                if(collider.name == _target_collider_name)
                {
                    _boDetected = true;
                }
            }
        }
    }
}
