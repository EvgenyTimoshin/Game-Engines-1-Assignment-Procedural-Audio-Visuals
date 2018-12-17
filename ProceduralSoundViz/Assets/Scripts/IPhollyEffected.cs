using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhollyEffected {

    void LerpToTarget();
    void SetTargetPosition(Vector3 newEndPos);
    void SetLerpSpeed(float speed);
}
