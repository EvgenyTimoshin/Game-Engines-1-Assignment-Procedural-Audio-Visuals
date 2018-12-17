using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedByGravity : MonoBehaviour {
    Rigidbody _rb;
    protected Transform _attractedTo;
    ///Make this modifiable in the editor
    protected float _attractionStrenght = 100;
    protected float _maxPower = 50;
    protected float _size;
    protected Color _color = new Color(0, 0, 0);
    protected Color _soundColor;
    protected float _maxSize;
    protected Vector3 _minSize;
    protected bool _lerpyScale = true;
    // Use this for initialization

    public static AffectedByGravity Create(Vector3 pos, float size, Transform attractedTo, Material mat) {
        AffectedByGravity ab = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<AffectedByGravity>();
        Rigidbody rb = ab.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        ab.gameObject.name = "ReactiveObj";
        ab._size = size;
        ab.transform.localScale = new Vector3(size,size,size);
        ab._maxSize = size * 4;
        ab._attractedTo = attractedTo;
        ab.transform.position = pos;
        ab.GetComponent<Renderer>().material = mat;
        ab._soundColor = mat.color;
        ab._minSize = ab.transform.localScale;

        return ab;
    }

	void Start () {
        _rb = GetComponent<Rigidbody>();
	}

    public void Scale(float scaler) {
        //Mathf.Lerp(ls.y, 1 + (AudioAnalyzer.bands[i] * scale), Time.deltaTime * 3.0f);
        //transform.localScale = Vector3.Lerp(_minSize, _maxSize, scaler);
        if (_lerpyScale){
            float newSize = Mathf.Lerp(transform.localScale.x, 1 + (scaler * _maxSize), Time.deltaTime * 10);
            transform.localScale = new Vector3(newSize, newSize, newSize);
        }
        else {
            transform.localScale = Vector3.Lerp(_minSize, new Vector3(_maxSize, _maxSize, _maxSize), scaler);
        }
    }

    public void SetLerpyScale(bool set) {
        _lerpyScale = set;
    }

    public void GravityOn() {
        _rb.useGravity = true;
    }

    public void GravityOff() {
        _rb.useGravity = false;
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
