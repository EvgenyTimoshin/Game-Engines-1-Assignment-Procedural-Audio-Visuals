using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomObjectPhyllo : Phyllotaxis{
    public GameObject _customPrefab;
    [Range(0,10)]
    public float _customObjectSize = 1;

	// Use this for initialization
	void Start () {
        _degreeDiff = GetDegreeDiff(_numOfObjects);
        var n = _startNumber;
        for (int i = 0; i < _numOfObjects; i++)
        {
            GameObject newPrefab = Instantiate(_customPrefab);
            IPhylloEffected newObj = CustomObject.Create(newPrefab);
            _objects.Add(newObj, n);
            n = n + _degreeDiff;

            if (_useLerping)
            {
                _isLerping = true;
                SetNewTargetPoses();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void UpdateObjects()
    {
        var objects = new List<IPhylloEffected>(_objects.Keys);
        foreach (CustomObject o in objects)
        {
            o.ChangeScale(_customObjectSize);
        }
            
    }
}
