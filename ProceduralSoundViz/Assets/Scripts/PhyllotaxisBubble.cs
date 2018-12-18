using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyllotaxisBubble : Phyllotaxis
{
    //private Dictionary<IPhylloEffected, int> _objects = new Dictionary<IPhylloEffected, int>();

    public bool _setUpTrails = false;

    //Phyllotaxis stuff
    public int[] _attractorBands;
    public float[] _attractorAudioTreshHolds;
    [Range(0, 60)]
    public int _objectsPerAttractor;
    [Range(0, 10)]
    public float _attractorsScale;
    public Gradient _gradient;
    public bool _lerpyScale = false;

    [Range(0, 1)]
    public float _audioTreshhold;

    private void Awake()
    {
        _degreeDiff = GetDegreeDiff(_attractorBands.Length);
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
            IPhylloEffected interfaceVer = (IPhylloEffected)a;
            _objects.Add(interfaceVer, n);
            n = n + _degreeDiff;
        }

        if (_useLerping)
        {
            _isLerping = true;
            SetNewTargetPoses();
        }
    }

    //Non inherited methods

    protected override void UpdateObjects()
    {
        var attractors = new List<IPhylloEffected>(_objects.Keys);
        foreach (MovingAttractor attractor in attractors)
        {
            attractor.UpdateTreshhold(_audioTreshhold);//unique
            attractor.SetLerpMode(_lerpyScale);
        }
    }

}



