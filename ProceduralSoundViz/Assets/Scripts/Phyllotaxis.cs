using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {

    public float _scale;
    public float _degree;
    public Color _trailColor;
    public int _startNumber;
    public int _maxIteration;
    public int _stepSize;
    public Vector2 _lerpPosSpeedMinMax;
    public AnimationCurve _lerpPosAnimCurve;
    public int _lerpPosBand;
    public bool _useLerping;

    private int _currentIteration;
    private bool _isLerping;
    private Vector3 _startPos, _endPos;
    private float _lerpPosTimer, _lerpPosSpeed;
    private Material _trailMat;
    private TrailRenderer _trailRenderer;
    private Vector2 _phyllotaxisPosition;
    private int _number;


    private void Awake()
    {
        
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        _trailMat.SetColor("_TintColor", _trailColor);
        _trailRenderer.material = _trailMat;
        _number = _startNumber;
        //transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        _trailMat.SetColor("_TintColor", _trailColor);
        _trailRenderer.material = _trailMat;
        _number = _startNumber;
        transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);
        if (_useLerping) {
            _isLerping = true;
            SetLerpPositions();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_useLerping)
        {
            if (_isLerping)
            {
                _lerpPosSpeed = Mathf.Lerp(_lerpPosSpeedMinMax.x, _lerpPosSpeedMinMax.y, _lerpPosAnimCurve.Evaluate(AudioAnalyzer.bands[_lerpPosBand]));
                _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;
                transform.localPosition = Vector3.Lerp(_startPos,_endPos, Mathf.Clamp01(_lerpPosTimer));

                if (_lerpPosTimer >= 1) {
                    _lerpPosTimer -= 1;
                    _number += _stepSize;
                    _currentIteration++;
                    SetLerpPositions();
                }
            }
        }
        if(!_useLerping)
        {
            _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, transform.position.z);
            _number += _stepSize;
            _currentIteration++;
        }
    }

    void SetLerpPositions() {
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
