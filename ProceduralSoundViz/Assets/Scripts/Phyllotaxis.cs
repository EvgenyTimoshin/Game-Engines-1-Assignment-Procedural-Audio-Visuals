using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class for the phyllotaxis algorithm
/// </summary>
public class Phyllotaxis : MonoBehaviour {

    public float _scale;
    public float _degree;
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
    public bool _changeDegrees = false;
    [Range(1,200)]
    public int _changeDegreeInterval = 10;

    protected Dictionary<IPhylloEffected, int> _objects = new Dictionary<IPhylloEffected, int>();


    private void Awake()
    {
        //transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        //_trailMat.SetColor("_TintColor", _trailColor);
        //_trailMat.color = _trailColor;
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

    /// <summary>
    /// Runs every phycis frame and takes care of updating all objects and calculating the phyllotaxis positions
    /// </summary>
    private void FixedUpdate()
    {
        CheckDegreeChange();
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

    public void CheckDegreeChange() {
        if (_changeDegrees)
        {
            if (_currentIteration % _changeDegreeInterval == 0)
            {
                _stepSize = _stepSize * -1;
            }
        }
    }

    /// <summary>
    /// Sets the Lerp positions of the phyllotaxis algorithm
    /// </summary>
    protected void SetLerpPositions() {
        _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _number);
        _startPos = transform.localPosition;
        _endPos = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
    }
    
    /// <summary>
    /// Calculates the new phyllotaxis postion (Core algorithm)
    /// </summary>
    /// <param name="degree">the degree</param>
    /// <param name="scale">the scale (Radius) </param>
    /// <param name="count">which iteration of the algorithm</param>
    /// <returns></returns>
    protected Vector2 CalculatePhylllotaxis(float degree, float scale, int count)
    {
        double angle = count * (degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(count);
        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);
        return new Vector2(x, y);
    }

    /// <summary>
    /// Scales the polytaxis size (Itterations)
    /// </summary>
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

    /// <summary>
    /// Returns the degree difference based on amount of objects that will be used (points)
    /// </summary>
    /// <param name="num">amount of objects that will be used</param>
    /// <returns></returns>
    protected int GetDegreeDiff(int num) {
        var degreeDiff = (int)(360 / _degree / num);
        return degreeDiff;
    }

    /// <summary>
    /// Abstract or Virtual method that must be implemented and overriden to update the respective objects of child class
    /// </summary>
    protected virtual void UpdateObjects() { }

    /// <summary>
    /// Sets the new target positions for whatever classes are exetending this class
    /// </summary>
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

    /// <summary>
    /// Lerpts the objects to the their new targets
    /// </summary>
    public void LerpObjsToTarget()
    {
        foreach (KeyValuePair<IPhylloEffected, int> entry in _objects)
        {
            entry.Key.SetLerpSpeed(Mathf.Clamp01(_lerpPosTimer));
            entry.Key.LerpToTarget();
        }
    }

    /// <summary>
    /// Updates the lerp timer based on the audio band
    /// </summary>
    public void UpdateLerpTimer()
    {
        _lerpPosSpeed = Mathf.Lerp(_lerpPosSpeedMinMax.x, _lerpPosSpeedMinMax.y, _lerpPosAnimCurve.Evaluate(AudioAnalyzer.bands[_lerpPosBand]));
        _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;
    }
}
