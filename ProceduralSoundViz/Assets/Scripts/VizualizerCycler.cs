using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VizualizerCycler : MonoBehaviour {

    public string[] _vizualiserSceneNames;
    public float _timeBetweenVizualiserScenes = 30f;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(PlayVizualisers(_timeBetweenVizualiserScenes));
	}

    IEnumerator PlayVizualisers(float timeBetween) {
        int i = 0;
        while (i < _vizualiserSceneNames.Length) {

            SceneManager.LoadScene(_vizualiserSceneNames[i]);
            yield return new WaitForSeconds(timeBetween);
            i++;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
