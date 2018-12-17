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
        _driver.transform.localScale =new Vector3( _size, _size, _size);
        
    }

    public override void CreateSegments()
    {
        float depth = _size * 0.05f;
        Vector3 startPos = Vector3.forward * depth;


        GameObject previous = null;

        for (int i = 0; i < _segmentNumber; i++)
        {
            GameObject segment = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Rigidbody rb = segment.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.mass = 1.0f;
            Vector3 pos = startPos + (-Vector3.forward * depth * 4 * i);
            segment.transform.position = transform.TransformPoint(pos);
            segment.transform.rotation = transform.rotation;
            segment.transform.parent = this.transform;
            segment.transform.localScale = new Vector3(_size, _size, _size);

            if (previous != null)
            {
                HingeJoint j = segment.AddComponent<HingeJoint>();
                j.connectedBody = previous.GetComponent<Rigidbody>();
                j.autoConfigureConnectedAnchor = false;
                j.anchor = new Vector3(0, 0, -2f);
                j.connectedAnchor = new Vector3(0, 0, 2f);
                j.axis = Vector3.right;
                j.useSpring = true;
                JointSpring js = j.spring;
                js.spring = spring;
                js.damper = damper;
                j.spring = js;
            }
            previous = segment;

        }

    }
}
