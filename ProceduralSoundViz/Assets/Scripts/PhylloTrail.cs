using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylloTrail : MonoBehaviour, IPhylloEffected {

    protected Vector3 _currentPos;
    protected Vector3 _targetPos;
    protected float _posLerpSpeed;
    private TrailRenderer _trailRenderer;
    private Material _trailMat;

    /// <summary>
    /// Static method for creating and instance of this class
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Sets the trails life time
    /// </summary>
    /// <param name="time"></param>
    public void SetTrailLifeTime(float time)
    {
        _trailRenderer.time = time;
    }

    /// <summary>
    /// Lerps the trail to its target position
    /// </summary>
    public void LerpToTarget()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, _posLerpSpeed);
    }

    /// <summary>
    /// Sets the lerp speed of the object
    /// </summary>
    /// <param name="speed"></param>
    public void SetLerpSpeed(float speed)
    {
        _posLerpSpeed = speed;
    }

    /// <summary>
    /// Sets the new target postion that will be used for lerping position
    /// </summary>
    /// <param name="newEndPos"></param>
    public void SetTargetPosition(Vector3 newEndPos)
    {
        _targetPos = newEndPos;
    }
}
