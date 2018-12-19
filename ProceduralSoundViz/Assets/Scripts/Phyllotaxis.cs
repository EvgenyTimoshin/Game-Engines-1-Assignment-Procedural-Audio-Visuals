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
    [Range(0,6)]
    public int _lerpPosBand;
    public bool _useLerping;
    public Vector2 _minMaxScale;
    public bool _scalePhyllotaxisSize = true;

    protected int _currentIteration;
    protected bool _isLerping;
    protected Vector3 _startPos, _endPos;
    protected float _lerpPosTimer, _lerpPosSpeed;
    protected Material _trailMat;
    protected TrailRenderer _trailRenderer;
    protected Vector2 _phyllotaxisPosition;
    protected int _number;
    protected int _degreeDiff;
    [Range(1, 8)]
    public int _numOfObjects;

    protected Dictionary<IPhylloEffected, int> _objects = new Dictionary<IPhylloEffected, int>();


    private void Awake()
    {
        //transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        //_trailMat.SetColor("_TintColor", _trailColor);
        _trailMat.color = _trailColor;
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

    private void FixedUpdate()
    {
        UpdateObjects();
        ScalePolytaxisSize();
        if (_useLerping)
        {
            if (_isLerping)
            {
                UpdateLerpTimer();
                LerpObjsToTarget();

                if (_lerpPosTimer >= 1)
                {
                    _lerpPosTimer -= 1;
                    SetNewTargetPoses();
                    _currentIteration++;
                }
            }
        }
        if (!_useLerping)
        {
            _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, transform.position.z);
            _number += _stepSize;
            _currentIteration++;
        }
    }
    /*
    // Update is called once per frame
    void Update()
    {
        UpdateObjects();
        ScalePolytaxisSize();
        if (_useLerping)
        {
            if (_isLerping)
            {
                UpdateLerpTimer();
                LerpObjsToTarget();

                if (_lerpPosTimer >= 1)
                {
                    _lerpPosTimer -= 1;
                    SetNewTargetPoses();
                    _currentIteration++;
                }
            }
        }
        if (!_useLerping)
        {
            _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, transform.position.z);
            _number += _stepSize;
            _currentIteration++;
        }
    }
    */


    protected void SetLerpPositions() {
        _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _number);
        _startPos = transform.localPosition;
        _endPos = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
    }
    

    protected Vector2 CalculatePhylllotaxis(float degree, float scale, int count)
    {
        double angle = count * (degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(count);
        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);
        return new Vector2(x, y);
    }

    protected void ScalePolytaxisSize()
    {
        if (_scalePhyllotaxisSize)
        {
            //_scale = Mathf.Lerp(_scale, _scale * 2, AudioAnalyzer.bands[_lerpPosBand] * Time.deltaTime);
            _scale = Mathf.Lerp(_minMaxScale.x, 1 + (AudioAnalyzer.bands[_lerpPosBand]) * _minMaxScale.y, Time.deltaTime * 10f);
        }
        else
        {
            _scale = _minMaxScale.x;
        }
    }

    protected int GetDegreeDiff(int num) {
        var degreeDiff = (int)(360 / _degree / num);
        return degreeDiff;
    }

    protected virtual void UpdateObjects() { }

    public void SetNewTargetPoses()
    {
        var objs = new List<IPhylloEffected>(_objects.Keys);
        foreach (IPhylloEffected obj in objs)
        {
            _objects[obj] += _stepSize;
            _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _objects[obj]);
            obj.SetTargetPosition(new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0));
        }
    }

    public void LerpObjsToTarget()
    {
        foreach (KeyValuePair<IPhylloEffected, int> entry in _objects)
        {
            entry.Key.SetLerpSpeed(Mathf.Clamp01(_lerpPosTimer));
            entry.Key.LerpToTarget();
        }
    }

    public void UpdateLerpTimer()
    {
        _lerpPosSpeed = Mathf.Lerp(_lerpPosSpeedMinMax.x, _lerpPosSpeedMinMax.y, _lerpPosAnimCurve.Evaluate(AudioAnalyzer.bands[_lerpPosBand]));
        _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;
    }
}
