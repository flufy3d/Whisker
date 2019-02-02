using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayersScript : MonoBehaviour {

public Vector3 offset;
private Vector3 newPosition;
public List<GameObject> targets=new List<GameObject>();
private Bounds bounds;
private Vector3 centerPoint;
private Vector3 refPos;
public float minZoom=10f;
public float maxZoom=60f;

private float newZoom;
public float zoomLimiter=20f;

	void Start () {
		targets.AddRange(GameObject.FindGameObjectsWithTag("Jerry"));
		targets.Add(GameObject.FindGameObjectWithTag("Tom"));
	}
	
	void LateUpdate () {
		Move();
		Zoom();
	}

void Zoom(){
	newZoom=Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance()/zoomLimiter);
	offset=new Vector3(0,newZoom,0);
}

float GetGreatestDistance(){
	if(bounds.size.x>bounds.size.z){
		return bounds.size.x;
	}else{
		return bounds.size.z;
	}
	
}

	void Move(){
centerPoint = GetCenterPoint();
		newPosition=centerPoint+offset;
		transform.position=Vector3.SmoothDamp(transform.position, newPosition, ref refPos, 0.5f);
	}

	Vector3 GetCenterPoint(){
		if(targets.Count==1){
			return targets[0].transform.position;
		}
		bounds=new Bounds(targets[0].transform.position, Vector3.zero);
		for(int i=0; i<targets.Count;i++){
			bounds.Encapsulate(targets[i].transform.position);
		}
		return bounds.center;
	}


	public void RemoveObjectFromList(GameObject obj){
		targets.Remove(obj);
	}
}
