﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorsSetup : MonoBehaviour {

    public int[] _attractorBands;
    public float[] _attractorAudioTreshHolds;
    [Range(0,10)]
    public float _attractorSpacing;
    [Range(0, 60)]
    public int _objectsPerAttractor;
    [Range(0, 10)]
    public float _attractorsScale;
    public Vector3 _directionOfLayout;
    private List<Attractor> _attractors = new List<Attractor>();
    public Gradient _gradient;
    public bool _lerpyScale = true;
    public bool _emissionLerping = false;
    [Range(1, 10)]
    public float _emissionLerpStrenght = 7;

    [Range(0,1)]
    public float _audioTreshhold;

    ///PhylloTaxis Stuff
    public bool _phylotaxisOn;

    
	// Use this for initialization
    /// <summary>
    /// Sets up this class, kind of like a constructor
    /// Creates the amount of attractors to spawn and specifies other paramters for the attractor to use
    /// </summary>
	public virtual void Start () {
        for (int i = 0; i < _attractorBands.Length; i++) {

            float step = 1.0f / _attractorBands.Length;
            Color color = _gradient.Evaluate(step * i);
            Material newMaterial = new Material(Shader.Find("Standard"));
            newMaterial.color = color;
            Attractor at = Attractor.Create<Attractor>(_attractorsScale, _objectsPerAttractor, _attractorBands[i],_audioTreshhold,newMaterial);
            at.transform.position = new Vector3(transform.position.x + (_attractorSpacing * i * _directionOfLayout.x),
               transform.position.y + (_attractorSpacing * i * _directionOfLayout.y),
               transform.position.z + (_attractorSpacing * i * _directionOfLayout.z));
            at.transform.position = new Vector3(at.transform.position.x, at.transform.position.y, at.transform.position.z + 5);
            at.transform.position = new Vector3(at.transform.position.x, at.transform.position.y, at.transform.position.z - 5);
            at.transform.parent = transform;
            _attractors.Add(at);
        }
	}
	
    /*
	// Update is called once per frame
	void Update () {
        UpdateAttractors();
	}
    */

    private void FixedUpdate()
    {
        UpdateAttractors();
    }

    /// <summary>
    /// Updates the child Attractor elements
    /// </summary>
    private void UpdateAttractors() {
        foreach (Attractor a in _attractors) {
            a.UpdateTreshhold(_audioTreshhold);
            a.UpdateChildrenBubbles(_emissionLerpStrenght, _emissionLerping, _lerpyScale);
        }
    }
}
