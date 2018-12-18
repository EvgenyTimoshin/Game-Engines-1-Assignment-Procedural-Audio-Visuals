using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour {

    Mesh _mesh;
    //Vector3[] _verticies;
    List<Vector3> _verticies = new List<Vector3>();
    int[] _triangles;
    [Range(0,6)]
    public int _band;

    public int xSize = 20;
    public int zSize = 20;

    // Use this for initialization
    void Start () {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        CreateShape();
       
	}


    // Update is called once per frame
    void Update()
    {
        UpdateMesh();
    }


    void CreateShape() {
        //_verticies = new Vector3[(xSize + 1) * (zSize + 1)];
        var verticiesLenght = (xSize + 1) * (zSize + 1);

       
        for (int i = 0, z = 0; z < zSize; z++) {

            for (int x = 0; x <= xSize; x++) {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                //_verticies[i] = new Vector3(x, y, z);
                _verticies.Add(new Vector3(x, y, z));
                i++;
            }
        }

        Debug.Log("Original COunt :" + _verticies.Count);
        Debug.Log("Difference between 2 : " + (verticiesLenght - _verticies.Count));
        var fill = verticiesLenght - _verticies.Count;

        for (int i = 0; i < fill; i++) {
            _verticies.Add(new Vector3(0, 0, 0));
        }

        Debug.Log("Count of Verticies : " + _verticies.Count + "But meant to be : " + verticiesLenght );

        _triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++) {
            for (int x = 0; x < xSize; x++)
            {
                _triangles[tris + 0] = vert + 0;
                _triangles[tris + 1] = vert + xSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + xSize + 1;
                _triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }

            vert++;

        }

    }

    void UpdateMesh() {

        var AudioSample = AudioAnalyzer.bands[_band];
        List<Vector3> _oldVerticies = new List<Vector3>(_verticies);
        _verticies.Clear();
        var verticiesLenght = (xSize + 1) * (zSize + 1);

        for (int i = 0, z = 0; z < zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y;
                Vector3 vertex;

                if (i < _oldVerticies.Count){
                    vertex = _oldVerticies[i];
                    y = Mathf.PerlinNoise(x * AudioSample, z * AudioSample) * 2f;
                    vertex.y = Mathf.Lerp(vertex.y, 1 + y * 5, Time.deltaTime * 2f);
                    _verticies.Add(new Vector3(x,vertex.y,z));
                }
                else{
                    y = Mathf.PerlinNoise(x * AudioSample, z * AudioSample) * 2f;
                    _verticies.Add(new Vector3(x, y, z));
                    
                }
                i++;
            }
        }
        var fill = verticiesLenght - _verticies.Count;

        for (int i = 0; i < fill; i++)
        {
            _verticies.Add(new Vector3(0, 0, 0));
        }

        _triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                _triangles[tris + 0] = vert + 0;
                _triangles[tris + 1] = vert + xSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + xSize + 1;
                _triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }

            vert++;

        }

        _mesh.Clear();
        _mesh.vertices = _verticies.ToArray();
        _mesh.triangles = _triangles;

        _mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (_verticies == null) {
            return;
        }

        for (int i = 0; i < _verticies.Count; i++) {
            Gizmos.DrawSphere(_verticies[i], .1f);
        }
    }

}
