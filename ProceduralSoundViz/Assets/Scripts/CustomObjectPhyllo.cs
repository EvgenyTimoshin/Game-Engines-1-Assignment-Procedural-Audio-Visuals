using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomObjectPhyllo : Phyllotaxis{
    public GameObject _customPrefab;
    [Range(0,10)]
    public float _customObjectSize = 1;
    public Gradient _gradient;

    private void Awake()
    {
        _degreeDiff = GetDegreeDiff(_numOfObjects);
        var n = _startNumber;
        for (int i = 0; i < _numOfObjects; i++)
        {
            float step = 1.0f / _numOfObjects;
            Color color = _gradient.Evaluate(step * i);

            GameObject newPrefab = Instantiate(_customPrefab);
            Renderer rend = newPrefab.GetComponent<Renderer>();
            Material newMat = new Material(rend.material);
            newMat.color = color;
            rend.material = newMat;
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

    // Use this for initialization
    void Start () {
       
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
