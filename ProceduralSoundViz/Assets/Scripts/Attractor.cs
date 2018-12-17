using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {


    protected float _size;
    protected int _objectsCount;
    protected int _audioBand;
    protected float _currentPosition;
    protected float _nextPosition;
    protected float _bandTreshhold = 0.5f;
    protected GameObject _objectPrefab;
    protected List<AffectedByGravity> _soundObjects = new List<AffectedByGravity>();

    public static Attractor Create(float size, int objectCount, int audioBand, float treshHold) {
        Attractor attractor = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Attractor>();
        attractor.gameObject.name = "Attractor";
        attractor._size = size;
        //attractor.transform.position = pos;
        attractor._objectsCount = objectCount;
        attractor._audioBand = audioBand;
        attractor._bandTreshhold = treshHold;
        return attractor;

    }
	// Use this for initialization
	void Start () {
        CreateSoundReactiveObjects();
	}
	
	// Update is called once per frame
	void Update () {
        CheckBandTreshHold();
	}

    private void BounceObjects() {
    }

    private void CheckBandTreshHold() {
        if (AudioAnalyzer.bands[_audioBand] > _bandTreshhold) {
            BounceObjects();
        }
    }

    private void CreateSoundReactiveObjects() {
        for (int i = 0; i < _objectsCount; i++) {
            Vector3 randomDirection = new Vector3(Random.Range(1, 3), Random.Range(1, 3), Random.Range(1, 3));
            //EXPOSE SIZE IN EDITOR FOR MODIFICATION
            Vector3 randomOffsetPoint = transform.position + randomDirection;
            AffectedByGravity ab = AffectedByGravity.Create(randomOffsetPoint, 0.3f, transform);
            _soundObjects.Add(ab); 
        }
    }
}
