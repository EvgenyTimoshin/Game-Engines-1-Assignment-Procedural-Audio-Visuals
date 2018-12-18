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
    protected Material _material;
    protected float _bandOutput;

    public static T Create<T>(float size, int objectCount, int audioBand, float treshHold, Material color) where T : Attractor {
        Attractor attractor = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<T>();
        attractor.gameObject.name = "Attractor";
        attractor._size = size;
        attractor.transform.localScale = new Vector3(size,size,size);
        //attractor.transform.position = pos;
        attractor._objectsCount = objectCount;
        attractor._audioBand = audioBand;
        attractor._bandTreshhold = treshHold;
        Renderer rend = attractor.gameObject.GetComponent<Renderer>();
        //mat = color;
        rend.material = color;
        attractor._material = color;
        return attractor.GetComponent<T>();
    }

	// Use this for initialization
	void Start () {
        CreateSoundReactiveObjects();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Running");
        CheckBandTreshHold();
	}

    public void UpdateTreshhold(float newTreshhold) {
        _bandTreshhold = newTreshhold;
    }

    public void SetLerpMode(bool set) {
        foreach (AffectedByGravity a in _soundObjects) {
            a.SetLerpyScale(set);
        }
    }

    private void BounceObjects() {
        foreach (AffectedByGravity obj in _soundObjects) {
            obj.Scale(_bandOutput);
        }

    }

    private void CheckBandTreshHold() {
        _bandOutput = AudioAnalyzer.bands[_audioBand];
        //Debug.Log(_bandOutput);
        if (_bandOutput > _bandTreshhold) {
            //Debug.Log("BOUNCE");
            BounceObjects();
        }
    }

    private void CreateSoundReactiveObjects() {
        for (int i = 0; i < _objectsCount; i++) {
            Vector3 randomDirection = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y + Random.Range(-3, 3), transform.position.z + Random.Range(-3, 3));
            //EXPOSE SIZE IN EDITOR FOR MODIFICATION
            AffectedByGravity ab = AffectedByGravity.Create(randomDirection, _size*0.5f, transform, _material);
            ab.transform.parent = transform;
            _soundObjects.Add(ab); 
        }
    }
}
