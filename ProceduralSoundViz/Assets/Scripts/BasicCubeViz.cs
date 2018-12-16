using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCubeViz : MonoBehaviour {

    public int _blockAmount = 7;
    public float _blockSize = 1;
    public float _gapScale = 2;
    public float _responsiveness = 3.0f;
    private List<GameObject> _cubes = new List<GameObject>();


	// Use this for initialization
	void Start () {
        for (int i = 0; i < _blockAmount; i++) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(transform.position.x + i * _gapScale, transform.position.y, transform.position.z);
            cube.transform.localScale = new Vector3(_blockSize, _blockSize, _blockSize);

            _cubes.Add(cube);
        }
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCubes();
	}

    void UpdateCubes() {
        for(int i = 0; i < _cubes.Count; i ++) {
            Vector3 ls = _cubes[i].transform.localScale;
            ls.y = Mathf.Lerp(ls.y, 1 + (AudioAnalyzer.bands[i] * _blockSize*5), Time.deltaTime * _responsiveness);
            _cubes[i].transform.localScale = ls;
        }
    }
}
