using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpawner : MonoBehaviour {

	[Range(1,30)]
	public int _spawnRandomSeed;
	public bool _affectedByMusic = false;
	[Range(1,100)]
	public int _lifeTimeOfObjects;
	[Range(1,10)]
	public int _musicBand;
    public bool _enableSpawning = false;

    public Dictionary<GameObject,float> _spawnAbles = new Dictionary<GameObject, float>();
    //public Dictionary<GameObject, float> _spawnedObjects = new Dictionary<GameObject, float>();

	/// <summary>
	/// Awake this instance. And Assign spawn frequencies to the selected objects
	/// </summary>
	void Awake(){
		foreach (Transform t in transform) {
			//t.transform.parent = null;
			float freq = CalculateSpawnFrequency ();
            Debug.Log(freq);
            //_spawnedObjects.Add (t.gameObject, freq);
            if (_enableSpawning)
            {
                StartCoroutine(SpawnObjects(t.gameObject, freq));
            }
		}

	}

	/// <summary>
	/// Reset the spawnable object to be child of this object
	/// </summary>
	void OnDisable(){
		foreach(KeyValuePair<GameObject, float> entry in _spawnAbles)
		{
			entry.Key.transform.parent = transform;
		}
	}

	// Use this for initialization
	void Start () {
	}

    void Update()
    {
        
    }

    IEnumerator SpawnObjects(GameObject go, float freq){
		while (true) {
            //yield return WaitForSeconds (freq * Random.Range(0,3));
            GameObject newObj = new GameObject();
            newObj.SetActive(false);
            newObj = Instantiate (go, transform.TransformPoint(new Vector3 (transform.position.x,transform.position.y,
			 transform.position.z + freq * 500)), Quaternion.identity);
            newObj.SetActive(true);
            //float lifeTime = Random.Range(3, 10);
            //_spawnedObjects.Add(newObj,freq);
            yield return new WaitForSeconds (freq);
            Destroy(newObj);
            yield return new WaitForSeconds(freq);
            SpawnObjects(newObj, freq);
		}
	}

	float CalculateSpawnFrequency(){
		_spawnRandomSeed = (int)Mathf.Sqrt (_spawnRandomSeed);
		_spawnRandomSeed = _spawnRandomSeed * Random.Range (1, 20);
		return _spawnRandomSeed /2;
	}
	
	// Update is called once per frame
	

    private void UpdateSpawnedObjects() {
    }
}
