using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyllotaxisTrails : Phyllotaxis {

    public Gradient _gradient;
    [Range(0,10)]
    public float _trailTime;
    private List<GameObject> _trails = new List<GameObject>();
    //private Dictionary<GameObject, int> _trailObjs = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, Vector3> _trailTargetPos = new Dictionary<GameObject, Vector3>();
    public AnimationCurve _trailsSizes;
    // Use this for initialization
    void Awake()
    {
        if (_trailsSizes.keys.Length <= 0)
        {
            Keyframe[] animKeys = new Keyframe[2];
            animKeys[0].time = 0;
            animKeys[0].value = 1;
            animKeys[1].time = 1;
            animKeys[1].value = 1;

            _trailsSizes.keys = animKeys;
        }
        _degreeDiff = GetDegreeDiff(_numOfObjects);
        var n = _startNumber;

        for (int i = 0; i < _numOfObjects; i++)
        {
            float step = 1.0f / _numOfObjects;
            Color color = _gradient.Evaluate(step * i);
            PhylloTrail pT = PhylloTrail.Create(color);

            pT.transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);
            pT.transform.parent = transform;

            IPhylloEffected iPE = pT;
            _objects.Add(iPE, n);
            n = n + _degreeDiff;

            if (_useLerping)
            {
                _isLerping = true;
                SetNewTargetPoses();
            }
        }
    }

    //Inherited Update takes care of all functionality related to the Phyllotaxis

    //Non inherited methods

    /// <summary>
    /// Updates the child objects
    /// </summary>
    protected override void UpdateObjects()
    {
        var trails = new List<IPhylloEffected>(_objects.Keys);
        foreach (PhylloTrail t in trails)
        {
            t.SetTrailLifeTime(_trailTime);
            t.SetTrailSizes(_trailsSizes);
        }
    }
}
