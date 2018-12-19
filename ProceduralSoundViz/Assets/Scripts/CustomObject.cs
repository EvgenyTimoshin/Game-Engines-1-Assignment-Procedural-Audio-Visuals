using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomObject : MonoBehaviour,IPhylloEffected {

    protected Vector3 _currentPos;
    protected Vector3 _targetPos;
    protected float _posLerpSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LerpToTarget()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, _posLerpSpeed);
    }

    public void SetLerpSpeed(float speed)
    {
        throw new System.NotImplementedException();
    }

    public void SetTargetPosition(Vector3 newEndPos)
    {
        throw new System.NotImplementedException();
    }
}
