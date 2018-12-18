﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomForViz : MonoBehaviour {

    Terrain _left;
    Terrain _right;
    Terrain _bottom;
    Terrain _back;
    public int _roomXSize = 230;
    public int _roomZSize = 300;
	// Use this for initialization
	void Start () {
        /*
        _left = Terrain.Create(_roomXSize, _roomZSize, false,true, 1);
        _left.gameObject.transform.localPosition = new Vector3(-112,-150,-215);
        _left.transform.Rotate(-90, -90, 0);
        
        _right = Terrain.Create(_roomXSize, _roomZSize, false,true, 1);
        _right.transform.position = new Vector3(111, -150, 12);
        _right.transform.Rotate(-90, 90, 0);

        
        _bottom = Terrain.Create(_roomXSize, _roomZSize, false,true, 1);
        _bottom.transform.position = new Vector3(-115, -143, -274);
        _bottom.transform.Rotate(0, 0, 0);
        */

        
        _back = Terrain.Create(_roomXSize, _roomZSize, false,true, 1);
        _back.transform.position = new Vector3(-115, -149, 18);
        _back.transform.Rotate(-90, 0, 0);
        
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
