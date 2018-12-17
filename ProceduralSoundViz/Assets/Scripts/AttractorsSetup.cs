using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorsSetup : MonoBehaviour {

    public int[] _attractorBands;
    [Range(0,10)]
    public float _attractorSpacing;
    [Range(0, 60)]
    public int _objectsPerAttractor;
    [Range(0, 10)]
    public float _attractorsScale;
    public Vector3 _directionOfLayout;
    private List<Attractor> _attractors = new List<Attractor>();
    
	// Use this for initialization
	void Start () {
        for (int i = 0; i < _attractorBands.Length; i++) {
            Attractor at = Attractor.Create(transform.position * _attractorSpacing, _attractorsScale, _objectsPerAttractor, _attractorBands[i]);
            _attractors.Add(at);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
