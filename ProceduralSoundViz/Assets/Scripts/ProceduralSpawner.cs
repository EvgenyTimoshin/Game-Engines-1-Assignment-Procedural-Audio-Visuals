using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpawner : MonoBehaviour {

	public Transform _spawnInfrontOf;
	[Range(1,30)]
	public int _spawnRandomSeed;
	public bool _affectedByMusic = false;
	[Range(1,10)]
	public int _musicBand;

	public Dictionary<GameObject,int> _spawnAbles = new Dictionary<GameObject,int>();

	void Awake(){
		foreach (Transform t in transform) {
			t.transform.parent = null;
			int freq = CalculateSpawnFrequency ();
			_spawnAbles.Add (t.gameObject, freq);
		}
	}

	void OnDisable(){
		foreach(KeyValuePair<GameObject, int> entry in _spawnAbles)
		{
			entry.Key.transform.parent = transform;
		}
	}

	// Use this for initialization
	void Start () {
	}

	IEnumerator SpawnObjects(GameObject go, int freq){
		while (true) {
			GameObject newObj = Instantiate (go, new Vector3 (0, 0, freq * 150), Quaternion.identity);
			yield return WaitForSeconds (freq);
		}
	}

	int CalculateSpawnFrequency(){
		_spawnRandomSeed = Mathf.Sqrt (_spawnRandomSeed);
		_spawnRandomSeed = _spawnRandomSeed * Random.Range (1, 20);
		return _spawnRandomSeed / 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
