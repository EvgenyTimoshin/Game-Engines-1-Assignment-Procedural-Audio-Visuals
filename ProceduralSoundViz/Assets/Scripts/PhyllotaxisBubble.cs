using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyllotaxisBubble : Phyllotaxis
{
    [Range(1, 7)]
    public int _numOfObjects;

    private TrailRenderer _trailRenderer;
    private Vector2 _phyllotaxisPosition;
    private bool _isLerping;
    private Vector3 _startPos, _endPos;
    private float _lerpPosTimer, _lerpPosSpeed;
    private int _currentIteration;
    private int _number;
    private Material _trailMat;
    private int _degreeDiff;

    public bool _setUpBubbles = false;
    public bool _setUpTrails = false;

    //Phyllotaxis stuff
    public int[] _attractorBands;
    public float[] _attractorAudioTreshHolds;
    [Range(0, 10)]
    public float _attractorSpacing;
    [Range(0, 60)]
    public int _objectsPerAttractor;
    [Range(0, 10)]
    public float _attractorsScale;
    public Vector3 _directionOfLayout;
    public Gradient _gradient;
    public bool _lerpyScale = false;
    public bool _scalePhyllotaxisSize = true;

    public Vector2 _minMaxScale;

    [Range(0, 1)]
    public float _audioTreshhold;

    private Dictionary<MovingAttractor,int> _objects = new Dictionary<MovingAttractor,int>();

    private void Awake()
    {
        _degreeDiff = (int)(360 / _degree / _numOfObjects);
        Debug.Log("DEGREE DIFF" + _degreeDiff);
        var n = _startNumber;

        for (int i = 0; i < _attractorBands.Length; i++)
        {
            float step = 1.0f / _attractorBands.Length;
            Color color = _gradient.Evaluate(step * i);
            Material newMaterial = new Material(Shader.Find("Transparent/Diffuse"));
            newMaterial.color = color;
            MovingAttractor a = Attractor.Create<MovingAttractor> (_attractorsScale, _objectsPerAttractor, _attractorBands[i], _audioTreshhold, newMaterial);
            a.transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);
            a.transform.parent = transform;
            _objects.Add(a, n);
            n = n + _degreeDiff;
        }

        if (_useLerping)
        {
            _isLerping = true;
            ///ABSTRACT
            var attractors = new List<MovingAttractor>(_objects.Keys);
            foreach (MovingAttractor attractor in attractors)
            {
                //Debug.Log("blah blah");
                _objects[attractor] += _stepSize;
                _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _objects[attractor]);
                attractor.SetTargetPosition(new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0));
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //Dont run start from inherited class
    }

    private void UpdateAttractors()
    {
        var attractors = new List<MovingAttractor>(_objects.Keys);
        foreach (MovingAttractor attractor in attractors)
        {
            //Debug.Log("OTherSHit");
            attractor.UpdateTreshhold(_audioTreshhold);
            attractor.SetLerpMode(_lerpyScale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAttractors();
        if (_useLerping)
        {
            if (_isLerping)
            {
                _lerpPosSpeed = Mathf.Lerp(_lerpPosSpeedMinMax.x, _lerpPosSpeedMinMax.y, _lerpPosAnimCurve.Evaluate(AudioAnalyzer.bands[_lerpPosBand]));
                _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;

                if (_scalePhyllotaxisSize)
                {
                    //_scale = Mathf.Lerp(_scale, _scale * 2, AudioAnalyzer.bands[_lerpPosBand] * Time.deltaTime);
                    _scale = Mathf.Lerp(_minMaxScale.x, 1 + (AudioAnalyzer.bands[_lerpPosBand]) * _minMaxScale.y, Time.deltaTime * 10f);
                }
                else {
                    _scale = _minMaxScale.x;
                }

                foreach (KeyValuePair<MovingAttractor, int> entry in _objects) {
                    entry.Key.SetLerpSpeed(Mathf.Clamp01(_lerpPosTimer));
                    entry.Key.LerpToTarget();
                }
                //transform.localPosition = Vector3.Lerp(_startPos, _endPos, Mathf.Clamp01(_lerpPosTimer));

                if (_lerpPosTimer >= 1)
                {
                    _lerpPosTimer -= 1;

                    //_number += _stepSize;
                    var attractors = new List<MovingAttractor>(_objects.Keys);
                    foreach (MovingAttractor attractor in attractors)
                    {
                        //Debug.Log("blah blah");
                        _objects[attractor] += _stepSize;
                        _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _objects[attractor]);
                        attractor.SetTargetPosition(new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0));
                    }

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

    private Vector2 CalculatePhylllotaxis(float degree, float scale, int count)
    {
        double angle = count * (degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(count);
        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);
        return new Vector2(x, y);
    }
}



