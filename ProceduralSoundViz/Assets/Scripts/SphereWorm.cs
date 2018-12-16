using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWorm : Creature {

    
	// Use this for initialization
	void Start () {
        SetupCreature();
	}

    protected override void SetupCreature() {
        _driver = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _driver.transform.position = transform.position;
        _driver.transform.parent = transform;
    } 
}
