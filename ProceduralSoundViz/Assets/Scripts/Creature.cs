using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public GameObject _driver;
    //public GameObject _shape;
    public List<GameObject> _segments = new List<GameObject>();
    protected Vector3 _currentPos;
    protected Vector3 _targetPos;
    protected float _posLerpSpeed;
    protected int _segmentNumber;
    protected float _size;

    /*
    public static Creature Create(Vector3 startPos,int segments,float size) {
        Creature creature = new GameObject().AddComponent<Creature>();
        creature._size = size;
        creature._segmentNumber = segments;
        creature.transform.position = startPos;

        return creature;
    }
    */

    public static T Create<T>(Vector3 startPos, int segments, float size) where T : Creature {
        Creature creature = new GameObject().AddComponent<T>();
        creature._size = size;
        creature._segmentNumber = segments;
        creature.transform.position = startPos;
        return creature.GetComponent<T>();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_targetPos != null) {
            //LerpToTarget();
        }
	}


    public void LerpToTarget() {
        Debug.Log("Lerping");
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, _posLerpSpeed);
        //Vector3 dir = (transform.position - _targetPos).normalized;
    }

    public void SetTargetPosition(Vector3 newEndPos){
        _targetPos = newEndPos;
    }

    public void SetLerpSpeed(float speed) {
        _posLerpSpeed = speed;
    }

    protected virtual void SetupCreature() {

    }
}
