using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAttractor : Attractor, IPhylloEffected {

    protected Vector3 _currentPos;
    protected Vector3 _targetPos;
    protected float _posLerpSpeed;

   

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
