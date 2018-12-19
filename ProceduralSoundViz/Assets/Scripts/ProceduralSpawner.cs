using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpawner : MonoBehaviour {

	public Transform _spawnInfrontOf;
	[Range(1,30)]
	public int _spawnRandomSeed;
	public bool _affectedByMusic = false;
	[Range(1,100)]
	public int _lifeTimeOfObjects;
	[Range(1,10)]
	public int _musicBand;

	public Dictionary<GameObject,int> _spawnAbles = new Dictionary<GameObject,int>();

	/// <summary>
	/// Awake this instance. And Assign spawn frequencies to the selected objects
	/// </summary>
	void Awake(){
		foreach (Transform t in transform) {
			t.transform.parent = null;
			int freq = CalculateSpawnFrequency ();
			_spawnAbles.Add (t.gameObject, freq);
			StartCoroutine (SpawnObjects (t.gameObject, freq));
		}
	}

	/// <summary>
	/// Reset the spawnable object to be child of this object
	/// </summary>
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
			//yield return WaitForSeconds (freq * Random.Range(0,3));
			GameObject newObj = Instantiate (go, new Vector3 (_spawnInfrontOf.transform.position.x, _spawnInfrontOf.transform.position.y,
				_spawnInfrontOf.transform.position.z + freq * 500), Quaternion.identity);
			yield return new WaitForSeconds (freq);
		}
	}

	int CalculateSpawnFrequency(){
		_spawnRandomSeed = (int)Mathf.Sqrt (_spawnRandomSeed);
		_spawnRandomSeed = _spawnRandomSeed * Random.Range (1, 20);
		return _spawnRandomSeed / 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
