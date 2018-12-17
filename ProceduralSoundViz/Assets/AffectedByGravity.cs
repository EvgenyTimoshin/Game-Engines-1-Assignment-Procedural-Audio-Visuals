using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedByGravity : MonoBehaviour {
    Rigidbody _rb;
    protected Transform _attractedTo;
    ///Make this modifiable in the editor
    protected float _attractionStrenght = 100;
    protected float _maxPower = 100;
    protected float _size;
    // Use this for initialization

    public static AffectedByGravity Create(Vector3 pos, float size, Transform attractedTo) {
        AffectedByGravity ab = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<AffectedByGravity>();
        ab._size = size;
        ab.transform.localScale = new Vector3(size,size,size);
        ab._attractedTo = attractedTo;

        return ab;
    }

	void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_attractedTo) {
            Vector3 dir = _attractedTo.transform.position - transform.position;
            _rb.AddForce(_attractionStrenght * dir);

            if (_rb.velocity.magnitude > _maxPower) {
                _rb.velocity = _rb.velocity.normalized * _maxPower;
            }
        }
		
	}
}
