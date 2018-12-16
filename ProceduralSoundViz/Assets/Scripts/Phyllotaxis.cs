using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {

    public GameObject _creature;
    public int _n = 5;
    public float _scale;
    public float _degree;
    private Vector2 _phyllotaxisPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space)) {
            _phyllotaxisPosition = CalculatePhylllotaxis(_degree, _scale, _n);
            GameObject creature = (GameObject)Instantiate(_creature);
            creature.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, transform.position.z);
            _n++;
        }
	}

    private Vector2 CalculatePhylllotaxis(float degree, float scale, int count)
    {

        double angle = count * (degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(count);
        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);

        return new Vector2(x, y);
    }
}
