using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies implementation of Every object that can be effected by the Phyllotaxis algorithm
/// </summary>
public interface IPhylloEffected {

    /// <summary>
    /// Lerps the object using this to the new target postion
    /// </summary>
    void LerpToTarget();

    /// <summary>
    /// Sets the new target position for the object
    /// </summary>
    /// <param name="newEndPos">new end or target postion</param>
    void SetTargetPosition(Vector3 newEndPos);

    /// <summary>
    /// Sets the lerp speed
    /// </summary>
    /// <param name="speed">the speed of lerping</param>
    void SetLerpSpeed(float speed);
}
