﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHeightAdjuster : MonoBehaviour
{
	public Transform player;
	public Camera cam;
    // Update is called once per frame
    void Update()
    {
        if(player.position.y < -3){
        	cam.cullingMask = 1 << LayerMask.NameToLayer("MiniMapFloor0");
        }
        else if((player.position.y > -3) && (player.position.y < 6)){
        	cam.cullingMask = 1 << LayerMask.NameToLayer("MiniMapFloor1");
        }
        else if(player.position.y > 6){
        	cam.cullingMask = 1 << LayerMask.NameToLayer("MiniMapFloor2");
        }
    }
}
