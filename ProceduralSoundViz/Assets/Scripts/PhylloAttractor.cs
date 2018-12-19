using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An extension of the attractor class that can be affected by the phyllotaxis algoirthm
/// </summary>
public class MovingAttractor : Attractor, IPhylloEffected {

    protected Vector3 _currentPos;
    protected Vector3 _targetPos;
    protected float _posLerpSpeed;

    /// <summary>
    /// Lerps the current object to the new target location
    /// </summary>
    public void LerpToTarget()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, _posLerpSpeed);
    }

    /// <summary>
    /// Sets the speed of the lerp
    /// </summary>
    /// <param name="speed"></param>
    public void SetLerpSpeed(float speed)
    {
        _posLerpSpeed = speed;
    }

    /// <summary>
    /// Sets the target position of the object
    /// </summary>
    /// <param name="newEndPos"></param>
    public void SetTargetPosition(Vector3 newEndPos)
    {
        _targetPos = newEndPos;
    }
}
