using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyllotaxisTrails : Phyllotaxis {

    public Gradient _gradient;
    [Range(0,3)]
    public float _trailTime;
    private List<GameObject> _trails = new List<GameObject>();
    private Dictionary<GameObject, int> _trailObjs = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, Vector3> _trailTargetPos = new Dictionary<GameObject, Vector3>();
    // Use this for initialization
    void Awake()
    {
        _degreeDiff = GetDegreeDiff(_numOfObjects);
        var n = _startNumber;

        for (int i = 0; i < _numOfObjects; i++)
        {
            float step = 1.0f / _numOfObjects;
            Color color = _gradient.Evaluate(step * i);
            Material newMaterial = new Material(Shader.Find("Transparent/Diffuse"));
            newMaterial.color = color;

            GameObject obj = new GameObject();
            TrailRenderer trail = obj.AddComponent<TrailRenderer>();

            _trailMat = new Material(trail.material);
            _trailMat.color = color;
            _trailRenderer.material = _trailMat;

            obj.transform.localPosition = CalculatePhylllotaxis(_degree, _scale, _number);

            _trailObjs.Add(obj, n);
            n = n + _degreeDiff;


            if (_useLerping)
            {
                _isLerping = true;
                SetNewTargetPoses();
            }
        }
    }

    //Non inherited methods
    /*
    protected override void UpdateObjects()
    {
        foreach (KeyValuePair<GameObject, Vector3> entry in _trailTargetPos)
        {
            entry.Key.transform.position
            entry.Key.LerpToTarget();
        }
    }
    */
}
