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
    protected List<Bubble> _soundObjects = new List<Bubble>();
    protected Material _material;
    protected float _bandOutput;

    /// <summary>
    /// Static method to create and instance of this class as part of a game object
    /// </summary>
    /// <typeparam name="T">The type of child class of this object that will be created</typeparam>
    /// <param name="size">The size of this object</param>
    /// <param name="objectCount">How many bubbles will be created based on this</param>
    /// <param name="audioBand">The audio band this object will use to control child objects</param>
    /// <param name="treshHold">The treshhold of sound that this object will react to</param>
    /// <param name="color">The color this object will be</param>
    /// <returns></returns>
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
	
    /*
	// Update is called once per frame
	void Update () {
        //Debug.Log("Running");
        CheckBandTreshHold();
	}
    */

    /// <summary>
    /// Called once every physics update
    /// </summary>
    private void FixedUpdate()
    {
        CheckBandTreshHold();
    }

    /// <summary>
    /// Updates the children bubbles with values that are changed in the editor from parent object
    /// </summary>
    /// <param name="strenght">Strenght of the emission lerp</param>
    /// <param name="emissionLerp">Bool to say if emmission lerp is on or not</param>
    /// <param name="lerpMode">Sets the lerp mode to lerpy scale or not</param>
    public void UpdateChildrenBubbles(float strenght, bool emissionLerp, bool lerpMode) {
        foreach (Bubble a in _soundObjects)
        {
            a.SetEmissionLepring(emissionLerp);
            a.SetLerpyScale(lerpMode);
            a.SetEmissionLerpStrenght(strenght);
        }
    }

    /// <summary>
    /// Updates the emission strenght of the bubble objects
    /// </summary>
    /// <param name="strenght"></param>
    public void UpdateEmissionLerpStrenght(float strenght) {
        foreach (Bubble a in _soundObjects)
        {
            a.SetEmissionLerpStrenght(strenght);
        }
    }

    /// <summary>
    /// Updates the treshold of sound reaction of bubbles
    /// </summary>
    /// <param name="newTreshhold"></param>
    public void UpdateTreshhold(float newTreshhold) {
        _bandTreshhold = newTreshhold;
    }

    /// <summary>
    /// Sets wether the bubbles will emit to the sound of the music or not
    /// </summary>
    /// <param name="set"></param>
    public void SetEmissionLerpMode(bool set){
        foreach (Bubble a in _soundObjects)
        {
            a.SetEmissionLepring(set);
        }
    }

    /// <summary>
    /// Set wether lerpy scale will be user or not
    /// </summary>
    /// <param name="set"></param>
    public void SetLerpMode(bool set) {
        foreach (Bubble a in _soundObjects) {
            a.SetLerpyScale(set);
        }
    }

    /// <summary>
    /// Controls the child bubbles bounce
    /// </summary>
    private void BounceObjects() {
        foreach (Bubble obj in _soundObjects) {
            obj.Scale(_bandOutput);
        }

    }

    /// <summary>
    /// Checks the audio band treshhold and calls the child bubbles to scale if treshold is reached
    /// </summary>
    private void CheckBandTreshHold() {
        _bandOutput = AudioAnalyzer.bands[_audioBand];
        //Debug.Log(_bandOutput);
        if (_bandOutput > _bandTreshhold) {
            //Debug.Log("BOUNCE");
            BounceObjects();
        }
    }

    /// <summary>
    /// Creates the sound interactive objects
    /// </summary>
    private void CreateSoundReactiveObjects() {
        for (int i = 0; i < _objectsCount; i++) {
            Vector3 randomDirection = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y + Random.Range(-3, 3), transform.position.z + Random.Range(-3, 3));
            //EXPOSE SIZE IN EDITOR FOR MODIFICATION
            Bubble ab = Bubble.Create(randomDirection, _size*0.5f, transform, _material);
            ab.transform.parent = transform;
            _soundObjects.Add(ab); 
        }
    }
}
