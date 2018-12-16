using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {

   // public GameObject _creature;
    public int _startNumber;
    private int _number;
    public int _maxIteration;
    public int _stepSize;
    private int _currentIteration;

    //Lerping
    public bool _useLerping;
    public float _intervalLerp;
    private bool _isLerping;
    private Vector3 _startPos, _endPos;
    private float _timeStartedLerping;

    public float _scale;
    public float _degree;

    private TrailRenderer _trailRenderer;
    private Vector2 _phyllotaxisPosition;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _number = _startNumber;
        transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);
        if (_useLerping) {
            StartLepring();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space)) {
            //_phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _number);
           // GameObject creature = (GameObject)Instantiate(_creature);
           // creature.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, transform.position.z);
            //_number++;
            //creature.transform.parent = transform;
        }
	}

    private void FixedUpdate()
    {
        if (_useLerping){
            if (_isLerping) {
                float timeSinceStarted = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStarted / _intervalLerp;
                transform.localPosition = Vector3.Lerp(_startPos, _endPos, percentageComplete);
                if (percentageComplete >= 0.97f) {
                    transform.localPosition = _endPos;
                    _number += _stepSize;
                    _currentIteration++;
                    if (_currentIteration <= _maxIteration){
                        StartLepring();
                    }
                    else {
                        _isLerping = false;
                    }
                }
            }
        }
        else{
            _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, transform.position.z);
            _number += _stepSize;
            _currentIteration++;
        }
    }

    void StartLepring() {
        _isLerping = true;
        _timeStartedLerping = Time.time;
        _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _number);
        _startPos = transform.localPosition;
        _endPos = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
    }

    private Vector2 CalculatePhylllotaxis(float degree, float scale, int count)
    {
        double angle = count * (degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(count);
        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);
        return new Vector2(x, y);
    }
}
