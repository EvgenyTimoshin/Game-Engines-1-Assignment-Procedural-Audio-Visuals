using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour {

    public GameObject _prefab;
    protected Vector3 _currentPos;
    protected Vector3 _targetPos;
    protected float _posLerpSpeed;
    protected int _segments;
    protected float _size;

    public static Creature Create(Vector3 startPos,int segments,float size) {
        Creature creature = new GameObject().AddComponent<Creature>();
        creature._size = size;
        creature._segments = segments;
        creature.transform.position = startPos;

        return creature;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_targetPos != null) {
            LerpToTarget();
        }
	}


    private void LerpToTarget() {
        transform.localPosition = Vector3.Lerp(transform.position, _targetPos, _posLerpSpeed);
        //Vector3 dir = (transform.position - _targetPos).normalized;
    }

    public void SetTargetPosition(Vector2 newEndPos){
        _targetPos = new Vector3(newEndPos.x, newEndPos.y, transform.position.z);
    }

    public void SetPrefab(GameObject prefab) {
        _prefab = prefab;
    }
}
