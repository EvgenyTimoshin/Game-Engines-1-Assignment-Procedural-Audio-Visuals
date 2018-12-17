using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour
{
    private Material _trailMat;
    public Color _trailColor;
    public GameObject _creature;
    public int _startNumber;
    private int _number;
    public int _maxIteration;
    public int _stepSize;
    private int _currentIteration;

    //Lerping
    public bool _useLerping;
    private bool _isLerping;
    private Vector3 _startPos, _endPos;
    private float _lerpPosTimer, _lerpPosSpeed;
    public Vector2 _lerpPosSpeedMinMax;
    public AnimationCurve _lerpPosAnimCurve;
    public int _lerpPosBand;

    public float _scale;
    public float _degree;

    private TrailRenderer _trailRenderer;
    private Vector2 _phyllotaxisPosition;

    public int _numOfObjects;
    private int _degreeDiff;

    private Dictionary<PhylloAttractor,int> _objects = new Dictionary<PhylloAttractor,int>();

    private void Awake()
    {
        /*
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        _trailMat.SetColor("_TintColor", _trailColor);
        _trailRenderer.material = _trailMat;
        _number = _startNumber;
        */
        //transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);

        _degreeDiff = (int)(360 / _degree / _numOfObjects);
        Debug.Log("DEGREE DIFF" + _degreeDiff);
        var n = _startNumber;
        //TODO CALCULATE BASED ON SIZE OF SCALE AND ALSO NUMBER OF OBJECTS
        var segmentNumber = 3;

        for (int i = 0; i < _numOfObjects; i++)
        {
            Attractor a = Attractor.Create(CalculatePhylllotaxis(_degree, _scale, n), segmentNumber, 5);
            _objects.Add(worm, n);
            n = n + _degreeDiff;
        }

        if (_useLerping)
        {
            _isLerping = true;

            ///ABSTRACT
            var creatures = new List<Creature>(_objects.Keys);
            foreach (Creature creature in creatures)
            {
                Debug.Log("blah blah");
                _objects[creature] += _stepSize;
                _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _objects[creature]);
                creature.SetTargetPosition(new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0));
            }
            //SetLerpPosition();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_useLerping)
        {
            if (_isLerping)
            {
                _lerpPosSpeed = Mathf.Lerp(_lerpPosSpeedMinMax.x, _lerpPosSpeedMinMax.y, _lerpPosAnimCurve.Evaluate(AudioAnalyzer.bands[_lerpPosBand]));
                _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;

                foreach (KeyValuePair<Creature, int> entry in _objects) {
                    entry.Key.SetLerpSpeed(Mathf.Clamp01(_lerpPosTimer));
                    entry.Key.LerpToTarget();
                }
                //transform.localPosition = Vector3.Lerp(_startPos, _endPos, Mathf.Clamp01(_lerpPosTimer));

                if (_lerpPosTimer >= 1)
                {
                    _lerpPosTimer -= 1;

                    //_number += _stepSize;
                    var creatures = new List<Creature>(_objects.Keys);
                    foreach (Creature creature in creatures)
                    {
                        _objects[creature] += _stepSize;
                        _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _objects[creature]);
                        creature.SetTargetPosition(new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0));
                    }

                    _currentIteration++;

                    //SetLerpPositions();
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

    void SetLerpPosition()
    { 
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



