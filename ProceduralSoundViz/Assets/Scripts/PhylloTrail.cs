using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylloTrail : MonoBehaviour, IPhylloEffected {

    protected Vector3 _currentPos;
    protected Vector3 _targetPos;
    protected float _posLerpSpeed;
    private TrailRenderer _trailRenderer;
    private Material _trailMat;


    public static PhylloTrail Create(Color color) {
        PhylloTrail pT = new GameObject().AddComponent<PhylloTrail>();
        pT._trailRenderer = pT.gameObject.AddComponent<TrailRenderer>();
        pT._trailMat = new Material(pT._trailRenderer.material);
        pT._trailMat.color = color;
        pT._trailRenderer.material = pT._trailMat;
        pT._trailRenderer.material.EnableKeyword("_EMISSION");
        pT._trailRenderer.material.SetColor("_EmissionColor", color);
        return pT;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTrailLifeTime(float time)
    {
        _trailRenderer.time = time;
    }

    public void LerpToTarget()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, _posLerpSpeed);
    }

    public void SetLerpSpeed(float speed)
    {
        _posLerpSpeed = speed;
    }

    public void SetTargetPosition(Vector3 newEndPos)
    {
        _targetPos = newEndPos;
    }
}
