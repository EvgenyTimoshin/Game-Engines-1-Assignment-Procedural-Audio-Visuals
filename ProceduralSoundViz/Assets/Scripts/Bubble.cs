using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
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
    protected bool _emissionLerping = false;
    private Renderer _rend;
    private float _emmisionLerpStrenght;
    // Use this for initialization

    /// <summary>
    /// Static Create method for instatiating this class as part of a game object
    /// </summary>
    /// <param name="pos"> Position where it will be intantiated</param>
    /// <param name="size"> The size of each buble</param>
    /// <param name="attractedTo"> What transform the objects will gravitate towards</param>
    /// <param name="mat"> The material that the object will  use</param>
    /// <returns></returns>
    public static Bubble Create(Vector3 pos, float size, Transform attractedTo, Material mat) {
        Bubble ab = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Bubble>();
        Rigidbody rb = ab.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        ab.gameObject.name = "ReactiveObj";
        ab._size = size;
        ab.transform.localScale = new Vector3(size,size,size);
        ab._maxSize = size * 4;
        ab._attractedTo = attractedTo;
        ab.transform.position = pos;
        var rend = ab.GetComponent<Renderer>();
        rend.material = new Material(rend.material);
        rend.material.color = ab._color;
        rend.material.EnableKeyword("_EMISSION");
        ab._soundColor = mat.color;
        rend.material.SetColor("_EmissionColor", ab._color);
        ab._minSize = ab.transform.localScale;
        return ab;
    }

    
	void Start () {
        _rb = GetComponent<Rigidbody>();
        _rend = GetComponent<Renderer>();
        _rend.material.EnableKeyword("_EMISSION");
        //GetComponent<Material>().EnableKeyword("_EMISSION");
    }

    /// <summary>
    /// Controls how the object scales based on scaler value passed in
    /// This is usually the audioband
    /// </summary>
    /// <param name="scaler"></param>
    public void Scale(float scaler) {
        //Mathf.Lerp(ls.y, 1 + (AudioAnalyzer.bands[i] * scale), Time.deltaTime * 3.0f);
        //transform.localScale = Vector3.Lerp(_minSize, _maxSize, scaler);
        if (_lerpyScale){
            float newSize = Mathf.Lerp(transform.localScale.x, 1 + (scaler * _maxSize), Time.deltaTime * 10);
            transform.localScale = new Vector3(newSize, newSize, newSize);
        }
        else {
            transform.localScale = Vector3.Lerp(_minSize, new Vector3(_maxSize, _maxSize, _maxSize), scaler*2);   
        }

        if (_emissionLerping){
            _rend.material.color = _color;
            Color newEmiitedColor = Color.Lerp(_color, _soundColor, scaler * _emmisionLerpStrenght);
            _rend.material.SetColor("_EmissionColor", newEmiitedColor);
        }
        else
        {
            if (_rend)
            {
                _rend.material.color = _soundColor;
            }
        }
    }

    /// <summary>
    /// Sets whether emission lerping is on or not
    /// </summary>
    /// <param name="set">boolean value toi set</param>
    public void SetEmissionLepring(bool set) {
        _emissionLerping = set;
    }

    /// <summary>
    /// Sets whether lerpy scale is on or not
    /// </summary>
    /// <param name="set">boolean value to set</param>
    public void SetLerpyScale(bool set) {
        _lerpyScale = set;
    }

    /// <summary>
    /// Turns on gravity of the object
    /// </summary>
    public void GravityOn() {
        _rb.useGravity = true;
    }

    /// <summary>
    /// Turns off the gravity of the object
    /// </summary>
    public void GravityOff() {
        _rb.useGravity = false;
    }

    /// <summary>
    /// Sets the strenght of the emission lerp function
    /// </summary>
    /// <param name="strenght"></param>
    public void SetEmissionLerpStrenght(float strenght) {
        _emmisionLerpStrenght = strenght;
    }

    /// <summary>
    /// Runs once every physics frame
    /// </summary>
    private void FixedUpdate()
    {
        if (_attractedTo)
        {
            Vector3 dir = _attractedTo.transform.position - transform.position;
            _rb.AddForce(_attractionStrenght * dir);

            if (_rb.velocity.magnitude > _maxPower)
            {
                _rb.velocity = _rb.velocity.normalized * _maxPower;
            }
        }
    }

    // Update is called once per frame
    /*
    void Update () {
        if (_attractedTo) {
            Vector3 dir = _attractedTo.transform.position - transform.position;
            _rb.AddForce(_attractionStrenght * dir);

            if (_rb.velocity.magnitude > _maxPower) {
                _rb.velocity = _rb.velocity.normalized * _maxPower;
            }
        }
		
	}
    */
}
